using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ModularAbyss.Scripts.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularAbyss.Scripts.Components.HudObjectNamespace.Objects
{
    internal class Text : HudObject
    {
        protected string _text;
        protected SpriteFont _font;

        public Text(string text = "", bool hidden = false) : base (hidden)
        {
            _text = text;
            _font = Main.Content.Load<SpriteFont>(SPRITES.ARIAL_FONT);
        }

        public Text(SpriteFont font, bool hidden = false) : base(hidden)
        {
            _text = "";
            _font = font;
        }

        public Text(string text, SpriteFont font, bool hidden = false) : base(hidden)
        {
            _text = text;
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
            spriteBatch.DrawString(_font, _text, Position, Color.Black, Rotation, new Vector2(0,0), Scale, SpriteEffects.None, 0);
        }

        /// <summary>
        /// Defini le texte de l'element
        /// </summary>
        public void setText(string text)
        {
            _text = text;
        }
    }
}
