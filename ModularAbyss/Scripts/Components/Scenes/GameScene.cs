using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ModularAbyss.Scripts.Components.Gameobjects;
using ModularAbyss.Scripts.Components.Gameobjects.Entities;
using ModularAbyss.Scripts.Components.Gameobjects.Entities.Enemies;
using ModularAbyss.Scripts.Components.HudObjects;
using ModularAbyss.Scripts.Components.Tilemaps;
using ModularAbyss.Scripts.Structures;
using System.Collections.Generic;

namespace ModularAbyss.Scripts.Components.Scene.Scenes
{
    public class GameScene : Scene
    {
        protected override void LoadContent()
        {
            tilemap = new Tilemap(TILESET.FLOOR, 10, 5);
            tilemap.Position = new Vector2(160, 160);
            tilemapCollider = new TilemapCollider(TILESET.WALL, 12, 7);
            defineTiles();

            base.LoadContent();
        }

        protected override void Draw(GameTime gameTime, ref SpriteBatch spriteBatch)
        {
            tilemap.Draw(gameTime, ref spriteBatch);
            tilemapCollider.Draw(gameTime, ref spriteBatch);
            base.Draw(gameTime, ref spriteBatch);
        }

        protected override void UpdateHudObjects()
        {
            ProgressBar healthBar = HudObjects.GetValueOrDefault("HealthBar") as ProgressBar;
            if (healthBar != null)
            {
                (HudObjects.GetValueOrDefault("HealthBar") as ProgressBar).value = Player.Health;
                (HudObjects.GetValueOrDefault("HealthBar") as ProgressBar).maxValue = Player.MAX_HEALTH;
                HudObjects.GetValueOrDefault("HealthBar").Scale = 5;
            }

            Text healthInfo = HudObjects.GetValueOrDefault("HealthInfo") as Text;
            if (healthInfo != null)
            {
                healthInfo.setText($"{Player.Health} / {Player.MAX_HEALTH}");
            }
        }

        /// <summary>
        /// Créer les gameobjects
        /// </summary>
        protected override void CreateGameObjects()
        {
            //GameObjects.Add("Slime", new Slime());
            //GameObjects.GetValueOrDefault("Slime").Position = new Vector2(500, 200);
            //(GameObjects.GetValueOrDefault("Slime") as Entity).Destroyed += DestroyGameobject;

            //GameObjects.Add("Slime_01", new Slime());
            //GameObjects.GetValueOrDefault("Slime_01").Position = new Vector2(700, 200);
            //(GameObjects.GetValueOrDefault("Slime_01") as Entity).Destroyed += DestroyGameobject;

            GameObjects.Add("Player", new Player());
            Player.inventory.Add(ITEMS.STEEL_KNIFE);
            Player.OnDeath += (player) =>
            {
                // PLAYER DED :(
            };


            List<Enemy> enemies = new List<Enemy>();
            for (int i = 0; i < 10; i++)
            {
                Enemy enemy = new Slime(true);
                enemy.Initialize();
                enemy.OnDestroyed += DestroyGameobject;
                enemies.Add(enemy);
            }

            Wave wave = new Wave(enemies);
            wave.mapSize = new Vector2(1920 - 160, 1080 - 160);
            List<Enemy> spawnedEnemies = wave.SpawnEnemies();

            foreach (Enemy item in spawnedEnemies)
            {
                GameObjects.Add(item.GetType().ToString() + GameObjects.Count, item);
            }
        }

        /// <summary>
        /// Créer les hudObjects
        /// </summary>
        protected override void CreateHudObjects()
        {
            /* Overlay */
            int HEALTH_BAR_SIZE_X = 48;
            int HEALTH_BAR_SIZE_Y = 16;

            HudObjects.Add("HealthBar", new ProgressBar(HEALTH_BAR_SIZE_X, HEALTH_BAR_SIZE_Y, 8));
            HudObjects.GetValueOrDefault("HealthBar").Destroyed += DestroyHudobject;

            HudObjects.Add("HealthInfo", new Text($"{Player.Health} / {Player.MAX_HEALTH}"));
            HudObjects.GetValueOrDefault("HealthInfo").Position = new Vector2(54, 29);
            HudObjects.GetValueOrDefault("HealthInfo").Destroyed += DestroyHudobject;
        }


        /// <summary>
        /// Defini les tiles id dans les tilemaps
        /// </summary>
        public void defineTiles()
        {
            //#region Floor
            //tilemap.SetTile(0, 0, 0);
            //for (int i = 0; i < 8; i++)
            //{
            //    tilemap.SetTile(1, 1 + i, 0);
            //}
            //tilemap.SetTile(2, 9, 0);

            //for (int y = 0; y < 3; y++)
            //{
            //    tilemap.SetTile(4, 0, 1 + y);
            //    for (int i = 0; i < 8; i++)
            //    {
            //        tilemap.SetTile(5, 1 + i, 1 + y);
            //    }
            //    tilemap.SetTile(6, 9, 1 + y);
            //}

            //tilemap.SetTile(8, 0, 4);
            //for (int i = 0; i < 8; i++)
            //{
            //    tilemap.SetTile(9, 1 + i, 4);
            //}
            //tilemap.SetTile(10, 9, 4);
            //#endregion

            //#region Wall
            //tilemapCollider.SetTile(13, 1, 0);
            //for (int i = 0; i < 8; i++)
            //{
            //    tilemapCollider.SetTile(14, 2 + i, 0);
            //}
            //tilemapCollider.SetTile(15, 10, 0);

            //for (int y = 0; y < 6; y++)
            //{
            //    tilemapCollider.SetTile(6, 0, 0 + y);
            //    tilemapCollider.SetTile(10, 11, 0 + y);
            //}

            //tilemapCollider.SetTile(18, 0, 6);
            //for (int i = 0; i < 10; i++)
            //{
            //    tilemapCollider.SetTile(19, 1 + i, 6);
            //}
            //tilemapCollider.SetTile(22, 11, 6);

            ///* Collision */

            //tilemapCollider.Colliders.Add(new Rectangle(new Point(0, 0), new Point(1920, 120)));
            //tilemapCollider.Colliders.Add(new Rectangle(new Point(0, 1080 - 120), new Point(1920, 120)));

            //tilemapCollider.Colliders.Add(new Rectangle(new Point(0, 0), new Point(160, 1080)));
            //tilemapCollider.Colliders.Add(new Rectangle(new Point(1920 - 160, 0), new Point(160, 1080)));

            //#endregion

            tilemap.LoadFromCSV(TILEMAPS.TEST_FLOOR);
            tilemapCollider.LoadFromCSV(TILEMAPS.TEST_WALL);
        }

    }
}
