using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ModularAbyss.Scripts.Components.Gameobjects;
using ModularAbyss.Scripts.Components.Gameobjects.Entities;
using ModularAbyss.Scripts.Components.Gameobjects.Entities.Enemies;
using ModularAbyss.Scripts.Components.HudObjects;
using ModularAbyss.Scripts.Components.Tilemaps;
using ModularAbyss.Scripts.Core;
using ModularAbyss.Scripts.Structures;
using System;
using System.Collections.Generic;

namespace ModularAbyss.Scripts.Components.Scene.Scenes
{
    public class GameScene : Scene
    {
        public Wave currentWave;
        private int waveNumber = 1;

        Vector2 _playableMapSize = new Vector2(29 * 80 - 80, 19 * 80 - 320);
        Vector2 _playableMapPosition = new Vector2(80,320);

        protected override void LoadContent()
        {
            tilemap = new Tilemap(TILESET.GRASS, 30, 20);
            tilemap.Position = new Vector2(0, 0);
            tilemapCollider = new TilemapCollider(TILESET.WALL, 30, 20);
            tilemapCollider.Position = new Vector2(0, 0);
            defineTiles();

            base.LoadContent();
        }

        public override void Initialize()
        {
            base.Initialize();

            GameManager.Camera.isSticky = false;
            CameraObject.Position = new Vector2(0, 0);
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
            GameObjects.Add("Player", new Player());
            Player.Position = new Vector2(500, 500);
            Player.inventory.Add(ITEMS.STEEL_SWORD);
            Player.OnDeath += (player) =>
            {
                // PLAYER DED :(
            };

            currentWave = GenerateNewWave(waveNumber);
            currentWave.OnEnemiesAllDead += EndOfWave;
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
            (HudObjects.GetValueOrDefault("HealthBar") as ProgressBar).isSticky = true;

            HudObjects.Add("HealthInfo", new Text($"{Player.Health} / {Player.MAX_HEALTH}"));
            HudObjects.GetValueOrDefault("HealthInfo").Position = new Vector2(54, 29);
            HudObjects.GetValueOrDefault("HealthInfo").Destroyed += DestroyHudobject;

            HudObjects.Add("CurrentWave", new Text($"{waveNumber + 1}"));
            HudObjects.GetValueOrDefault("CurrentWave").Position = new Vector2(54, 69);
            HudObjects.GetValueOrDefault("CurrentWave").Scale = 5;
            HudObjects.GetValueOrDefault("CurrentWave").Destroyed += DestroyHudobject;
        }


        /// <summary>
        /// Defini les tiles id dans les tilemaps
        /// </summary>
        public void defineTiles()
        {
            tilemap.LoadFromCSV(TILEMAPS.GAME_FLOOR);
            tilemapCollider.LoadFromCSV(TILEMAPS.GAME_WALL);
        }

        public Wave GenerateNewWave(int waveNumber)
        {
            List<Enemy> enemies = new List<Enemy>();
            for (int i = 0; i < 5 + Math.Pow(waveNumber / 2, 2); i++)
            {
                Enemy enemy = new Slime(true);
                enemy.Initialize();
                enemy.OnDestroyed += DestroyGameobject;
                enemies.Add(enemy);
            }

            Wave wave = new Wave(enemies);
            List<Enemy> spawnedEnemies = wave.SpawnEnemies();

            foreach (Enemy item in spawnedEnemies)
            {
                GameObjects.Add(item.GetType().ToString() + GameObjects.Count, item);
            }

            return wave;
        }

        public void EndOfWave(Wave wave)
        {
            wave.Destroy();
            waveNumber++;
            currentWave = GenerateNewWave(waveNumber);
            currentWave.OnEnemiesAllDead += EndOfWave;
            (HudObjects.GetValueOrDefault("CurrentWave") as Text).setText($"{waveNumber + 1}");
        }

        public override Vector2 GetPlayableMapSize()
        {
            return _playableMapSize;
        }

        public override Vector2 GetPlayableMapPosition()
        {
            return _playableMapPosition;
        }
    }
}
