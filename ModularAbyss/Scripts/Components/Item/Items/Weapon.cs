using Microsoft.Xna.Framework;
using ModularAbyss.Scripts.Components.StatNamespace.StatsNamespace;
using ModularAbyss.Scripts.Enums;
using ModularAbyss.Scripts.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularAbyss.Scripts.Components.ItemNamespace.Items
{
    public abstract class Weapon : Item
    {
        public Action Attack;

        public int Damage;

        public Weapon()
        {
            Attack = _attack;
        }

        public Weapon(string id, string name, string description, ItemTier itemTier, string texturePath, Rectangle spriteRectangle) : base(id, name, description, itemTier, texturePath, spriteRectangle)
        {
            Attack = _attack;
        }

        /// <summary>
        /// L'attaque par defaut de l'arme
        /// </summary>
        protected abstract void _attack();
    }
}
