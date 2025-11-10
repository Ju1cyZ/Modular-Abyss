using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ModularAbyss.Scripts.Components.GameObjectNamespace;
using ModularAbyss.Scripts.Components.GameObjectNamespace.Objects;
using ModularAbyss.Scripts.Components.GameObjectNamespace.Objects.Entity;
using ModularAbyss.Scripts.Components.GameObjectNamespace.Objects.Entity.Enemy;
using ModularAbyss.Scripts.Core;
using ModularAbyss.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularAbyss.Scripts.Components.WaveNamespace
{
    internal class Wave
    {
        private Main _main;
        private List<Enemy> _enemies;

        public Vector2 mapSize = new Vector2();
        public int SpawnRaduisMax = 800;
        public int SpawnRaduisMin = 200;

        public Wave(List<Enemy> enemies)
        {
            _main = Main.getInstance();

            _enemies = enemies;

            _main.OnUpdate += Update;
        }

        protected virtual void Update(GameTime gameTime, List<ICollider> colliders)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.M))
                SpawnEnemies();

        }

        /// <summary>
        /// Permet de faire apparaitre les enemies de la vague
        /// </summary>
        public List<Enemy> SpawnEnemies()
        {
            foreach (Enemy item in _enemies)
            {
                item.Position = FindSpawnablePos(item);
                item.Create();
            }

            return _enemies;
        }

        /// <summary>
        /// Permet de recuperer un endroit ou une entiter peut apparaitre
        /// </summary>
        /// <returns>Une position</returns>
        private Vector2 FindSpawnablePos(Entity entity)
        {
            Player player;
            Random rnd = new Random();
            Vector2 spawnPos;
            bool checkCollision;

            do
            {
                checkCollision = false;

                player = Main.ActiveScene.Player;

                // Recupere un direction aleatoire depuis le joueur
                Vector2 rndPos = new Vector2(rnd.Next(0, (int)mapSize.X), rnd.Next(0, (int)mapSize.Y));
                Vector2 spawnDirection = Vector2.Normalize(player.Position - rndPos);

                // trouve une position avec la direction entre SpawnRaduisMin et SpawnRaduisMax
                spawnPos = spawnDirection * rnd.Next(SpawnRaduisMin, SpawnRaduisMax) + player.Position;

            }
            while (checkCollision);

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
