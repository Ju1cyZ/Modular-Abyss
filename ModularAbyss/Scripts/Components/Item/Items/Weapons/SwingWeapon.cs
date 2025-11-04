using Microsoft.Xna.Framework;
using ModularAbyss.Scripts.Components.GameObjectNamespace;
using ModularAbyss.Scripts.Components.GameObjectNamespace.Objects.Entity.Enemy;
using ModularAbyss.Scripts.Components.GameObjectNamespace.Objects;
using ModularAbyss.Scripts.Enums;
using System.Collections.Generic;
using ModularAbyss.Scripts.Components.GameObjectNamespace.Objects.Entity;

namespace ModularAbyss.Scripts.Components.ItemNamespace.Items.Weapons
{
    internal class SwingWeapon : Weapon
    {

        public SwingWeapon()
        {
        }

        public SwingWeapon(string id, string name, string description, ItemTier itemTier, string texturePath, Rectangle spriteRectangle) : base(id, name, description, itemTier, texturePath, spriteRectangle) { }

        protected override void Update(GameTime gameTime, List<Interfaces.ICollider> colliders)
        {
            foreach (GameObject gameObject in Main.CollidersObjects)
            {
                //Detecte si le joueur touche un Collider avec son SwingWeapon
                if (CollisionRectangle.Intersects(gameObject.CollisionRect))
                {
                    //Si c'est un Enemi
                    if (typeof(Enemy).IsAssignableFrom(gameObject.GetType()) && !asAttacked)
                    {
                        Attack();
                        //On empeche d'infiliger plusieur fois les degats
                        asAttacked = true;

                        //On defini la derniere entiter attaquer
                        lastAttackedEntity = gameObject as Entity;
                        //Inflige les degats a l'entiter
                        (gameObject as Enemy).HurtWithStats(Damage, Main.getInstance().Player);
                    }
                }

                if (asAttacked && lastAttackedEntity != null && !CollisionRectangle.Intersects(lastAttackedEntity.CollisionRect))
                {
                    asAttacked = false;
                }
            }
        }

        /// <summary>
        /// L'attaque par defaut
        /// </summary>
        protected override void _attack()
        {
            // -----------------
            //L'attaque de base
            // -----------------
        }
    }
}
