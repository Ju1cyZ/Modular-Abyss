using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ModularAbyss.Scripts.Components.HudObjects;
using ModularAbyss.Scripts.Core;
using ModularAbyss.Scripts.Interfaces;
using ModularAbyss.Scripts.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularAbyss.Scripts.Components.Gameobjects
{
    public abstract class Entity : GameObject, ITextureAnimated, IStat, ILifeBar
    {
        /* Animation */
        protected Dictionary<string, Texture2D> _entityAnimations = new Dictionary<string, Texture2D>();
        protected Texture2D _currentAnim;
        protected int _currentAnimIndex = 0;
        protected float _animationTimeBetweenFrame = 0;

        protected int _textureY = 0;
        protected SpriteEffects _spriteEffect = SpriteEffects.None;

        public Stats Stats { get; set; } = new Stats();

        private ProgressBar _healthBar;
        public ProgressBar HealthBar 
        { 
            get => _healthBar;
            set => _healthBar = value;
        }

        public int Health
        {
            get
            {
                return Stats.getStatValue(STATS_ID.HEALTH);
            }
            set
            {
                Stats.setStatValue(STATS_ID.HEALTH, value);
            }
        }

        public int MAX_HEALTH
        {
            get
            {
                return Stats.getStat(STATS_ID.HEALTH).MAX_VALUE;
            }
        }

        /// <summary>
        /// construire
        /// </summary>
        /// <param name="needsToBeCreated">Permet de determiner si l'objet se creer tout seul ou doit etre creer avec la fonction Create()</param>
        public Entity(bool needsToBeCreated = false) : base(needsToBeCreated)
        {

        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void Draw(GameTime gameTime, ref SpriteBatch spriteBatch) { }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
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
            else if (Health <= 0)
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
                // Le degat reellement infliger
                damageDealt = Math.Abs(0 - Health);
                Health = 0;
                Die();
            }

            if (player.Stats.getStatValue(STATS_ID.LIFE_STEAL) > 0)
            {
                //Chance aleatoire de soigner le joueur
                int rnd = GameManager.Random.Next(0, 100);
                if (rnd <= 2 * player.Stats.getStatValue(STATS_ID.LIFE_STEAL))
                {
                    player.Heal(damageDealt * rnd / 100); // Soigne le joueur de pourcentage tirer (rnd) des degats infliger
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
                    Entity.Heal(damageDealt * rnd / 100);
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

        public override void Destroy()
        {
            base.Destroy();
            HealthBar.Destroy();
        }

        /// <summary>
        /// Defini la position de la bar de vie
        /// </summary>
        /// <returns>Sa position</returns>
        public abstract Vector2 GetHealthBarPos();
    }

}
