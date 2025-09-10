using Microsoft.Xna.Framework.Graphics;
using ModularAbyss.Scripts.Components.TilemapNamespace.TilesetNamespace;

namespace ModularAbyss.Scripts.Structures
{
    public struct TILESET
    {
        public static Tileset FLOOR => new Tileset(16, 4, 4)
        {
            texture = Main.getInstance().Content.Load<Texture2D>(SPRITES.FLOOR_TILESET)
        };

        public static Tileset WALL => new Tileset(16, 6, 4)
        {
            texture = Main.getInstance().Content.Load<Texture2D>(SPRITES.WALL_TILESET),
            defaultTileId = 11,
        };

    }
}
