using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ModularAbyss.Scripts.Core;
using ModularAbyss.Scripts.Interfaces;
using ModularAbyss.Scripts.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularAbyss.Scripts.Components.Gameobjects.Entities.Enemies
{
    public class Slime : Enemy
    {
        /* Texture*/
        protected const int TEXTURE_SIZE = 64;
        protected int _animationFrameCount = 6;

        public Slime(bool needsToBeCreated = false) : base(needsToBeCreated)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
            /* Animation */
            _entityAnimations = new Dictionary<string, Texture2D>()
            {
                {"ANIM_IDLE", Main.Content.Load<Texture2D>(SPRITES.SLIME_IDLE)},
                {"ANIM_WALK", Main.Content.Load<Texture2D>(SPRITES.PLAYER_WALK)},
                {"ANIM_RUN", Main.Content.Load<Texture2D>(SPRITES.PLAYER_RUN)},
            };
            _currentAnim = _entityAnimations.GetValueOrDefault("ANIM_IDLE");
        }
        protected override void OnTwoGameObjectsCollision(ICollider object1, ICollider object2) { return; }

        protected override void Draw(GameTime gameTime, ref SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, ref spriteBatch);
            if (GLOBAL.DEBUGING)
            {
                Texture2D _texture = new Texture2D(Main.GraphicsDevice, 1, 1);
                _texture.SetData(new Color[] { Color.Blue });
                spriteBatch.Draw(_texture, new Vector2(Position.X + 120, Position.Y + 120), Bounds, Color.White);
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
            Bounds = new Rectangle(new Point((int)Position.X + 120, (int)Position.Y + 120), new Point(TEXTURE_SIZE + 20, TEXTURE_SIZE + 20));
        }

        public override void Attack(Player p, GameTime gameTime)
        {
            if (attackTime <= 0)
            {
                p.HurtWithStats(Stats.getStatValue(STATS_ID.DAMAGE), this);
                attackTime = Stats.getStatValue(STATS_ID.ATTACK_SPEED);
            }
        }

        public override Vector2 GetHealthBarPos()
        {
            return new Vector2(Position.X + 90, Position.Y + 60);
        }
    }
}
