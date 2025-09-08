using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        public const float MAX_HEALTH = 100;

        /* Entity Properties */

        public float health = MAX_HEALTH;

        protected override void Initialize()
        {
            base.Initialize();

            //Charge la texture contenant toutes les barres
            healthBarsTexture = game.Content.Load<Texture2D>(SPRITES.BARS);
        }

        protected override void Draw(GameTime gameTime, ref SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(healthBarsTexture, SetHealthBarPos(), getCorrectHealthBar(health), Color.White, 0, new Vector2(0, 0), new Vector2(3, 3), _spriteEffect, 0f);
        }

        /// <summary>
        /// Permet d'afficher la bar de vie qui correspond le plus a la vie de l'entiter
        /// </summary>
        /// <param name="health">La vie de l'entiter</param>
        /// <returns>La bar de vie selon le pourcentage de vie</returns>
        private Rectangle getCorrectHealthBar(float health)
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
            }; ;

            switch (health)
            {
                case > MAX_HEALTH * 0.8f :
                    return healthBarRectangles[0];
                case > MAX_HEALTH * 0.6f:
                    return healthBarRectangles[1];
                case > MAX_HEALTH * 0.4f:
                    return healthBarRectangles[2];
                case > MAX_HEALTH * 0.2f:
                    return healthBarRectangles[3];
                case > MAX_HEALTH * 0f:
                    return healthBarRectangles[4];
                default:
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
