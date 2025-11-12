using ModularAbyss.Scripts.Components.HudObjects;
using Microsoft.Xna.Framework;

namespace ModularAbyss.Scripts.Interfaces
{
    internal interface ILifeBar
    {
        public ProgressBar HealthBar { get; set; }

        /* Overlay */
        private const int HEALTH_BAR_SIZE_X = 48;
        private const int HEALTH_BAR_SIZE_Y = 16;

        /// <summary>
        /// Permet d'initializer la bar de vie
        /// </summary>
        /// <param name="MAX_HEALTH">La vie maximal de la bar</param>
        /// <param name="HEALTH_BAR_SIZE_X">La taille X de la bar</param>
        /// <param name="HEALTH_BAR_SIZE_Y">La taille Y de la bar</param>
        void Initialize(int MAX_HEALTH)
        {
            HealthBar = new ProgressBar(HEALTH_BAR_SIZE_X, HEALTH_BAR_SIZE_Y, 4);
            HealthBar.maxValue = MAX_HEALTH;
            HealthBar.Scale = 3;
        }

        void Update(int Health)
        {
            HealthBar.Position = GetHealthBarPos(); // On definie la position de la bar de vie
            HealthBar.value = Health;
        }

        Vector2 GetHealthBarPos();
    }
}
