using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularAbyss.Scripts.Structures
{
    public struct UI_COMPONENTS
    {
        /*
         * Permet de recuperer les rectangles adapter type de souris
         * Les types de souris son pris de "SPRITES.MOUSE_ICONS"
         */

        public const int SQUARE_SIZE = 16;

        public static Rectangle BUTTON => new Rectangle()
        {
            X = SQUARE_SIZE * 0,
            Y = SQUARE_SIZE * 6,
            Size = new Point(SQUARE_SIZE * 3, SQUARE_SIZE)
        };

        public static Rectangle BUTTON_HOVER => new Rectangle()
        {
            X = SQUARE_SIZE * 0,
            Y = SQUARE_SIZE * 7,
            Size = new Point(SQUARE_SIZE * 3, SQUARE_SIZE)
        };

        /// <summary>
        /// Permet de recuprer le panel en morceau de la forme suivante :
        /// <code>
        ///     [0][1][2]
        ///     [3][4][5]
        ///     [6][7][8]
        /// </code>
        /// </summary>
        public static Rectangle[] BOOK_PANEL => new Rectangle[]
        {
            new Rectangle() { X = SQUARE_SIZE * 0, Y = SQUARE_SIZE * 0, Size = new Point(SQUARE_SIZE * 2, SQUARE_SIZE * 2)},
            new Rectangle() { X = SQUARE_SIZE * 2, Y = SQUARE_SIZE * 0, Size = new Point(SQUARE_SIZE * 2, SQUARE_SIZE * 2)},
            new Rectangle() { X = SQUARE_SIZE * 4, Y = SQUARE_SIZE * 0, Size = new Point(SQUARE_SIZE * 2, SQUARE_SIZE * 2)},

            new Rectangle() { X = SQUARE_SIZE * 0, Y = SQUARE_SIZE * 2, Size = new Point(SQUARE_SIZE * 2, SQUARE_SIZE * 2)},
            new Rectangle() { X = SQUARE_SIZE * 2, Y = SQUARE_SIZE * 2, Size = new Point(SQUARE_SIZE * 2, SQUARE_SIZE * 2)},
            new Rectangle() { X = SQUARE_SIZE * 4, Y = SQUARE_SIZE * 2, Size = new Point(SQUARE_SIZE * 2, SQUARE_SIZE * 2)},
            
            new Rectangle() { X = SQUARE_SIZE * 0, Y = SQUARE_SIZE * 4, Size = new Point(SQUARE_SIZE * 2, SQUARE_SIZE * 2)},
            new Rectangle() { X = SQUARE_SIZE * 2, Y = SQUARE_SIZE * 4, Size = new Point(SQUARE_SIZE * 2, SQUARE_SIZE * 2)},
            new Rectangle() { X = SQUARE_SIZE * 4, Y = SQUARE_SIZE * 4, Size = new Point(SQUARE_SIZE * 2, SQUARE_SIZE * 2)},
        };

        /// <summary>
        /// Permet de recuprer le panel en morceau de la forme suivante :
        /// <code>
        ///     [0][1][2]
        ///     [3][4][5]
        ///     [6][7][8]
        /// </code>
        /// </summary>
        public static Rectangle[] BOOK_PAPER_LEFT_PANEL => new Rectangle[]
        {
            new Rectangle() { X = SQUARE_SIZE * 6 , Y = SQUARE_SIZE * 0, Size = new Point(SQUARE_SIZE * 2, SQUARE_SIZE * 2)},
            new Rectangle() { X = SQUARE_SIZE * 8 , Y = SQUARE_SIZE * 0, Size = new Point(SQUARE_SIZE * 2, SQUARE_SIZE * 2)},
            new Rectangle() { X = SQUARE_SIZE * 10, Y = SQUARE_SIZE * 0, Size = new Point(SQUARE_SIZE * 2, SQUARE_SIZE * 2)},

            new Rectangle() { X = SQUARE_SIZE * 6 , Y = SQUARE_SIZE * 2, Size = new Point(SQUARE_SIZE * 2, SQUARE_SIZE * 2)},
            new Rectangle() { X = SQUARE_SIZE * 8 , Y = SQUARE_SIZE * 2, Size = new Point(SQUARE_SIZE * 2, SQUARE_SIZE * 2)},
            new Rectangle() { X = SQUARE_SIZE * 10, Y = SQUARE_SIZE * 2, Size = new Point(SQUARE_SIZE * 2, SQUARE_SIZE * 2)},

            new Rectangle() { X = SQUARE_SIZE * 6 , Y = SQUARE_SIZE * 4, Size = new Point(SQUARE_SIZE * 2, SQUARE_SIZE * 2)},
            new Rectangle() { X = SQUARE_SIZE * 8 , Y = SQUARE_SIZE * 4, Size = new Point(SQUARE_SIZE * 2, SQUARE_SIZE * 2)},
            new Rectangle() { X = SQUARE_SIZE * 10, Y = SQUARE_SIZE * 4, Size = new Point(SQUARE_SIZE * 2, SQUARE_SIZE * 2)},
        };

        /// <summary>
        /// Permet de recuprer le panel en morceau de la forme suivante :
        /// <code>
        ///     [0][1][2]
        ///     [3][4][5]
        ///     [6][7][8]
        /// </code>
        /// </summary>
        public static Rectangle[] BOOK_PAPER_RIGHT_PANEL => new Rectangle[]
        {
            new Rectangle() { X = SQUARE_SIZE * 12, Y = SQUARE_SIZE * 0, Size = new Point(SQUARE_SIZE * 2, SQUARE_SIZE * 2)},
            new Rectangle() { X = SQUARE_SIZE * 14, Y = SQUARE_SIZE * 0, Size = new Point(SQUARE_SIZE * 2, SQUARE_SIZE * 2)},
            new Rectangle() { X = SQUARE_SIZE * 16, Y = SQUARE_SIZE * 0, Size = new Point(SQUARE_SIZE * 2, SQUARE_SIZE * 2)},

            new Rectangle() { X = SQUARE_SIZE * 12, Y = SQUARE_SIZE * 2, Size = new Point(SQUARE_SIZE * 2, SQUARE_SIZE * 2)},
            new Rectangle() { X = SQUARE_SIZE * 14, Y = SQUARE_SIZE * 2, Size = new Point(SQUARE_SIZE * 2, SQUARE_SIZE * 2)},
            new Rectangle() { X = SQUARE_SIZE * 16, Y = SQUARE_SIZE * 2, Size = new Point(SQUARE_SIZE * 2, SQUARE_SIZE * 2)},

            new Rectangle() { X = SQUARE_SIZE * 12, Y = SQUARE_SIZE * 4, Size = new Point(SQUARE_SIZE * 2, SQUARE_SIZE * 2)},
            new Rectangle() { X = SQUARE_SIZE * 14, Y = SQUARE_SIZE * 4, Size = new Point(SQUARE_SIZE * 2, SQUARE_SIZE * 2)},
            new Rectangle() { X = SQUARE_SIZE * 16, Y = SQUARE_SIZE * 4, Size = new Point(SQUARE_SIZE * 2, SQUARE_SIZE * 2)},
        };
    }
}
