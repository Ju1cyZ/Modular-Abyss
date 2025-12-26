using Microsoft.Xna.Framework.Graphics;
using ModularAbyss.Scripts.Components;

namespace ModularAbyss.Scripts.Structures
{
    public struct TILESET
    {
        public static Tileset FLOOR => new Tileset(16, 16, 16)
        {
            texture = Main.getInstance().Content.Load<Texture2D>(SPRITES.GRASS_TILESET)
        };

        public static Tileset WALL => new Tileset(16, 32, 32)
        {
            texture = Main.getInstance().Content.Load<Texture2D>(SPRITES.WALL_TILESET),
            defaultTileId = 0,
        };

    }
}
