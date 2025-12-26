using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ModularAbyss.Scripts.Components.HudObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularAbyss.Scripts.Components.InputDevices.InputElements
{
    public class Input
    {
        Keys Key = Keys.None;
        MouseButtons MouseKey = MouseButtons.None;

        bool asBeenCheck = false;

        public Input(Keys key)
        {
            Key = key;
            Main.getInstance().OnUpdate += Update;
        }

        public Input(MouseButtons key)
        {
            MouseKey = key;
            Main.getInstance().OnUpdate += Update;
        }

        private void Update(GameTime gameTime)
        {
            if (Key != Keys.None)
            {
                if (asBeenCheck && !InputManager.KeyboardObject.kState.IsKeyDown(Key))
                {
                    asBeenCheck = false;
                }
            }
            else if (MouseKey != MouseButtons.None)
            {

                if (asBeenCheck && getCorrectMouseButton() == ButtonState.Released)
                {
                    asBeenCheck = false;
                }
            }

        }

        /// <summary>
        /// Permet de recuperer si la touche est presse une seule fois
        /// </summary>
        /// <returns>Si la touche est presse</returns>
        public bool IsKeyDown()
        {
            if (Key != Keys.None)
            {
                if (!asBeenCheck && InputManager.KeyboardObject.kState.IsKeyDown(Key))
                {
                    asBeenCheck = true;
                    return true;
                }
            }
            else if (MouseKey != MouseButtons.None)
            {
                if (!asBeenCheck && getCorrectMouseButton() == ButtonState.Pressed)
                {
                    asBeenCheck = true;
                    return true;
                }
            }
            
            return false;
        }

        private ButtonState getCorrectMouseButton()
        {
            switch (MouseKey)
            {
                case MouseButtons.LeftButton:
                    return InputManager.MouseObject.mState.LeftButton;
                case MouseButtons.RightButton:
                    return InputManager.MouseObject.mState.RightButton;
            }

            throw new ArgumentException("Mouse button not processed");
        }

        /// <summary>
        /// Permet de recuperer si la touche est presser pendant l'image demander
        /// </summary>
        /// <returns>Si la touche est presse</returns>
        public bool IsKeyDownThisFrame()
        {
            if (Key != Keys.None)
            {
                return InputManager.KeyboardObject.kState.IsKeyDown(Key);
            }
            else
            {
                return getCorrectMouseButton() == ButtonState.Pressed;
            }
        }
    }
}
