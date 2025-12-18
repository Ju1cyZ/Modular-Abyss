using Microsoft.Xna.Framework;
using ModularAbyss.Scripts.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularAbyss.Scripts.Core
{
    public static class GameManager
    {

        public static CameraObject Camera;

        public static Vector2 CameraPosition
        {
            get => CameraObject.Position;
        }

        public static Random Random = new Random();

        /// <summary>
        /// Permet de bouger la camera
        /// </summary>
        /// <param name="x">Le mouvement sur l'axe X</param>
        /// <param name="y">Le mouvement sur l'axe Y</param>
        public static void MoveCamera(float x = 0, float y = 0)
        {
            CameraObject.MoveCamera(x, y);
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
            return MoveLinearCamera(deltaTime, speed, destination);
        }

        /// <summary>
        /// Permet de bouger la camera
        /// </summary>
        /// <param name="movementAxes">Le mouvement sur l'axe X/Y</param>
        public static void MoveCamera(Vector2 movementAxes, int moveSpeed = 1)
        {
            MoveCamera(movementAxes, moveSpeed);
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
