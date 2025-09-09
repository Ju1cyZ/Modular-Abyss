using ModularAbyss.Scripts.Components.StatNamespace.StatsNamespace;
using ModularAbyss.Scripts.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ModularAbyss.Scripts.Components.StatNamespace
{
    public class Stats
    {
        List<Stat> stats = new List<Stat>()
        {
            STATS.HEALTH,
            STATS.ARMOR,
            STATS.SPEED,
            STATS.DAMAGE,
        };

        public List<Stat> getStats()
        {
            return stats;
        }

        /// <summary>
        /// Permet de recuperer une stat a partir de l'id
        /// </summary>
        /// <param name="id">L'id de la stat</param>
        /// <returns>La Stat</returns>
        public Stat getStat(string id)
        {
            foreach (Stat stat in stats)
            {
                if (stat.Id == id)
                {
                    return stat;
                }
            }

            return new Stat();
        }

        /// <summary>
        /// Permet de changer la valeur d'une stat
        /// </summary>
        /// <param name="id">L'id de la stat a changee</param>
        /// <param name="value">La nouvelle valeur</param>
        public void setStatValue(string id, int value)
        {
            foreach (Stat stat in stats)
            {
                if (stat.Id == id)
                {
                    //Cas ou la stat a une valeur max
                    if (stat.MAX_VALUE != 0 && value <= stat.MAX_VALUE )
                    {
                        stat.Value = value;
                    }
                    else
                    {
                        stat.Value = value;
                    }
                }
            }
        }

        /// <summary>
        /// Permet de recuperer la valeur d'une stat
        /// </summary>
        /// <param name="id">L'id de la stat</param>
        /// <returns>La valeur ou 0 si la stat n'est pas trouvee</returns>
        public int getStatValue(string id)
        {
            foreach (Stat stat in stats)
            {
                if (stat.Id == id)
                {
                    return stat.Value;
                }
            }
            return 0;
        }

        public static Stats operator+(Stats a, Stats b)
        {
            //La nouvelle liste de stats
            List<Stat> newStats = new List<Stat>();
            //On parcoures toutes les stats pour les additionners
            for (int i = 0; i < a.stats.Count; i++)
            {
                //On evite quelque bug
                if (b.stats[i] != null && a.stats[i].Id == b.stats[i].Id)
                {
                    //On additionne les values
                    a.stats[i].Value += b.stats[i].Value;
                    newStats.Add(a.stats[i]);
                }
            }
            //On retourne un instance de Stats avec les nouvelle stats
            return new Stats() { stats = newStats };
        }
    }
}
