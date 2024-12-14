using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace recap
{
    public class InputHandler
    {
        private KeyboardState CurrentKeyState;
        private KeyboardState PreviousKeyState;

        public void Update()
        {
            PreviousKeyState = CurrentKeyState;
            CurrentKeyState = Keyboard.GetState();
        }

        public bool IsKeyDown(Keys key) => CurrentKeyState.IsKeyDown(key);
        public bool WasKeyPressed(Keys key) => 
            PreviousKeyState.IsKeyUp(key) && CurrentKeyState.IsKeyDown(key);
        public bool IsKeyPressed(Keys key) => 
            CurrentKeyState.IsKeyDown(key) && !PreviousKeyState.IsKeyDown(key);
        public bool IsKeyReleased(Keys key) => 
            CurrentKeyState.IsKeyUp(key) && PreviousKeyState.IsKeyDown(key);
    }
}