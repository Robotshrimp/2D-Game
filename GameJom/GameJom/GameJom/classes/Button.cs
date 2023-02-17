using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace GameJom
{
    public class Button 
    {
        static AutomatedDraw drawButton;
        public bool Pressed;
        public void ButtonUpdate(Rectangle button, Texture2D texture, bool loaded = true)
        {
            if (loaded)
            {
                Pressed = false;
                drawButton = new AutomatedDraw();
                drawButton.draw(button, texture);
                button = drawButton.DisplayRectangle(button);
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {

                    if (Mouse.GetState().X < button.Right &&
                        Mouse.GetState().X > button.Left &&
                        Mouse.GetState().Y < button.Bottom &&
                        Mouse.GetState().Y > button.Top)
                    {
                        Pressed = true;
                    }
                }
            }
        }
    }
}
