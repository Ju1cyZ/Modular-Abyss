using Microsoft.Xna.Framework;
namespace ModularAbyss.Scripts.Interfaces
{
    internal interface IAi
    {
        public static Vector2 moveTowardsPlayer(Vector2 objectPosition)
        {
            Vector2 playerPos = Main.ActiveScene.Player.Position;
            Vector2 heading = playerPos - objectPosition;
            heading.Normalize();
            return heading;
        }
    }
}
