using ModularAbyss;
using ModularAbyss.Scripts.Components.Gameobjects;
using ModularAbyss.Scripts.Components.Gameobjects.Entities;
using ModularAbyss.Scripts.Components.Gameobjects.Entities.Enemies;
using ModularAbyss.Scripts.Structures;

namespace UnitTest
{
    [TestClass]
    public sealed class UnitTest
    {
        [TestMethod]
        public void TestPlayer_hurtWithStats()
        {
            //
            Player player;
            Enemy enemy;
            int damage;

            //
            player = new Player();
            player.Stats.setStatMaxValue(STATS_ID.HEALTH, 50, true);
            enemy = new Slime();

            enemy.Stats.setStatValue(STATS_ID.MELE_DAMAGE, 5);
            damage = 1;

            //
            player.HurtWithStats(damage, enemy);

            Assert.AreEqual(50 - 5, player.Health);
        }
    }
}
