using Microsoft.Xna.Framework;
using ModularAbyss.Scripts.Components.Gameobjects;
using System.Linq;
namespace ModularAbyss.Scripts.Interfaces
{
    public interface IAi
    {
        public static Vector2 moveTowardsPlayer(Vector2 objectPosition)
        {
            Vector2 playerPos = Main.ActiveScene.Player.Position + new Vector2(Player.TextureSize / 2, Player.TextureSize / 2);
            Vector2 heading = playerPos - objectPosition;
            heading.Normalize();
            return heading;
        }
    }
}
