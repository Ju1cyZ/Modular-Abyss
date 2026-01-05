using ModularAbyss.Scripts.Components.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularAbyss.Scripts.Structures
{
    public struct SPRITES
    {
        private const string ASSETS_FOLDER = "Assets/";
        public const string PLAYER_IDLE = ASSETS_FOLDER + "Player/idle";
        public const string PLAYER_WALK = ASSETS_FOLDER + "Player/walk";
        public const string PLAYER_RUN = ASSETS_FOLDER + "Player/run";
        public const string PLAYER_HIT = ASSETS_FOLDER + "Player/hurt";
        public const string PLAYER_DEATH = ASSETS_FOLDER + "Player/death";

        /* ENEMIES */
        public const string SLIME_IDLE = ASSETS_FOLDER + "Enemies/Slime/Slime1_Idle_full";

        /* HUD */
        public const string BARS = ASSETS_FOLDER + "HUD/bars";
        public const string MOUSE_ICONS = ASSETS_FOLDER + "HUD/MouseIcons";
        public const string UI = ASSETS_FOLDER + "HUD/UI";

        /* ITEMS */
        public const string WEAPON_HAND = ASSETS_FOLDER + "Weapons/hand";
        public const string WEAPON_WAND = ASSETS_FOLDER + "Weapons/wand";

        /* PROJECTILE */
        public const string PROJECTILES_FOLDER = "Weapons/Projectile/";

        public const string FIRE_BULLETS_ORANGE =       ASSETS_FOLDER + PROJECTILES_FOLDER + "All_Fire_Bullet_Pixel_16x16_00";
        public const string FIRE_BULLETS_RED =          ASSETS_FOLDER + PROJECTILES_FOLDER + "All_Fire_Bullet_Pixel_16x16_01";
        public const string FIRE_BULLETS_BLUE =         ASSETS_FOLDER + PROJECTILES_FOLDER + "All_Fire_Bullet_Pixel_16x16_02";
        public const string FIRE_BULLETS_GREEN =        ASSETS_FOLDER + PROJECTILES_FOLDER + "All_Fire_Bullet_Pixel_16x16_03";
        public const string FIRE_BULLETS_LIGHT_ORANGE = ASSETS_FOLDER + PROJECTILES_FOLDER + "All_Fire_Bullet_Pixel_16x16_04";
        public const string FIRE_BULLETS_PURPLE =       ASSETS_FOLDER + PROJECTILES_FOLDER + "All_Fire_Bullet_Pixel_16x16_05";
        public const string FIRE_BULLETS_LIGHT_PURPLE = ASSETS_FOLDER + PROJECTILES_FOLDER + "All_Fire_Bullet_Pixel_16x16_06";
        public const string FIRE_BULLETS_LIGHT_RED =    ASSETS_FOLDER + PROJECTILES_FOLDER + "All_Fire_Bullet_Pixel_16x16_07";

        /* TILESETS */
        public const string GRASS_TILESET = ASSETS_FOLDER + "Tilesets/Grass";
        public const string WALL_TILESET = ASSETS_FOLDER + "Tilesets/Wall";
        public const string STONE_GROUND_TILESET = ASSETS_FOLDER + "Tilesets/StoneGround";

        /* FONTS */
        public const string ARIAL_FONT = ASSETS_FOLDER + "Fonts/Arial";

    }
}
