using ModularAbyss.Scripts.Components.ItemNamespace.Items.Weapons;
using ModularAbyss.Scripts.Components.ItemNamespace;
using ModularAbyss.Scripts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModularAbyss.Scripts.Components.StatNamespace.StatsNamespace;

namespace ModularAbyss.Scripts.Structures
{
    public struct STATS
    {
        //Equivalent d'un getter
        public static Stat HEALTH => new Stat()
        {
            Id = STATS_ID.HEALTH,
            Name = "Health",
            Value = 10,
            MAX_VALUE = 10,
        };

        public static Stat ARMOR => new Stat()
        {
            Id = STATS_ID.ARMOR,
            Name = "Armor",
            Value = 0,
        };

        public static Stat SPEED => new Stat()
        {
            Id = STATS_ID.SPEED,
            Name = "Speed",
            Value = 1,
        };

        public static Stat DAMAGE => new Stat()
        {
            Id = STATS_ID.DAMAGE,
            Name = "Damage",
            Value = 1,
        };
    }
}
