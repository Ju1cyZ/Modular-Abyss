using Microsoft.Xna.Framework;
using ModularAbyss.Scripts.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularAbyss.Scripts.Core
{
    internal static class GameManager
    {
        public static Vector2 CameraPosition = new Vector2(0, 0);

        public static Random Random = new Random();

        /// <summary>
        /// Permet de bouger la camera
        /// </summary>
        /// <param name="x">Le mouvement sur l'axe X</param>
        /// <param name="y">Le mouvement sur l'axe Y</param>
        public static void MoveCamera(float x = 0, float y = 0)
        {
            Vector2 destination = new Vector2(x, y);

            destination.X += GLOBAL.WINDOW_WIDTH / 2;
            destination.Y += GLOBAL.WINDOW_HEIGHT / 2;

            CameraPosition = destination;
        }

        /// <summary>
        /// Applique un mouvement lineaire sur la camera
        /// </summary>
        /// <param name="deltaTime">Delta Time</param>
        /// <param name="speed">La vitesse</param>
        /// <param name="destination">La position vers laquelle bouger</param>
        /// <returns>Si arriver a destination</returns>
        public static bool MoveLinearCamera(float deltaTime, int speed, Vector2 destination)
        {
            destination.X += GLOBAL.WINDOW_WIDTH / 2;
            destination.Y += GLOBAL.WINDOW_HEIGHT / 2;

            float distance = Vector2.Distance(CameraPosition, destination);

            //Le gameObject est arriver a destination
            if (distance <= 10)
            {
                return true;
            }

            //Defini si il doit reculer ou pas 
            float movementX = CameraPosition.X > destination.X ? -speed : speed;
            //Defini si il doit monter ou pas
            float movementY = CameraPosition.Y > destination.Y ? -speed : speed;

            //Cas ou la position N est egale a la destination N donc on bouge pas dans l'axe N
            movementX = CameraPosition.X == destination.X ? 0 : movementX;
            movementY = CameraPosition.Y == destination.Y ? 0 : movementY;

            //Effectue le mouvement
            CameraPosition.X += movementX * deltaTime;
            CameraPosition.Y += movementY * deltaTime;

            return false;
        }

        /// <summary>
        /// Permet de bouger la camera
        /// </summary>
        /// <param name="movementAxes">Le mouvement sur l'axe X/Y</param>
        public static void MoveCamera(Vector2 movementAxes)
        {
            MoveCamera(movementAxes.X, movementAxes.Y);
        }


        /// <summary>
        /// Permet de recuperer l'angle entre deux positions 
        /// </summary>
        /// <param name="vector1">La p1</param>
        /// <param name="vector2">La p2</param>
        /// <returns>L'angle en Radian</returns>
        public static double AngleBetweenVectors(Vector2 vector1, Vector2 vector2)
        {
            return Math.Atan2(vector2.Y - vector1.Y, vector2.X - vector1.X);
        }
    }
}
