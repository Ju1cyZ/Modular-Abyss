using Microsoft.Xna.Framework;

namespace ModularAbyss.Scripts.Interfaces
{
    internal interface ICollider 
    {
        Rectangle Bounds { get; set; }

        public void SetColliderBounds();
    }
}
