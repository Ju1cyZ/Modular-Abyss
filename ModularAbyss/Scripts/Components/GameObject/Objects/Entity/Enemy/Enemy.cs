using ModularAbyss.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularAbyss.Scripts.Components.GameObjectNamespace.Objects.Entity.Enemy
{
    public abstract class Enemy : Entity, ICollider
    {
        public abstract void SetColliderBounds();
    }
}
