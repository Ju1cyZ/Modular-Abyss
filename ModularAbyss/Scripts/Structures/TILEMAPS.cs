using Microsoft.Xna.Framework.Graphics;
using ModularAbyss.Scripts.Components;

namespace ModularAbyss.Scripts.Structures
{
    public struct TILEMAPS
    {
        public static string TEST_FLOOR =>
            "0,1,1,2,0,2,0,1,1,2\r\n4,5,5,6,4,6,4,5,5,6\r\n4,5,5,6,4,6,4,5,5,6\r\n4,5,5,6,4,6,4,5,5,6\r\n8,9,9,10,8,10,8,9,9,10\r\n";

        public static string TEST_WALL =>
            "12,13,14,14,14,14,14,14,14,14,15,16\r" +
            "\n6,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,16\r" +
            "\n6,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,10\r" +
            "\n6,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,10\r" +
            "\n6,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,10\r" +
            "\n12,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,16\r" +
            "\n18,19,20,20,20,20,20,20,20,20,21,22\r\n";

        public static string LEVEL_1 => "";

    }
}
