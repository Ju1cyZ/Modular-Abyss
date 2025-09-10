using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularAbyss.Scripts.Components.TilemapNamespace.TilesetNamespace
{
    public class Tileset
    {
        public int TILE_SIZE;
        public int TILE_WIDTH_COUNT;
        public int TILE_HEIGHT_COUNT;

        public int defaultTileId = 0;

        private List<Rectangle> tiles;
        public Texture2D texture;

        public Tileset(int tileSize, int tileWithCount, int tileHeightCount) 
        { 
            TILE_SIZE = tileSize;
            TILE_HEIGHT_COUNT = tileHeightCount;
            TILE_WIDTH_COUNT = tileWithCount;

            tiles = new List<Rectangle>();

            for (int Y = 0; Y < TILE_HEIGHT_COUNT; Y++)
            {
                for (int X = 0; X < TILE_WIDTH_COUNT; X++)
                {
                    tiles.Add(new Rectangle(X * TILE_SIZE, Y * TILE_SIZE, TILE_SIZE, TILE_SIZE));
                }
            }
        }

        /// <summary>
        /// Recuperer le rectange de l'id
        /// </summary>
        /// <param name="id">L'id du tile</param>
        /// <returns>Le rectangle</returns>
        public Rectangle getTile(int id)
        {
            if (tiles.Count - 1 >= id && id >= 0)
            {
                return tiles[id];
            }
            return tiles[defaultTileId];
        }

        /// <summary>
        /// Permet de recuperer la texture 2D du Tileset
        /// </summary>
        /// <returns></returns>
        public Texture2D GetTexture2D()
        {
            return texture;
        }
    }
}
