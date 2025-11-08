using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ModularAbyss.Scripts.Components.GameObjectNamespace;
using ModularAbyss.Scripts.Components.HudObjectNamespace;
using ModularAbyss.Scripts.Interfaces;
using ModularAbyss.Scripts.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularAbyss.Scripts.Components.HudObjectNamespace.Objects
{
    internal class Button : HudObject
    {

        private Rectangle _buttonRect;

        /* Events */
        public delegate void OnHoverHandler(Button btn);
        public event OnHoverHandler OnHover;
        public delegate void OnClickHandler(Button btn);
        public event OnClickHandler OnClick;

        public Button()
        {
            _buttonRect = new Rectangle()
            {
                Width = GetTexture2D().Width,
                Height = GetTexture2D().Height,
            };
        }

        protected override void Draw(GameTime gameTime, ref SpriteBatch spriteBatch)
        {
            _buttonRect.Location = Position.ToPoint();
            spriteBatch.Draw(GetTexture2D(), Position, null, Color.White, 0, new Vector2(0, 0), new Vector2(Scale, Scale), SpriteEffects.None, 0f);

            if (GLOBAL.DEBUGING)
            {
                Texture2D _texture = new Texture2D(Main.getInstance().GraphicsDevice, 1, 1);
                _texture.SetData(new Color[] { Color.Green });
                spriteBatch.Draw(_texture, new Vector2(Position.X, Position.Y), _buttonRect, Color.White);
            }

        }

        protected override void Update(GameTime gameTime, List<ICollider> colliders)
        {
            base.Update(gameTime, colliders);

            MouseObject mouse = Main.Mouse;

            if (mouse.CollisionRect.Intersects(_buttonRect))
            {
                OnHover?.Invoke(this);
                if (mouse.LeftButtonPressed)
                {
                    OnClick?.Invoke(this);
                }
            }
        }

        protected override void setTexture2D(ref Texture2D texture2D)
        {
            texture2D = new Texture2D(Main.getInstance().GraphicsDevice, 300, 100);
        }
    }
}
