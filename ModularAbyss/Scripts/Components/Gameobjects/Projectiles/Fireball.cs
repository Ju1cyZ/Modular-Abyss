using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ModularAbyss.Scripts.Interfaces;
using ModularAbyss.Scripts.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ModularAbyss.Scripts.Components.Gameobjects.Projectiles
{
    public class Fireball : Projectile
    {
        public Fireball(Vector2 target, Vector2 startPos) : base(target, startPos) { }

        protected override void OnTwoGameObjectsCollision(ICollider object1, ICollider object2)
        {
            // Verifie si c'est la collision de l'object qui a ete toucher
            if (object1 != this && object2 != this)
            {
                return;
            }

            ICollider gameObjectTouched;

            // On determine lequelle des deux objet est la cible
            if (object1 == this)
            {
                gameObjectTouched = object2;
            }
            else
            {
                gameObjectTouched = object1;
            }

            // On desactive les colisions entre projectile
            if (!typeof(Projectile).IsAssignableFrom(gameObjectTouched.GetType())) 
            {

                Touch(this, gameObjectTouched);
            }
            
        }
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
