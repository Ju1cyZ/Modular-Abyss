using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace ModularAbyss.Scripts.Interfaces
{
    public interface ITextureAnimated
    {
        /// <summary>
        /// Permet de genere une texture a partir d'un spritesheet avec une animation
        /// </summary>
        /// <param name="width">La largeur de la texture</param>
        /// <param name="height">La hauteur de la texture</param>
        /// <param name="posY">La position Y dans le spritesheet pour la texture</param>
        /// <param name="frameCount">Le nombre de frame/texture pour l'animation (Logic)</param>
        /// <param name="frameIndex">Le frame courrant</param>
        /// <param name="deltaTime">Le deltaTime</param>
        /// <param name="animationTimeBetweenFrame">Le temps entre chaque frame (Logic)</param>
        /// <returns>Un rectangle pour la texture</returns>
        public Rectangle GenerateTextureRect(int width, int height, int posY, int frameCount, ref int frameIndex, float deltaTime, ref float animationTimeBetweenFrame, bool stayInLastFrame = false)
        {
            animationTimeBetweenFrame += deltaTime;

            if (animationTimeBetweenFrame >= 0.08f)
            {
                frameIndex += 1;
                animationTimeBetweenFrame = 0;
            }
            if (frameIndex >= frameCount)
            {
                frameIndex = 0;
                if (stayInLastFrame)
                {
                    frameIndex = frameCount - 1;
                }
            }

            return new Rectangle(width * frameIndex, posY, width, height);
        }
    }
}
