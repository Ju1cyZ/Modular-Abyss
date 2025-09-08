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

namespace ModularAbyss.Scripts.Components.GameObject.Objects.Entity.Enemy.Enemies
{
    public class Slime : Enemy
    {
        /* Texture*/
        protected const int TEXTURE_SIZE = 64;
        protected int _animationFrameCount = 6;

        protected override void Initialize()
        {
            base.Initialize();
            /* Animation */
            _entityAnimations = new Dictionary<string, Texture2D>()
            {
                {"ANIM_IDLE", game.Content.Load<Texture2D>(SPRITES.SLIME_IDLE)},
                {"ANIM_WALK", game.Content.Load<Texture2D>(SPRITES.PLAYER_WALK)},
                {"ANIM_RUN", game.Content.Load<Texture2D>(SPRITES.PLAYER_RUN)},
            };
            _currentAnim = _entityAnimations.GetValueOrDefault("ANIM_IDLE");
        }

        protected override void Draw(GameTime gameTime, ref SpriteBatch spriteBatch)
        {
            Texture2D _texture = new Texture2D(game.GraphicsDevice, 1, 1);
            _texture.SetData(new Color[] { Color.Red });
            if (GLOBAL.DEBUGING)
            {
                spriteBatch.Draw(_texture, new Vector2(Position.X + 120, Position.Y + 120), CollisionRect, Color.White);
            }

            Rectangle rect = ((ITextureAnimated)this).GenerateTextureRect(TEXTURE_SIZE, TEXTURE_SIZE, TEXTURE_SIZE * _textureY, _animationFrameCount, ref _currentAnimIndex, (float)gameTime.ElapsedGameTime.TotalSeconds, ref _animationTimeBetweenFrame);
            spriteBatch.Draw(GetTexture2D(), Position + GameManager.CameraPosition, rect, Color.White, 0, new Vector2(0, 0), new Vector2(5, 5), _spriteEffect, 0f);

        }

        /// <summary>
        /// Defini la texture 2D du joueur
        /// </summary>
        /// <param name="texture2D">La variable dans la Classe GameObject</param>
        protected override void setTexture2D(ref Texture2D texture2D)
        {
            texture2D = _currentAnim;
        }

        /// <summary>
        /// Permet de recuperer la texture du GameObject
        /// </summary>
        /// <returns>La texture</returns>
        public override Texture2D GetTexture2D()
        {
            return _currentAnim;
        }

        /// <summary>
        /// Defini le box collider
        /// </summary>
        public override void SetColliderBounds()
        {
            CollisionRect = new Rectangle(new Point((int)Position.X + 120, (int)Position.Y + 120), new Point(TEXTURE_SIZE + 20, TEXTURE_SIZE + 20));
        }
    }
}
