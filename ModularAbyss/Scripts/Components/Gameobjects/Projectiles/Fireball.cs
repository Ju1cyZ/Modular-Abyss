using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ModularAbyss.Scripts.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularAbyss.Scripts.Components.Gameobjects.Projectiles
{
    public class Fireball : Projectile
    {
        public Fireball(Vector2 target, Vector2 startPos) : base(target, startPos) { }

        public override void SetColliderBounds()
        {
            Bounds = new Rectangle(new Point((int)Position.X, (int)Position.Y), new Point(13, 24));
        }

        public override void setTextureRectangle(ref Rectangle rect)
        {
            rect = new Rectangle(new Point(16 * 16, 13 * 16), new Point(32, 16));
        }

        protected override void setTexture2D(ref Texture2D texture2D)
        {
            texture2D = Main.Content.Load<Texture2D>(SPRITES.FIRE_BULLETS_BLUE);
        }
    }
}
