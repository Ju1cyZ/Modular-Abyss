using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ModularAbyss.Scripts.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ModularAbyss.Scripts.Components.HudObjectNamespace.Objects
{
    internal class Text : HudObject
    {
        protected string _text;
        private bool _centered;
        protected SpriteFont _font;

        public Text(string text = "", Vector2 position = new(), float scale = 1, bool hidden = false, bool centered = false) : base(position, scale, hidden)
        {
            _text = text;
            _centered = centered;
            _font = Main.Content.Load<SpriteFont>(SPRITES.ARIAL_FONT);
        }

        public Text(SpriteFont font, Vector2 position = new(), float scale = 1, bool hidden = false, bool centered = false) : base(position, scale, hidden)
        {
            _text = "";
            _centered = centered;
            _font = font;
        }

        public Text(string text, SpriteFont font, Vector2 position = new(), float scale = 1, bool hidden = false, bool centered = false) : base(position, scale, hidden)
        {
            _text = text;
            _centered = centered;
            _font = font;
        }

        /// <summary>
        /// Defini la texture du HudObject
        /// </summary>
        /// <param name="texture2D"></param>
        protected override void setTexture2D(ref Texture2D texture2D)
        {
            texture2D = new Texture2D(Main.GraphicsDevice, (int)Size.X, (int)Size.Y);
        }

        protected override void Draw(GameTime gameTime, ref SpriteBatch spriteBatch)
        {
            if (_centered)
            {
                try
                {
                    Vector2 size = _font.MeasureString(_text) * Scale;

                    spriteBatch.DrawString(_font, _text, Position - size / 2, Color.Black, Rotation, new Vector2(0, 0), Scale, SpriteEffects.None, 0);
                }
                catch (Exception)
                {
                    throw new Exception("Un string centrer ne peut pas contenir des caractère spéciaux");
                }
            }
            else
            {
                spriteBatch.DrawString(_font, _text, Position, Color.Black, Rotation, new Vector2(0, 0), Scale, SpriteEffects.None, 0);
            }
        }

        /// <summary>
        /// Defini le texte de l'element
        /// </summary>
        public void setText(string text)
        {
            _text = text;
        }

        /// <summary>
        /// Permet de recuperer le texte de l'element
        /// </summary>
        public string getText()
        {
            return _text;
        }
    }
}
