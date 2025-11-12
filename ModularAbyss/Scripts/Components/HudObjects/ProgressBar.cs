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
    internal class ProgressBar : HudObject
    {
        private int _barID = 4;
        private int _progressBarSizeX = 48;
        private int _progressBarSizeY = 16;

        public int value;
        public int maxValue;
        public Vector2 Position = new Vector2();

        public ProgressBar(int progressBarSizeX, int progressBarSizeY, int barId, Vector2 position = new(), float scale = 1, bool hidden = false) : base(position, scale, hidden)
        {
            _progressBarSizeX = progressBarSizeX;
            _progressBarSizeY = progressBarSizeY;
            _barID = barId;
        }

        protected override void setTexture2D(ref Texture2D texture2D)
        {
            texture2D = Main.getInstance().Content.Load<Texture2D>(SPRITES.BARS);
        }

        protected override void Draw(GameTime gameTime, ref SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(GetTexture2D(), Position, getCorrectProgressBar(value, maxValue), Color.White, 0, new Vector2(0, 0), new Vector2(Scale, Scale), SpriteEffects.None, 0f);
        }

        /// <summary>
        /// Permet d'afficher la bar de vie qui correspond le plus a la vie de l'entiter
        /// </summary>
        /// <param name="health">La vie de l'entiter</param>
        /// <returns>La bar de vie selon le pourcentage de vie</returns>
        private Rectangle getCorrectProgressBar(int value, int maxValue)
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
                new Rectangle(_progressBarSizeX * 1, _progressBarSizeY * _barID, _progressBarSizeX, _progressBarSizeY),
                new Rectangle(_progressBarSizeX * 2, _progressBarSizeY * _barID, _progressBarSizeX, _progressBarSizeY),
                new Rectangle(_progressBarSizeX * 3, _progressBarSizeY * _barID, _progressBarSizeX, _progressBarSizeY),
                new Rectangle(_progressBarSizeX * 4, _progressBarSizeY * _barID, _progressBarSizeX, _progressBarSizeY),
                new Rectangle(_progressBarSizeX * 5, _progressBarSizeY * _barID, _progressBarSizeX, _progressBarSizeY),
                new Rectangle(_progressBarSizeX * 6, _progressBarSizeY * _barID, _progressBarSizeX, _progressBarSizeY),
            };

            if (value > maxValue * 0.8f)
            {
                return healthBarRectangles[0];
            }
            else if (value > maxValue * 0.6f)
            {
                return healthBarRectangles[1];
            }
            if (value > maxValue * 0.4f)
            {
                return healthBarRectangles[2];
            }
            if (value > maxValue * 0.2f)
            {
                return healthBarRectangles[3];
            }
            if (value > maxValue * 0f)
            {
                return healthBarRectangles[4];
            }
            else
            {
                return healthBarRectangles[5];
            }
        }
    }
}
