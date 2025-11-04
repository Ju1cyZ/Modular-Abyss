using Microsoft.Xna.Framework;
using ModularAbyss.Scripts.Components.GameObjectNamespace;
using ModularAbyss.Scripts.Components.GameObjectNamespace.Objects.Entity;
using ModularAbyss.Scripts.Components.GameObjectNamespace.Objects.Entity.Enemy;
using ModularAbyss.Scripts.Components.ItemNamespace.Items;
using ModularAbyss.Scripts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularAbyss.Scripts.Components.ItemNamespace.Items.Weapons
{
    internal class PokeWeapon : Weapon
    {
        public PokeWeapon() { }

        public PokeWeapon(string id, string name, string description, ItemTier itemTier, string texturePath, Rectangle spriteRectangle) : base(id, name, description, itemTier, texturePath, spriteRectangle) { }


        /// <summary>
        /// L'attaque par defaut
        /// </summary>
        protected override void _attack()
        {
            // -----------------
            // L'attaque de base
            // -----------------

            PlayAttackAnimation();

            if (CollisionRectangle.Intersects())
            {
                foreach (GameObject gameObject in Main.CollidersObjects)
                {
                    //Detecte si le joueur touche un Collider avec son SwingWeapon
                    if (CollisionRectangle.Intersects(gameObject.CollisionRect))
                    {
                        //Si c'est un Enemi
                        if (typeof(Enemy).IsAssignableFrom(gameObject.GetType()) && !asAttacked)
                        {

                        }
                    }
                }
            }
        }

        private void PlayAttackAnimation()
        {

        }
    }
}
