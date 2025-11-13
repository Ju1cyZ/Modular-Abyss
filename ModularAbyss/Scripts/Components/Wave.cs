using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using ModularAbyss.Scripts.Components.Gameobjects.Entities;
using ModularAbyss.Scripts.Components.Gameobjects;
using ModularAbyss.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularAbyss.Scripts.Components
{
    public class Wave : IDestroyer
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
            Parallel.For(0, _enemies.Count, (i) =>
            {
                FindSpawnablePos(_enemies[i], i);
                _enemies[i].Create();
            });

            return _enemies;
        }

        /// <summary>
        /// Permet de recuperer un endroit ou une entiter peut apparaitre
        /// </summary>
        /// <returns>Une position</returns>
        private Vector2 FindSpawnablePos(Entity entity, int iteration)
        {
            Player player = Main.ActiveScene.Player;
            Random rnd;
            Vector2 spawnPos;
            bool checkCollision;
            int maxIteration = 500;
            int i = 0;

            do
            {
                checkCollision = false;
                rnd = new Random();

                // Recupere un direction aleatoire depuis le joueur
                Vector2 rndPos = new Vector2(rnd.Next(0, (int)mapSize.X), rnd.Next(0, (int)mapSize.Y));
                Vector2 spawnDirection = Vector2.Normalize(rndPos - player.Position);

                int distance = rnd.Next(SpawnRaduisMin, SpawnRaduisMax);

                // trouve une position avec la direction entre SpawnRaduisMin et SpawnRaduisMax
                spawnPos = spawnDirection * distance + player.Position;

                entity.Position = spawnPos;

                foreach (Rectangle item in Main.ActiveScene.tilemapCollider.Colliders)
                {
                    if (item.Intersects(entity.Bounds))
                    {
                        checkCollision = true;
                        break;
                    }
                }

                i++;
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
