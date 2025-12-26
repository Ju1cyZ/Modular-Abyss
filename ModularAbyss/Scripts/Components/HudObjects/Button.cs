using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ModularAbyss.Scripts.Components.InputDevices;
using ModularAbyss.Scripts.Core;
using ModularAbyss.Scripts.Interfaces;
using ModularAbyss.Scripts.Structures;
using System;
using System.Collections.Generic;

namespace ModularAbyss.Scripts.Components.HudObjects
{
    public class Button : HudObject
    {

        private Rectangle _buttonRect;

        private bool isHovered = false;

        private Text _text;

        public string Text
        {
            get
            {
                return _text.getText();
            }
            set
            {
                _text.setText(value);
            }
        }

        /* Events */
        public delegate void OnHoverHandler(Button btn);
        public event OnHoverHandler OnHover;
        public delegate void OnClickHandler(Button btn);
        public event OnClickHandler OnClick;

        public Button(Vector2 position = new(), float scale = 1, bool hidden = false) : base(position, scale, hidden)
        {
            _buttonRect = new Rectangle()
            {
                Width = UI_COMPONENTS.BUTTON.Width * (int)Scale,
                Height = UI_COMPONENTS.BUTTON.Height * (int)Scale,
            };

            _text = new Text(centered: true)
            {
                Scale = scale * 0.4f,
                Position = new Vector2(_buttonRect.Width / 2, _buttonRect.Height / 2) + position
            };
        }

        protected override void Draw(GameTime gameTime, ref SpriteBatch spriteBatch)
        {
            if (GLOBAL.DEBUGING)
            {
                Texture2D _texture = new Texture2D(Main.getInstance().GraphicsDevice, 1, 1);
                _texture.SetData(new Color[] { Color.Green });
                spriteBatch.Draw(_texture, new Vector2(Position.X, Position.Y) - GameManager.CameraPosition, _buttonRect, Color.White);
            }

            _buttonRect.Location = Position.ToPoint();
            spriteBatch.Draw(
                GetTexture2D(),
                Position,
                isHovered ? UI_COMPONENTS.BUTTON_HOVER : UI_COMPONENTS.BUTTON,
                Color.White,
                0,
                new Vector2(0, 0),
                new Vector2(Scale, Scale),
                SpriteEffects.None,
                0f
            );
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            MouseObject mouse = InputManager.MouseObject;

            _buttonRect = new Rectangle(_buttonRect.Location - GameManager.CameraPosition.ToPoint(), _buttonRect.Size);

            if (mouse.CollisionRect.Intersects(_buttonRect))
            {
                OnHover?.Invoke(this);
                isHovered = true;
                if (InputManager.MouseObject.LeftButtonPressedThisFrame)
                {
                    OnClick?.Invoke(this);
                }
            }
            else
            {
                isHovered = false;
            }
        }

        protected override void setTexture2D(ref Texture2D texture2D)
        {
            texture2D = Main.Content.Load<Texture2D>(SPRITES.UI);
        }

        public override void Destroy()
        {
            base.Destroy();
            _text.Destroy();
        }
    }
}

