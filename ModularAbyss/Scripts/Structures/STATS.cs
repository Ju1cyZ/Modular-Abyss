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
            Name = "Point de vie",
            Description = "Le nombre de point de vie",
            Value = 10,
            MAX_VALUE = 10,
        };

        public static Stat ARMOR => new Stat()
        {
            Id = STATS_ID.ARMOR,
            Name = "Point d'armure",
            Description = "Le nombre de point d'armure. \nPermet d'encaisser un certain nombre de dégats avant de diminuer les points de vies",
            Value = 0,
        };

        public static Stat SPEED => new Stat()
        {
            Id = STATS_ID.SPEED,
            Name = "Vitesse",
            Description = "La vitesse de déplacement.",
            Value = 1,
        };

        public static Stat DAMAGE => new Stat()
        {
            Id = STATS_ID.DAMAGE,
            Name = "Dégats",
            Description = "Les dégâts infligée à un enemi lors d'une attaque au corps à corps.",
            Value = 1,
        };
        public static Stat ATTACK_SPEED => new Stat()
        {
            Id = STATS_ID.ATTACK_SPEED,
            Name = "Le vitesse d'attaque",
            Description = "Diminue le temps nécéssaire entre les attaques",
            Value = 1,
        };

        public static Stat RANGED_DAMAGE => new Stat()
        {
            Id = STATS_ID.RANGED_DAMAGE,
            Name = "Dégâts de distance",
            Description = "Les dégâts infligée à un enumi lors d'une attaque à longue ou mi-distance.",
            Value = 1,
        };

        public static Stat LIFE_STEAL => new Stat()
        {
            Id = STATS_ID.LIFE_STEAL,
            Name = "Vole de vie",
            Description = "Augmente la probalitité de volée des points de vies a vos enemi lors d'une attaque",
            Value = 0,
        };
    }
}
