using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ModularAbyss.Scripts.Components.InputDevices;
using ModularAbyss.Scripts.Enums;
using ModularAbyss.Scripts.Interfaces;
using ModularAbyss.Scripts.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ModularAbyss.Scripts.Components
{
    public static class InputManager
    {
        private static MouseObject mouseObject = new();
        private static KeyboardObject keyboardObject = new();

        public static MouseObject MouseObject
        {
            get => mouseObject;
        }

        public static KeyboardObject KeyboardObject
        {
            get => keyboardObject;
        }

        public static bool MOVE_UP
        {
            get => KeyboardObject.MOVE_UP_PRESSED;
        }

        public static bool MOVE_LEFT
        {
            get => KeyboardObject.MOVE_LEFT_PRESSED;
        }

        public static bool MOVE_DOWN
        {
            get => KeyboardObject.MOVE_DOWN_PRESSED;
        }

        public static bool MOVE_RIGHT
        {
            get => KeyboardObject.MOVE_RIGHT_PRESSED;
        }

        public static bool SPRINT
        {
            get => KeyboardObject.SPRINT_PRESSED;
        }

        public static void Update(GameTime gameTime, List<ICollider> colliders)
        {
            MouseObject.Update(gameTime, colliders);
        }

        public static void Draw(GameTime gameTime, ref SpriteBatch spriteBatch)
        {
            MouseObject.Draw(gameTime, ref spriteBatch);
        }

    }
}
