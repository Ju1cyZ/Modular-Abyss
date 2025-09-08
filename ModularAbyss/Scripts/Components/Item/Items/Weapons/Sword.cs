using Microsoft.Xna.Framework;
using ModularAbyss.Scripts.Components.ItemNamespace.Items;
using ModularAbyss.Scripts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularAbyss.Scripts.Components.ItemNamespace.Items.Weapons
{
    internal class Sword : Weapon
    {
        public Sword()
        {
        }

        public Sword(string name, string description, ItemTier itemTier, string texturePath, Rectangle spriteRectangle) : base(name, description, itemTier, texturePath, spriteRectangle)
        {

        }

        /// <summary>
        /// L'attaque par defaut de l'epee
        /// </summary>
        protected override void _attack()
        {
            //L'attaque de base pour l'epee
        }
    }
}
