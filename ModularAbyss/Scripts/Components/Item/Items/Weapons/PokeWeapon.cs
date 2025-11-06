using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ModularAbyss.Scripts.Components.GameObjectNamespace;
using ModularAbyss.Scripts.Components.GameObjectNamespace.Objects;
using ModularAbyss.Scripts.Components.GameObjectNamespace.Objects.Entity;
using ModularAbyss.Scripts.Components.GameObjectNamespace.Objects.Entity.Enemy;
using ModularAbyss.Scripts.Components.ItemNamespace.Items;
using ModularAbyss.Scripts.Enums;
using ModularAbyss.Scripts.Interfaces;
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


        protected override void Update(GameTime gameTime, List<ICollider> colliders)
        {
            base.Update(gameTime, colliders);

            PlayAttackAnimation(gameTime);
        }

        /// <summary>
        /// L'attaque par defaut
        /// </summary>
        protected override void _attack()
        {
            // -----------------
            // L'attaque de base
            // -----------------
            asAttacked = true;

            foreach (GameObject gameObject in Main.CollidersObjects)
            {
                // Detecte si le joueur touche un Collider avec son PokeWeapon
                if (CollisionRectangle.Intersects(gameObject.CollisionRect))
                {
                    // Si c'est un Enemi
                    if (typeof(Enemy).IsAssignableFrom(gameObject.GetType()))
                    {
                        HurtEntity(gameObject as Entity);
                    }
                }
            }
        }

        private void PlayAttackAnimation(GameTime gameTime)
        {
            if (asAttacked)
            {
                itemOffset += 1 * gameTime.ElapsedGameTime.Milliseconds;
            }
        }
    }
}
