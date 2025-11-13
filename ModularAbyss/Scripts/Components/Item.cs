using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ModularAbyss.Scripts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularAbyss.Scripts.Components
{
    public abstract class Item
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ItemTier ItemTier { get; set; }

        private Texture2D _texture2D;
        public string Texture2D
        {
            set
            {
                _texture2D = Main.getInstance().Content.Load<Texture2D>(value);
            }

        }
        public Rectangle SpriteRectangle { get; set; }

        public Item() { }

        public Item(string id, string name, string description, ItemTier itemTier, string texturePath, Rectangle spriteRectangle)
        {
            Id = id;
            Name = name;
            Description = description;
            ItemTier = itemTier;
            SpriteRectangle = spriteRectangle;
            Texture2D = texturePath;
        }

        public Texture2D GetTexture2D()
        {
            return _texture2D;
        }
    }
}
