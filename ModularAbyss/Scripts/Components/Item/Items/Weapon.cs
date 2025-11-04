using Microsoft.Xna.Framework;
using ModularAbyss.Scripts.Components.GameObjectNamespace.Objects.Entity;
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
        public Rectangle CollisionRectangle = Rectangle.Empty;
        protected bool asAttacked = false;
        protected Entity lastAttackedEntity;

        public Action Attack;

        public int Damage;

        public Weapon()
        {
            Attack = _attack;
            Main.getInstance().OnUpdate += Update;
        }

        public Weapon(string id, string name, string description, ItemTier itemTier, string texturePath, Rectangle spriteRectangle) : base(id, name, description, itemTier, texturePath, spriteRectangle)
        {
            Attack = _attack;
        }

        protected virtual void Update(GameTime gameTime, List<Interfaces.ICollider> colliders)
        {
            if (asAttacked && lastAttackedEntity != null && !CollisionRectangle.Intersects(lastAttackedEntity.CollisionRect))
            {
                asAttacked = false;
            }
        }

        /// <summary>
        /// L'attaque par defaut de l'arme
        /// </summary>
        protected abstract void _attack();

    }
}
