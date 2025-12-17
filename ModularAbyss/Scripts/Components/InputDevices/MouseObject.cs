using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ModularAbyss.Scripts.Core;
using ModularAbyss.Scripts.Interfaces;
using ModularAbyss.Scripts.Structures;
using System.Collections.Generic;

namespace ModularAbyss.Scripts.Components.InputDevices
{
    public class MouseObject
    {
        private MouseState mState
        {
            get => Mouse.GetState();
        }

        private Main _main;
        public Rectangle CollisionRect;
        private Rectangle MouseType;

        /// <summary>
        /// Get Left Click State
        /// </summary>
        public bool LeftButtonPressed
        {
            get
            {
                return mState.LeftButton == ButtonState.Pressed;
            }
        }

        /// <summary>
        /// Get Right Click State
        /// </summary>
        public bool RightButtonPressed
        {
            get
            {
                return mState.RightButton == ButtonState.Pressed;
            }
        }

        /// <summary>
        /// La position de la souris
        /// </summary>
        public Point Position
        {
            get
            {
                return mState.Position - GameManager.CameraPosition.ToPoint();
            }
        }

        public MouseObject()
        {
            _main = Main.getInstance();
            _main.IsMouseVisible = false;

            CollisionRect = new Rectangle()
            {
                Width = 16 * 2,
                Height = 16 * 2,
            };
        }

        public void Update(GameTime gameTime, List<ICollider> colliders)
        {
            CollisionRect.X = Position.X;
            CollisionRect.Y = Position.Y;

            if (LeftButtonPressed)
            {
                MouseType = MOUSE_ID.HAND_FINGER_CLICKED;
            }
            else
            {
                MouseType = MOUSE_ID.HAND_FINGER;
            }
        }

        public void Draw(GameTime gameTime, ref SpriteBatch spriteBatch)
        {
            /* Custom Cursor */
            if (GLOBAL.DEBUGING)
            {
                Texture2D _texture = new Texture2D(Main.getInstance().GraphicsDevice, 1, 1);
                _texture.SetData(new Color[] { Color.Green });
                spriteBatch.Draw(_texture, new Vector2(Position.X, Position.Y), CollisionRect, Color.White);
            }

            spriteBatch.Draw(
                _main.Content.Load<Texture2D>(SPRITES.MOUSE_ICONS),
                Position.ToVector2() + GameManager.CameraPosition,
                MouseType,
                Color.White, 0,
                new Vector2(0, 0),
                new Vector2(2, 2),
                SpriteEffects.None,
                0f
            );
        }
    }
}
