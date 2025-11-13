using Microsoft.Xna.Framework;

namespace ModularAbyss.Scripts.Interfaces
{
    public interface ICollider 
    {
        Rectangle Bounds { get; set; }

        public void SetColliderBounds();
    }
}
