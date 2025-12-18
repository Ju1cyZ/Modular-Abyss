using Microsoft.Xna.Framework;
using ModularAbyss.Scripts.Components.Gameobjects.Entities;
using ModularAbyss.Scripts.Components.Gameobjects;
using ModularAbyss.Scripts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularAbyss.Scripts.Components.Items.Weapons.MeleeWeapons
{
    public class SwingWeapon : MeleeWeapon
    {
        public SwingWeapon()
        {
            MIN_ITEM_OFFSET = 120;
            MAX_ITEM_OFFSET = 120;

            itemOffset = 120;
        }

        protected override void Update(GameTime gameTime)
        {
            // TODO : Adapter au nouveau system de detection de collision

            foreach (GameObject gameObject in Main.ActiveScene.CollidersObjects)
            {
                //Detecte si le joueur touche un Collider avec son SwingWeapon
                if (CollisionRectangle.Intersects(gameObject.Bounds))
                {
                    //Si c'est un Enemi
                    if (typeof(Enemy).IsAssignableFrom(gameObject.GetType()) && !asAttacked)
                    {
                        // On empeche d'infiliger plusieur fois les degats
                        asAttacked = true;

                        HurtEntity(gameObject as Entity);
                    }
                }

                if (asAttacked && lastAttackedEntity != null && !CollisionRectangle.Intersects(lastAttackedEntity.Bounds))
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
