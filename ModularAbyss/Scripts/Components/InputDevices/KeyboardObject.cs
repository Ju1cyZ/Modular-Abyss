using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularAbyss.Scripts.Components.InputDevices
{
    public class KeyboardObject
    {
        public KeyboardState kState
        {
            get => Keyboard.GetState();
        }

        private Keys MOVE_UP = Keys.W;
        private Keys MOVE_LEFT = Keys.A;
        private Keys MOVE_DOWN = Keys.S;
        private Keys MOVE_RIGHT = Keys.D;
        private Keys SPRINT = Keys.LeftShift;

        public bool MOVE_UP_PRESSED
        {
            get => kState.IsKeyDown(MOVE_UP);
        }

        public bool MOVE_LEFT_PRESSED
        {
            get => kState.IsKeyDown(MOVE_LEFT);
        }

        public bool MOVE_DOWN_PRESSED
        {
            get => kState.IsKeyDown(MOVE_DOWN);
        }

        public bool MOVE_RIGHT_PRESSED
        {
            get => kState.IsKeyDown(MOVE_RIGHT);
        }

        public bool SPRINT_PRESSED
        {
            get => kState.IsKeyDown(SPRINT);
        }


    }
}
