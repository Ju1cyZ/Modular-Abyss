
using System.Numerics;

namespace ModularAbyss.Scripts.Components
{
    public class Stat
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Value { get; set; }
        public int MAX_VALUE { get; set; }
    
    
        public static Stat operator +(Stat a, Stat b)
        {
            Stat stat;

            if (a.Id != b.Id)
                throw new System.ArgumentException("L'id des deux states ne peuvent pas être différents");

            // Prends la stat qui possedent le plus grands MAX_VALUE
            if (a.MAX_VALUE >= b.MAX_VALUE)
                stat = a;
            else
                stat = b;

            // On addition les stats
            stat.Value = a.Value + b.Value;

            // Si la valeur est plus grande que la valeur max
            if (stat.Value > stat.MAX_VALUE)
                // On defini la valeur au max
                stat.Value = stat.MAX_VALUE;

            // Si la valeur est plus petite que zero
            if (stat.Value < 0)
                // On defini la valeur a zero
                stat.Value = 0;

            return stat;
        }


        public static Stat operator -(Stat a, Stat b)
        {
            Stat stat;

            if (a.Id != b.Id)
                throw new System.ArgumentException("L'id des deux states ne peuvent pas être différents");

            // Prends la stat qui possedent le plus grands MAX_VALUE
            if (a.MAX_VALUE > b.MAX_VALUE)
                stat = a;
            else
                stat = b;

            // On addition les stats
            stat.Value = a.Value - b.Value;

            // Si la valeur est plus petite que zero
            if (stat.Value < 0)
                // On defini la valeur a zero
                stat.Value = 0;

            // Si la valeur est plus grande que la valeur max
            if (stat.Value > stat.MAX_VALUE)
                // On defini la valeur au max
                stat.Value = stat.MAX_VALUE;

            return stat;
        }
    }
}
