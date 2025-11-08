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
using ModularAbyss.Scripts.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularAbyss.Scripts.Components.ItemNamespace.Items.Weapons
{
    internal class PokeWeapon : Weapon
    {
        private bool playingAnimation = false;

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
            if (!asAttacked)
            {
                // -----------------
                // L'attaque de base
                // -----------------
                
                playingAnimation = true;

                foreach (GameObject gameObject in Main.ActiveScene.CollidersObjects)
                {
                    // Detecte si le joueur touche un Collider avec son PokeWeapon
                    if (CollisionRectangle.Intersects(gameObject.CollisionRect))
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
                if (itemOffset == MIN_ITEM_OFFSET)
                {
                    asAttacked = false;
                }
            }
        }
    }
}
