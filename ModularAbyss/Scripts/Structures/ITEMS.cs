using Microsoft.Xna.Framework;
using ModularAbyss.Scripts.Components.ItemNamespace;
using ModularAbyss.Scripts.Components.ItemNamespace.Items;
using ModularAbyss.Scripts.Components.ItemNamespace.Items.Weapons;
using ModularAbyss.Scripts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ModularAbyss.Scripts.Structures
{
    public struct ITEMS
    {
        //Equivalent d'un getter
        public static Item STEEL_SWORD => new SwingWeapon()
        {
            Id = "STEEL_SWORD",
            Name = "Basic Sword",
            Description = "MA_DESCRIPTION",
            ItemTier = ItemTier.Common,
            Texture2D = SPRITES.WEAPON_HAND,
            SpriteRectangle = new Rectangle(0, 0, 13, 21)
        };
    }
}
