using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ModularAbyss.Scripts.Components.StatNamespace;
using ModularAbyss.Scripts.Core;
using ModularAbyss.Scripts.Interfaces;
using ModularAbyss.Scripts.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularAbyss.Scripts.Components.GameObject.Objects.Entity
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

        private Stats Stats = new Stats();

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
        /// Defini la position de la bar de vie
        /// </summary>
        /// <returns>Sa position</returns>
        protected abstract Vector2 SetHealthBarPos();
    }
}
