using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ModularAbyss.Scripts.Interfaces;
using ModularAbyss.Scripts.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularAbyss.Scripts.Components.Tilemaps
{
    public class TilemapCollider : Tilemap, ICollider
    {
        public List<Rectangle> Colliders = new List<Rectangle>();
        
        public Rectangle Bounds { 
            get => throw new NotImplementedException(); 
            set => throw new NotImplementedException(); 
        }

        public TilemapCollider()
        {

        }

        public TilemapCollider(Tileset tileset, int width, int height) : base(tileset, width, height)
        {

        }

        public TilemapCollider(Tileset tileset, int width, int height, int[,] tilesIds) : base(tileset, width, height, tilesIds)
        {

        }


        public override void Draw(GameTime gameTime, ref SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, ref spriteBatch);
            if (GLOBAL.DEBUGING)
            {
                foreach (Rectangle collider in Colliders)
                {
                    Texture2D _texture = new Texture2D(Main.getInstance().GraphicsDevice, 1, 1);
                    _texture.SetData(new Color[] { Color.Red });
                    spriteBatch.Draw(_texture, new Vector2(collider.X, collider.Y), collider, Color.White);
                }

            }

        }

        public void SetColliderBounds()
        {
            for (int X = 0; X < _tilesIds.GetLength(0); X++)
            {
                for (int Y = 0; Y < _tilesIds.GetLength(1); Y++)
                {
                    if (_tilesIds[X, Y] != -1)
                    {
                        Colliders.Add(new Rectangle(new Point(160 * X, 160 * Y), new Point(160, 160)));
                    }
                }
            }
        }

        /// <summary>
        /// Permet de creer un tileset a partir d'un string
        /// </summary>
        /// <param name="CSVData"></param>
        public override void LoadFromCSV(string CSVData)
        {
            base.LoadFromCSV(CSVData);
            SetColliderBounds();
        }
    }
}
