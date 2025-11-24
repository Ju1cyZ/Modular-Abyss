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
            tilemap.LoadFromCSV(TILEMAPS.GAME_FLOOR);
            tilemapCollider.LoadFromCSV(TILEMAPS.GAME_WALL);
        }

    }
}
