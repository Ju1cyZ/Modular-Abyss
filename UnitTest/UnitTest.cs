using ModularAbyss;
using ModularAbyss.Scripts.Components;
using ModularAbyss.Scripts.Components.Gameobjects;
using ModularAbyss.Scripts.Components.Gameobjects.Entities;
using ModularAbyss.Scripts.Components.Gameobjects.Entities.Enemies;
using ModularAbyss.Scripts.Components.Gameobjects.Projectiles;
using ModularAbyss.Scripts.Structures;
using System.Numerics;

namespace UnitTest
{
    [TestClass]
    public sealed class UnitTest
    {
        [DataTestMethod]
        [DataRow(23, 23)]
        [DataRow(42, 42)]
        [DataRow(12, 12)]
        [DataRow(0, 0)]
        [DataRow(Int32.MaxValue, Int32.MaxValue)]
        [DataRow(-20, -20)]
        public void Stats_statValueDefine_statGetDefined(int value, int expected)
        {
            Stats stats = new Stats();

            stats.setStatValue(STATS_ID.ARMOR, value);

            Assert.AreEqual(expected, stats.getStat(STATS_ID.ARMOR).Value);
        }

        [DataTestMethod]
        [DataRow(23, 20, 20)]
        [DataRow(0, 50, 0)]
        [DataRow(78, 5, 5)]
        public void Stats_setValueBewondTheMax_statAreDefinedToMax(int value, int maxValue, int expected)
        {
            Stats stats = new Stats();

            stats.setStatMaxValue(STATS_ID.HEALTH, maxValue, true);
            stats.setStatValue(STATS_ID.HEALTH, value);

            Assert.AreEqual(expected, stats.getStat(STATS_ID.HEALTH).Value);
        }

        [DataTestMethod]
        [DataRow(14, 20, 32, 100, 46)]
        [DataRow(20, 20, 40, 50, 50)]
        public void Stats_sumTwoStats_EachStatAreAdditionned(int stat1Value, int maxStat1Value, int stat2Value, int maxStat2Value, int expected)
        {
            Stats stats1 = new Stats();
            stats1.setStatMaxValue(STATS_ID.ARMOR, maxStat1Value, true);
            stats1.setStatValue(STATS_ID.ARMOR, stat1Value);

            Stats stats2 = new Stats();
            stats2.setStatMaxValue(STATS_ID.ARMOR, maxStat2Value, true);
            stats2.setStatValue(STATS_ID.ARMOR, stat2Value);

            Stats stats = stats1 + stats2;

            Assert.AreEqual(expected, stats.getStat(STATS_ID.ARMOR).Value);
        }

        [DataTestMethod]
        [DataRow(14, 20, 32, 50)]
        [DataRow(20, 20, 40, 50)]
        public void Stats_sumTwoStats_NewStatLengthAreEqual(int stat1Value, int maxStat1Value, int stat2Value, int maxStat2Value)
        {
            Stats stats1 = new Stats();
            stats1.setStatMaxValue(STATS_ID.ARMOR, maxStat1Value, true);
            stats1.setStatValue(STATS_ID.ARMOR, stat1Value);

            Stats stats2 = new Stats();
            stats1.setStatMaxValue(STATS_ID.ARMOR, maxStat2Value, true);
            stats2.setStatValue(STATS_ID.ARMOR, stat2Value);

            Stats stats = stats1 + stats2;

            Assert.AreEqual(stats1.getStats().Count, stats.getStats().Count);
        }

        [DataTestMethod]
        [DataRow(14, 20, 32, 50, 46)]
        [DataRow(20, 20, 40, 50, 50)]
        public void Stat_sumTwoStat_NewStatValueAreCorrect(int stat1Value, int maxStat1Value, int stat2Value, int maxStat2Value, int expected)
        {
            Stat stat1 = STATS.HEALTH;
            stat1.MAX_VALUE = maxStat1Value;
            stat1.Value = stat1Value;

            Stat stat2 = STATS.HEALTH;
            stat2.MAX_VALUE = maxStat2Value;
            stat2.Value = stat2Value;

            Stat stat = stat1 + stat2;

            Assert.AreEqual(expected, stat.Value);
        }


        #region Player Test
        [TestMethod]
        public void TestPlayer_hurtWithStats()
        {
            // Arrange
            Player player;
            Enemy enemy;
            int damage;

            player = new Player();
            player.Stats.setStatMaxValue(STATS_ID.HEALTH, 50, true);
            enemy = new Slime();

            // Act
            enemy.Stats.setStatValue(STATS_ID.MELE_DAMAGE, 5);
            damage = 1;

            // Assert
            player.HurtWithStats(damage, enemy);
            Assert.AreEqual(50 - 5, player.Health);
        }


        [DataTestMethod]
        [DataRow(10, 35, 50, 45)]
        [DataRow(0, 21, 50, 21)]
        [DataRow(-32, 50, 50, 50)]
        [DataRow(35, 40, 50, 50)]
        public void Heal_WhenPlayerGetHeal_HealthIsIncreased(int heal, int health, int MAX_HEALTH, int expected)
        {
            // Arrange
            Player player;

            player = new Player();
            player.Stats.setStatMaxValue(STATS_ID.HEALTH, MAX_HEALTH, true);
            player.Stats.setStatValue(STATS_ID.HEALTH, health);

            // Act
            player.Heal(heal);

            // Assert
            Assert.AreEqual(expected, player.Stats.getStat(STATS_ID.HEALTH).Value);
        }


        [DataTestMethod]
        [DataRow(23, 25, 59, 21, 23, 12)]
        public void Projectile_TestSetDirection_DirectionIsCorrectlySet(int currentPosX, int currentPosY, int targetPosX, int targetPosY)
        {
            Fireball fireBall = new Fireball();
            fireBall.Position = new Microsoft.Xna.Framework.Vector2(currentPosX, currentPosY);
            Vector2 target = new Vector2(targetPosX, targetPosY);

            fireBall.setDirectionFromVector(target);


            Assert.AreEqual(new Vector2(), fireBall.Direction);
        }
        
        #endregion

    }
}
