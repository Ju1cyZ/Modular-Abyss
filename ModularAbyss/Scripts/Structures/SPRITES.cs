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

        /* ENEMIES */
        public const string SLIME_IDLE = ASSETS_FOLDER + "Enemies/Slime/Slime1_Idle_full";

        /* HUD */
        public const string BARS = ASSETS_FOLDER + "HUD/bars";

        /* ITEMS */
        public const string WEAPON_HAND = ASSETS_FOLDER + "Weapons/hand";

        /* TILESETS */
        public const string FLOOR_TILESET = ASSETS_FOLDER + "Tilesets/floor";
        public const string WALL_TILESET = ASSETS_FOLDER + "Tilesets/wall";

        /* FONTS */
    }
}
