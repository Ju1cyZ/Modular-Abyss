using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ModularAbyss.Scripts.Components.StatNamespace;
using ModularAbyss.Scripts.Core;
using ModularAbyss.Scripts.Interfaces;
using ModularAbyss.Scripts.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ModularAbyss.Scripts.Components.GameObjectNamespace.Objects.Entity
{
    public abstract class Entity : GameObject, ITextureAnimated
    {
        /* Animation */
        protected Dictionary<string, Texture2D> _entityAnimations = new Dictionary<string, Texture2D>();
        protected Texture2D _currentAnim;
        protected int _currentAnimIndex = 0;
        protected float _animationTimeBetweenFrame = 0;

        protected int _textureY = 0;
        protected SpriteEffects _spriteEffect = SpriteEffects.None;

        /* Overlay */
        protected Texture2D healthBarsTexture;
        private const int HEALTH_BAR_SIZE_X = 48; 
        private const int HEALTH_BAR_SIZE_Y = 16;

        /* Entity Properties */
        public Stats Stats = new Stats();

        public int Health {
            get
            {
                return Stats.getStatValue(STATS_ID.HEALTH);
            }
            set {
                Stats.setStatValue(STATS_ID.HEALTH, value);
            }
        }

        public int MAX_HEALTH
        {
            get {
                return Stats.getStat(STATS_ID.HEALTH).MAX_VALUE;
            }
        }

        protected override void Initialize()
        {
            base.Initialize();

            //Charge la texture contenant toutes les barres
            healthBarsTexture = game.Content.Load<Texture2D>(SPRITES.BARS);
        }

        protected override void Draw(GameTime gameTime, ref SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(healthBarsTexture, SetHealthBarPos(), getCorrectHealthBar(Health), Color.White, 0, new Vector2(0, 0), new Vector2(3, 3), _spriteEffect, 0f);
        }

        /// <summary>
        /// Permet d'afficher la bar de vie qui correspond le plus a la vie de l'entiter
        /// </summary>
        /// <param name="health">La vie de l'entiter</param>
        /// <returns>La bar de vie selon le pourcentage de vie</returns>
        private Rectangle getCorrectHealthBar(int health)
        {
            /// <summary>
            /// 0 => 100%,
            /// 1 => 80%,
            /// 2 => 60%,
            /// 3 => 40%,
            /// 4 => 20%,
            /// 5 => 0%
            /// </summary>
            Rectangle[] healthBarRectangles = new Rectangle[] {
                new Rectangle(HEALTH_BAR_SIZE_X * 1, HEALTH_BAR_SIZE_Y * 4, HEALTH_BAR_SIZE_X, HEALTH_BAR_SIZE_Y),
                new Rectangle(HEALTH_BAR_SIZE_X * 2, HEALTH_BAR_SIZE_Y * 4, HEALTH_BAR_SIZE_X, HEALTH_BAR_SIZE_Y),
                new Rectangle(HEALTH_BAR_SIZE_X * 3, HEALTH_BAR_SIZE_Y * 4, HEALTH_BAR_SIZE_X, HEALTH_BAR_SIZE_Y),
                new Rectangle(HEALTH_BAR_SIZE_X * 4, HEALTH_BAR_SIZE_Y * 4, HEALTH_BAR_SIZE_X, HEALTH_BAR_SIZE_Y),
                new Rectangle(HEALTH_BAR_SIZE_X * 5, HEALTH_BAR_SIZE_Y * 4, HEALTH_BAR_SIZE_X, HEALTH_BAR_SIZE_Y),
                new Rectangle(HEALTH_BAR_SIZE_X * 6, HEALTH_BAR_SIZE_Y * 4, HEALTH_BAR_SIZE_X, HEALTH_BAR_SIZE_Y),
            };

            if (health > MAX_HEALTH * 0.8f)
            {
                return healthBarRectangles[0];
            }
            else if (health > MAX_HEALTH * 0.6f)
            {
                return healthBarRectangles[1];
            }
            if (health > MAX_HEALTH * 0.4f)
            {
                return healthBarRectangles[2];
            }
            if (health > MAX_HEALTH * 0.2f)
            {
                return healthBarRectangles[3];
            }
            if (health > MAX_HEALTH * 0f)
            {
                return healthBarRectangles[4];
            }
            else
            {
                return healthBarRectangles[5];
            }
        }

        /// <summary>
        /// Permet d'infliger de degats a l'enemi
        /// </summary>
        /// <param name="damage">Le nombre de degats</param>
        /// <returns>La vie apres avoir infliger les degats</returns>
        public int Hurt(int damage)
        {
            if (Health - damage > 0)
            {
                Health -= damage;
            }
            else if(Health <= 0)
            {
                Health = 0;
                Die();
            }

            return Health;
        }


        /// <summary>
        /// Permet d'infliger de degats a l'enemy
        /// </summary>
        /// <param name="damage">Le nombre de degats</param>
        /// <param name="player">Le joueur qui inflige des degats</param>
        /// <param name="damageType">Le type de degats (STATS_ID.DAMAGE / STATS_ID.RANGED_DAMAGE etc...)</param>
        /// <returns>La vie apres avoir infliger les degats</returns>
        public int HurtWithStats(int damage, Player player, string damageType = STATS_ID.MELE_DAMAGE)
        {
            damage += player.Stats.getStatValue(damageType) - 1;

            int damageDealt = 0;

            if (Health - damage > 0)
            {
                Health -= damage;
                damageDealt = damage;
            }
            else if (Health - damage <= 0)
            {
                //Le degat reellement infliger
                damageDealt =  Math.Abs(0 - Health);
                Health = 0;
                Die();
            }

            if (player.Stats.getStatValue(STATS_ID.LIFE_STEAL) > 0)
            {
                //Chance aleatoire de soigner le joueur
                int rnd = GameManager.Random.Next(0, 100);
                if (rnd <= 2 * player.Stats.getStatValue(STATS_ID.LIFE_STEAL))
                {
                    player.Heal(damage * rnd / 100);
                }
            }

            return Health;
        }

        /// <summary>
        /// Permet d'infliger de degats a l'enemy
        /// </summary>
        /// <param name="damage">Le nombre de degats</param>
        /// <param name="Entity">L'entiter qui inflige des degats</param>
        /// <param name="damageType">Le type de degats (STATS_ID.DAMAGE / STATS_ID.RANGED_DAMAGE etc...)</param>
        /// <returns>La vie apres avoir infliger les degats</returns>
        public int HurtWithStats(int damage, Entity Entity, string damageType = STATS_ID.DAMAGE)
        {
            damage += Entity.Stats.getStatValue(damageType) - 1;

            int damageDealt = 0;

            if (Health - damage > 0)
            {
                Health -= damage;
                damageDealt = damage;
            }
            else if (Health - damage <= 0)
            {
                //Le degat reellement infliger
                damageDealt = Math.Abs(0 - Health);
                Health = 0;
                Die();
            }

            if (Entity.Stats.getStatValue(STATS_ID.LIFE_STEAL) > 0)
            {
                //Chance aleatoire de soigner le joueur
                int rnd = GameManager.Random.Next(0, 100);
                if (rnd <= 2 * Entity.Stats.getStatValue(STATS_ID.LIFE_STEAL))
                {
                    Entity.Heal(damage * rnd / 100);
                }
            }

            return Health;
        }

        /// <summary>
        /// La fonction qui tue l'enemi
        /// </summary>
        public void Die()
        {
            Destroy();
        }

        /// <summary>
        /// Permet de soigner le joueur
        /// </summary>
        /// <param name="amount">La quantiter de point de vie a donner</param>
        /// <returns>Les points de vie apres avoir soigner le joueur</returns>
        public int Heal(int amount)
        {
            Health += amount;
            if (Health > MAX_HEALTH)
            {
                Health = MAX_HEALTH;
            }

            return Health;
        }

        /// <summary>
        /// Defini la position de la bar de vie
        /// </summary>
        /// <returns>Sa position</returns>
        protected abstract Vector2 SetHealthBarPos();
    }
}
