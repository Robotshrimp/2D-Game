using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace GameJom
{
    public class Button : Game1
    {
        static AutomatedDraw drawButton;
        public bool Pressed;
        public void ButtonUpdate(Rectangle button, Texture2D texture, bool loaded = true)
        {
            if (loaded)
            {
                drawButton = new AutomatedDraw(loaded);
                drawButton.draw(button, texture);
                MouseState mouseState = Mouse.GetState();
                int x = mouseState.X;
                int y = mouseState.Y;
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    if (mouseState.X < button.Right &&
                        mouseState.X > button.Left &&
                        mouseState.Y < button.Bottom &&
                        mouseState.Y > button.Top)
                    {
                        Pressed = true;
                    }
                }
                Pressed = false;
            }
        }
    }
}
