using ModularAbyss.Scripts.Components.Gameobjects.Entities;
using ModularAbyss.Scripts.Components.Gameobjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using ModularAbyss.Scripts.Interfaces;
using ModularAbyss.Scripts.Components.Gameobjects.Projectiles;
using ModularAbyss.Scripts.Core;
using ModularAbyss.Scripts.Structures;

namespace ModularAbyss.Scripts.Components.Items.Weapons.RangedWeapons
{
    public class WandWeapon : RangedWeapon
    {
        private bool playingAnimation = false;

        public WandWeapon()
        {
            MIN_ITEM_OFFSET = 50;
            MAX_ITEM_OFFSET = 90;

            itemOffset = 90;
        }

        protected override void Update(GameTime gameTime, List<ICollider> colliders)
        {
            base.Update(gameTime, colliders);

            PlayAttackAnimation(gameTime);
        }

        protected override void _attack()
        {
            if (!asAttacked)
            {
                // -----------------
                // L'attaque de base
                // -----------------


                //La position central du joueur (sprite)
                Vector2 characterPos = new Vector2(Main.ActiveScene.Player.Position.X + 160 + 80 / 2, Main.ActiveScene.Player.Position.Y + 160 + 80 / 2);

                //L'angle de rotation depuis le joueur vers la souris
                double angleRadians = GameManager.AngleBetweenVectors(characterPos, InputManager.MouseObject.mState.Position.ToVector2());

                //La position de l'item selon la rotation
                Vector2 offsetPos = new Vector2((float)Math.Cos(angleRadians) * itemOffset, (float)Math.Sin(angleRadians) * itemOffset);

                //La position final de l'item
                Vector2 itemPos = characterPos + offsetPos;

                new Fireball(InputManager.MouseObject.mState.Position.ToVector2(), Main.ActiveScene.Player.Position);

                playingAnimation = true;

                asAttacked = true;
            }
        }

        private void PlayAttackAnimation(GameTime gameTime)
        {
            if (playingAnimation)
            {
                itemOffset -= 1 * gameTime.ElapsedGameTime.Milliseconds;
                if (itemOffset <= MIN_ITEM_OFFSET) // Verifie si l'arme a parcourus tous le "chemin"
                {
                    //Defini asAttacked apres 200 Millisecondes
                    Task.Delay(200).ContinueWith(t =>
                    {
                        playingAnimation = false;
                    });
                }
            }

            if (!playingAnimation && itemOffset != MAX_ITEM_OFFSET) // Permet de remettre l'arme a zero
            {
                itemOffset += 1 * gameTime.ElapsedGameTime.Milliseconds;
                if (itemOffset == MAX_ITEM_OFFSET)
                {
                    itemOffset = MAX_ITEM_OFFSET;
                    asAttacked = false;
                }
            }
        }
    }
}
