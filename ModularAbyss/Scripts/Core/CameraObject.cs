using ModularAbyss.Scripts.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Text;
using System.Threading.Tasks;
using ModularAbyss.Scripts.Components;

namespace ModularAbyss.Scripts.Core
{
    public class CameraObject
    {
        public static Vector2 Position;

        public delegate void OnCameraMoveHandler(Vector2 newPos);
        public static event OnCameraMoveHandler OnCameraMove;

        public static CameraObject Instance;

        public CameraObject()
        {
            Instance = this;

            Main.getInstance().OnInitialize += Initialize;
            Main.getInstance().OnUpdate += Update;
        }

        private void Initialize() 
        { 
        
        }

        private void Update(GameTime gameTime, List<Interfaces.ICollider> colliders)
        {
            Vector2 cameraMovePos = new Vector2(-(GLOBAL.WINDOW_WIDTH / 2), -(GLOBAL.WINDOW_HEIGHT / 2));

            if (InputManager.MouseObject.Position.Y < GLOBAL.WINDOW_HEIGHT * 0.2f)
            {
                cameraMovePos.Y += 50; 
            }
            else if (InputManager.MouseObject.Position.Y > GLOBAL.WINDOW_HEIGHT * 0.8f)
            {
                cameraMovePos.Y -= 50;
            }


            if (InputManager.MouseObject.Position.X < GLOBAL.WINDOW_WIDTH * 0.2f)
            {
                cameraMovePos.X += 50;
            }
            else if (InputManager.MouseObject.Position.X > GLOBAL.WINDOW_WIDTH * 0.8f)
            {
                cameraMovePos.X -= 50;
            }

            
            MoveLinearCamera(gameTime.ElapsedGameTime.TotalSeconds, 500, cameraMovePos);
        }

        /// <summary>
        /// Permet de bouger la camera
        /// </summary>
        /// <param name="x">Le mouvement sur l'axe X</param>
        /// <param name="y">Le mouvement sur l'axe Y</param>
        public static void MoveCamera(float x = 0, float y = 0)
        {
            Vector2 destination = new Vector2(x, y);

            //destination.X += GLOBAL.WINDOW_WIDTH / 2;
            //destination.Y += GLOBAL.WINDOW_HEIGHT / 2;

            Position += destination;

            OnCameraMove?.Invoke(Position);
        }

        /// <summary>
        /// Applique un mouvement lineaire sur la camera
        /// </summary>
        /// <param name="deltaTime">Delta Time</param>
        /// <param name="speed">La vitesse</param>
        /// <param name="destination">La position vers laquelle bouger</param>
        /// <returns>Si arriver a destination</returns>
        public static bool MoveLinearCamera(double deltaTime, int speed, Vector2 destination)
        {
            destination.X += GLOBAL.WINDOW_WIDTH / 2;
            destination.Y += GLOBAL.WINDOW_HEIGHT / 2;

            float distance = Vector2.Distance(Position, destination);

            //Le gameObject est arriver a destination
            if (distance <= 10)
            {
                return true;
            }

            //Defini si il doit reculer ou pas 
            float movementX = Position.X > destination.X ? -speed : speed;
            //Defini si il doit monter ou pas
            float movementY = Position.Y > destination.Y ? -speed : speed;

            //Cas ou la position N est egale a la destination N donc on bouge pas dans l'axe N
            movementX = Position.X == destination.X ? 0 : movementX;
            movementY = Position.Y == destination.Y ? 0 : movementY;

            Vector2 movement = new(movementX, movementY);

            //Effectue le mouvement
            Position += movement * (float)deltaTime;
            //Position.X += movementX * (float)deltaTime;
            //Position.Y += movementY * (float)deltaTime;

            OnCameraMove?.Invoke(Position);
            return false;
        }

        /// <summary>
        /// Permet de bouger la camera
        /// </summary>
        /// <param name="movementAxes">Le mouvement sur l'axe X/Y</param>
        public static void MoveCamera(Vector2 movementAxes, int moveSpeed = 1)
        {
            MoveCamera(movementAxes.X * moveSpeed, movementAxes.Y * moveSpeed);
        }
    }
}
