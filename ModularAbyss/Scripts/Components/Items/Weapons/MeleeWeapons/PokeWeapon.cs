using Microsoft.Xna.Framework;
using ModularAbyss.Scripts.Components.Gameobjects.Entities;
using ModularAbyss.Scripts.Components.Gameobjects;
using ModularAbyss.Scripts.Enums;
using ModularAbyss.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularAbyss.Scripts.Components.Items.Weapons.MeleeWeapons
{
    public class PokeWeapon : MeleeWeapon
    {
        private bool playingAnimation = false;

        public PokeWeapon() { }

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
            if (!asAttacked)
            {
                // -----------------
                // L'attaque de base
                // -----------------

                playingAnimation = true;

                foreach (GameObject gameObject in Main.ActiveScene.CollidersObjects)
                {
                    // Detecte si le joueur touche un Collider avec son PokeWeapon
                    if (CollisionRectangle.Intersects(gameObject.Bounds))
                    {
                        // Si c'est un Enemi
                        if (typeof(Enemy).IsAssignableFrom(gameObject.GetType()))
                        {
                            // On empeche d'infiliger plusieur fois les degats
                            asAttacked = true;
                            HurtEntity(gameObject as Entity);
                        }
                    }
                }
            }
        }

        private void PlayAttackAnimation(GameTime gameTime)
        {
            if (playingAnimation)
            {
                itemOffset += 1 * gameTime.ElapsedGameTime.Milliseconds;
                if (itemOffset == MAX_ITEM_OFFSET) // Verifie si l'arme a parcourus tous le "chemin"
                {
                    //Defini asAttacked apres 200 Millisecondes
                    Task.Delay(200).ContinueWith(t =>
                    {
                        playingAnimation = false;
                    });
                }
            }

            if (!playingAnimation && itemOffset != MIN_ITEM_OFFSET) // Permet de remettre l'arme a zero
            {
                itemOffset -= 1 * gameTime.ElapsedGameTime.Milliseconds;
                if (itemOffset <= MIN_ITEM_OFFSET)
                {
                    itemOffset = MIN_ITEM_OFFSET;
                    asAttacked = false;
                }
            }
        }
    }
}
    