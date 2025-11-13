using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ModularAbyss.Scripts.Interfaces;
using ModularAbyss.Scripts.Structures;
using System.Collections.Generic;

namespace ModularAbyss.Scripts.Components.InputDevices
{
    public class MouseObject : InputManager
    {
        private Main _main;
        public Rectangle CollisionRect;
        private Rectangle MouseType;

        public bool LeftButtonPressed
        {
            get
            {
                MouseState mState = Mouse.GetState();
                return mState.LeftButton == ButtonState.Pressed;
            }
        }

        public bool RightButtonPressed
        {
            get
            {
                MouseState mState = Mouse.GetState();
                return mState.RightButton == ButtonState.Pressed;
            }
        }

        public Point Position
        {
            get
            {
                MouseState mState = Mouse.GetState();
                return mState.Position;
            }
        }

        public MouseObject()
        {
            _main = Main.getInstance();

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
            MouseState mState = Mouse.GetState();

            if (GLOBAL.DEBUGING)
            {
                Texture2D _texture = new Texture2D(Main.getInstance().GraphicsDevice, 1, 1);
                _texture.SetData(new Color[] { Color.Green });
                spriteBatch.Draw(_texture, new Vector2(Position.X, Position.Y), CollisionRect, Color.White);
            }

            spriteBatch.Draw(
                _main.Content.Load<Texture2D>(SPRITES.MOUSE_ICONS),
                Position.ToVector2(),
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
