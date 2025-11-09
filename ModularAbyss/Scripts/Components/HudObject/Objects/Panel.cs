using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ModularAbyss.Scripts.Structures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularAbyss.Scripts.Components.HudObjectNamespace.Objects
{
    internal class Panel : HudObject
    {
        Rectangle[] _panelType;

        public Panel(Rectangle[] panelType, Vector2 position = new(), Vector2 size = new(), float scale = 1, bool hidden = false) : base(position, scale, hidden)
        {
            // Taille minimum de 2x2
            if (size.X < 2)
                size.X = 2;
            if (size.Y < 2)
                size.Y = 2;

            _panelType = panelType;
            Size = size;
        }


        protected override void Draw(GameTime gameTime, ref SpriteBatch spriteBatch)
        {
            /*
             * Dessine le panel carre par carre
             *  1. ABC
             *  2. ABC
             *  3. ABC
             */

            #region Ligne 1

            // Colonne A
            spriteBatch.Draw(
                GetTexture2D(),
                Position + new Vector2(UI_COMPONENTS.SQUARE_SIZE * 0, 0) * Scale,
                _panelType[0],
                Color.White,
                0,
                new Vector2(0, 0),
                new Vector2(Scale, Scale),
                SpriteEffects.None,
                0f
            );

            // Colonnes B
            for (int X = 1; X <= Size.X - 2; X++)
            {
                spriteBatch.Draw(
                    GetTexture2D(),
                    Position + new Vector2(UI_COMPONENTS.SQUARE_SIZE * (X + 1), 0) * Scale,
                    _panelType[1],
                    Color.White,
                    0,
                    new Vector2(0, 0),
                    new Vector2(Scale, Scale),
                    SpriteEffects.None,
                    0f
                );
            }

            // Colonne C
            spriteBatch.Draw(
                GetTexture2D(),
                Position + new Vector2(UI_COMPONENTS.SQUARE_SIZE * Size.X - 1, 0) * Scale,
                _panelType[2],
                Color.White,
                0,
                new Vector2(0, 0),
                new Vector2(Scale, Scale),
                SpriteEffects.None,
                0f
            );

            #endregion

            #region Lignes 2

            for (int Y = 0; Y <= Size.Y - 2; Y++) // Ligne 2
            {
                // Colonne A
                spriteBatch.Draw(
                    GetTexture2D(),
                    Position + new Vector2(UI_COMPONENTS.SQUARE_SIZE * 0, UI_COMPONENTS.SQUARE_SIZE * (Y + 1)) * Scale,
                    _panelType[3],
                    Color.White,
                    0,
                    new Vector2(0, 0),
                    new Vector2(Scale, Scale),
                    SpriteEffects.None,
                    0f
                );

                // Colonnes B
                for (int X = 1; X <= Size.X - 2; X++)
                {
                    spriteBatch.Draw(
                        GetTexture2D(),
                        Position + new Vector2(UI_COMPONENTS.SQUARE_SIZE * (X + 1), UI_COMPONENTS.SQUARE_SIZE * (Y + 1)) * Scale,
                        _panelType[4],
                        Color.White,
                        0,
                        new Vector2(0, 0),
                        new Vector2(Scale, Scale),
                        SpriteEffects.None,
                        0f
                    );
                }

                // Colonne C
                spriteBatch.Draw(
                    GetTexture2D(),
                    Position + new Vector2(UI_COMPONENTS.SQUARE_SIZE * Size.X - 1, UI_COMPONENTS.SQUARE_SIZE * (Y + 1)) * Scale,
                    _panelType[5],
                    Color.White,
                    0,
                    new Vector2(0, 0),
                    new Vector2(Scale, Scale),
                    SpriteEffects.None,
                    0f
                );
            }

            #endregion

            #region Ligne 3

            // Colonne A
            spriteBatch.Draw(
                GetTexture2D(),
                Position + new Vector2(UI_COMPONENTS.SQUARE_SIZE * 0, UI_COMPONENTS.SQUARE_SIZE * Size.Y - 1) * Scale,
                _panelType[6],
                Color.White,
                0,
                new Vector2(0, 0),
                new Vector2(Scale, Scale),
                SpriteEffects.None,
                0f
            );

            // Colonnes B
            for (int X = 1; X <= Size.X - 2; X++)
            {
                spriteBatch.Draw(
                    GetTexture2D(),
                    Position + new Vector2(UI_COMPONENTS.SQUARE_SIZE * (X + 1), UI_COMPONENTS.SQUARE_SIZE * Size.Y - 1) * Scale,
                    _panelType[7],
                    Color.White,
                    0,
                    new Vector2(0, 0),
                    new Vector2(Scale, Scale),
                    SpriteEffects.None,
                    0f
                );
            }

            // Colonne C
            spriteBatch.Draw(
                GetTexture2D(),
                Position + new Vector2(UI_COMPONENTS.SQUARE_SIZE * Size.X - 1, UI_COMPONENTS.SQUARE_SIZE * Size.Y - 1) * Scale,
                _panelType[8],
                Color.White,
                0,
                new Vector2(0, 0),
                new Vector2(Scale, Scale),
                SpriteEffects.None,
                0f
            );

            #endregion

        }

        protected override void setTexture2D(ref Texture2D texture2D)
        {
            texture2D = Main.Content.Load<Texture2D>(SPRITES.UI);
        }
    }
}
