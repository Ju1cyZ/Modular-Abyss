using Microsoft.Xna.Framework.Input;
using ModularAbyss.Scripts.Components.InputDevices.InputElements;
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

        private Input MOVE_UP = new Input(Keys.W);
        private Input MOVE_LEFT = new Input(Keys.A);
        private Input MOVE_DOWN = new Input(Keys.S);
        private Input MOVE_RIGHT = new Input(Keys.D);
        private Input SPRINT = new Input(Keys.LeftShift);

        public bool MOVE_UP_PRESSED
        {
            get => MOVE_UP.IsKeyDownThisFrame();
        }

        public bool MOVE_LEFT_PRESSED
        {
            get => MOVE_LEFT.IsKeyDownThisFrame();
        }

        public bool MOVE_DOWN_PRESSED
        {
            get => MOVE_DOWN.IsKeyDownThisFrame();
        }

        public bool MOVE_RIGHT_PRESSED
        {
            get => MOVE_RIGHT.IsKeyDownThisFrame();
        }

        public bool SPRINT_PRESSED
        {
            get => SPRINT.IsKeyDownThisFrame();
        }
    }
}
