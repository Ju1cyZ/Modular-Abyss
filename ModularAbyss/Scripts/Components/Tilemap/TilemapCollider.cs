using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ModularAbyss.Scripts.Components.TilemapNamespace.TilesetNamespace;
using ModularAbyss.Scripts.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularAbyss.Scripts.Components.TilemapNamespace
{
    internal class TilemapCollider : Tilemap
    {
        public List<Rectangle> Colliders = new List<Rectangle>();

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
    }
}
