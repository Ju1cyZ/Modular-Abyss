using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularAbyss.Scripts.Structures
{
    public struct MOUSE_ID
    {
        /*
         * Permet de recuperer les rectangles adapter type de souris
         * Les types de souris son pris de "SPRITES.MOUSE_ICONS"
         */

        private const int ICON_SIZE = 16;

        public static Rectangle HAND_FINGER => new Rectangle()
        {
            X = ICON_SIZE * 1,
            Y = ICON_SIZE * 0,
            Size = new Point(ICON_SIZE, ICON_SIZE)
        };

        public static Rectangle HAND_FINGER_CLICKED => new Rectangle()
        {
            X = ICON_SIZE * 0,
            Y = ICON_SIZE * 0,
            Size = new Point(ICON_SIZE, ICON_SIZE)
        };

        public static Rectangle HAND_GRAB => new Rectangle()
        {
            X = ICON_SIZE * 2,
            Y = ICON_SIZE * 0,
            Size = new Point(ICON_SIZE, ICON_SIZE)
        };

    }
}
