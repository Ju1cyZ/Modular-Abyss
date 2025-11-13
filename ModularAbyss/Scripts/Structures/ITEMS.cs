using Microsoft.Xna.Framework;
using ModularAbyss.Scripts.Components;
using ModularAbyss.Scripts.Components.Items.Weapons.MeleeWeapons;
using ModularAbyss.Scripts.Enums;

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
            SpriteRectangle = new Rectangle(0, 0, 13, 21), // La position de la texture dans le spritesheet
            Damage = 5,
        };

        public static Item STEEL_KNIFE => new PokeWeapon()
        {
            Id = "STEEL_KNIFE",
            Name = "Basic Knife",
            Description = "MA_DESCRIPTION",
            ItemTier = ItemTier.Common,
            Texture2D = SPRITES.WEAPON_HAND,
            SpriteRectangle = new Rectangle(40, 0, 7, 20),
            Damage = 3,
        };
    }
}
