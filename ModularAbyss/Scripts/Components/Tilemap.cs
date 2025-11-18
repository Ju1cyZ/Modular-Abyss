using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModularAbyss.Scripts.Interfaces;

namespace ModularAbyss.Scripts.Components
{
    public class Tilemap : ICSVLoader
    {
        public int Width { get; }
        public int Height { get; }

        private Tileset _tileset;

        public int[,] _tilesIds;

        public Vector2 Position;

        public Tilemap() { }

        public Tilemap(Tileset tileset, int width, int height)
        {
            _tileset = tileset;
            Width = width;
            Height = height;
            _tilesIds = new int[Width, Height];
            if (tileset.defaultTileId != 0)
            {
                for (int Y = 0; Y < Height; Y++)
                {
                    for (int X = 0; X < Width; X++)
                    {
                        _tilesIds[X, Y] = tileset.defaultTileId;
                    }
                }
            }
        }

        public Tilemap(Tileset tileset, int width, int height, int[,] tilesIds)
        {
            _tileset = tileset;
            Width = width;
            Height = height;
            _tilesIds = tilesIds;
        }


        /// <summary>
        /// Permet de definir un tile spécifique
        /// </summary>
        /// <param name="tileId">L'id du tile</param>
        /// <param name="x">La pos X</param>
        /// <param name="y">La pos Y</param>
        public void SetTile(int tileId, int x, int y)
        {
            //Verification si le tileId existe

            _tilesIds[x, y] = tileId;
        }

        /// <summary>
        /// Permet de recuperer le tileId a un certain endroit
        /// </summary>
        /// <param name="x">La pos X</param>
        /// <param name="y">La pos Y</param>
        /// <returns></returns>
        public int GetTileId(int x, int y)
        {
            return _tilesIds[x, y];
        }

        /// <summary>
        /// Dessine le tilemap
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public virtual void Draw(GameTime gameTime, ref SpriteBatch spriteBatch)
        {
            for (int Y = 0; Y < Height; Y++)
            {
                for (int X = 0; X < Width; X++)
                {
                    spriteBatch.Draw(_tileset.GetTexture2D(), new Vector2(X * _tileset.TILE_SIZE * 10, Y * _tileset.TILE_SIZE * 10) + Position, _tileset.getTile(_tilesIds[X, Y]), Color.White, 0, Vector2.Zero, 10, SpriteEffects.None, 0);
                }
            }
        }
    }
}
