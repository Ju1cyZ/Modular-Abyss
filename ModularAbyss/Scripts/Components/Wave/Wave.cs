using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ModularAbyss.Scripts.Components.GameObjectNamespace.Objects;
using ModularAbyss.Scripts.Components.GameObjectNamespace.Objects.Entity.Enemy;
using ModularAbyss.Scripts.Core;
using ModularAbyss.Scripts.Interfaces;

namespace ModularAbyss.Scripts.Components.WaveNamespace
{
    internal class Wave
    {
        private Main _main;
        private List<Enemy> _enemies;

        public Vector2 mapSize = new Vector2();
        public int SpawnRaduisMax = 200;
        public int SpawnRaduisMin = 50;

        public Wave(List<Enemy> enemies)
        {
            _main = Main.getInstance();

            _enemies = enemies;

            _main.OnUpdate += Update;
        }

        protected virtual void Update(GameTime gameTime, List<ICollider> colliders)
        {

        }

        /// <summary>
        /// Permet de faire apparaitre les enemies de la vague
        /// </summary>
        public void SpawnEnemies()
        {
            foreach (Enemy item in _enemies)
            {
                item.Position = FindSpawnablePos();
            }
        }

        /// <summary>
        /// Permet de recuperer un endroit ou une entiter peut apparaitre
        /// </summary>
        /// <returns>Une position</returns>
        private Vector2 FindSpawnablePos()
        {
            Player player;
            Random rnd = new Random();
            Vector2 spawnPos;

            do
            {
                player = Main.ActiveScene.Player;

                // Recupere un direction aleatoire depuis le joueur
                Vector2 rndPos = new Vector2(rnd.Next(0, (int)mapSize.X), rnd.Next(0, (int)mapSize.Y));
                Vector2 spawnDirection = Vector2.Normalize(player.Position - rndPos);

                // trouve une position avec la direction entre SpawnRaduisMin et SpawnRaduisMax
                spawnPos = spawnDirection * rnd.Next(SpawnRaduisMin, SpawnRaduisMax);
            }
            while (spawnPos.X > mapSize.X || spawnPos.Y > mapSize.Y || spawnPos.X < 0 || spawnPos.Y < 0);

            return spawnPos;
        }

        /// <summary>
        /// Permet de detruire la vague
        /// </summary>
        public virtual void Destroy()
        {
            _main.OnUpdate -= Update;

            foreach (Enemy item in _enemies)
            {
                item.Destroy();
            }
        }
    }
}
