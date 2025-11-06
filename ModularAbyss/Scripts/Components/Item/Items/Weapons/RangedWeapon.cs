using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ModularAbyss.Scripts.Components.GameObjectNamespace.Objects;
using ModularAbyss.Scripts.Components.GameObjectNamespace.Objects.Entity;
using ModularAbyss.Scripts.Components.ItemNamespace.Items;
using ModularAbyss.Scripts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularAbyss.Scripts.Components.ItemNamespace.Items.Weapons
{
    internal class RangedWeapon : Weapon
    {
        public RangedWeapon() { }

        public RangedWeapon(string id, string name, string description, ItemTier itemTier, string texturePath, Rectangle spriteRectangle) : base(id, name, description, itemTier, texturePath, spriteRectangle) { }

        /// <summary>
        /// L'attaque par defaut
        /// </summary>
        protected override void _attack()
        {
            // -----------------
            // L'attaque de base
            // -----------------
        }
    }
}
