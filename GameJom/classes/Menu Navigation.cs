using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace GameJom
{
    public class Menu
    {
        public Texture2D basePrint;
        public Texture2D hoverPrint;
        public Texture2D pressPrint;
        public static bool leftMouseInputLastFrame;
        AutomatedDraw drawParam;
        public FontSettings printParam;
        public bool enabled = true;
        public Menu(AutomatedDraw DrawParameter, FontSettings Print, FontTexture Font)
        {
            this.drawParam = DrawParameter;
            this.printParam = Print;

            basePrint = Font.Font;
            hoverPrint = Font.HoverFont;
            pressPrint = Font.PressFont;
        }
        public Rectangle ButtonSize(Rectangle button, string text = "")
        {
            if (printParam.PrintSize(text).X > button.Width)
                button.Width = printParam.PrintSize(text).X;
            if (printParam.PrintSize(text).Y > button.Height)
                button.Height = printParam.PrintSize(text).Y;
            return button;
        }
        public bool ButtonPressedLeftAt(Rectangle button, Texture2D baseTexture = null, string text = "")
        {
            button = ButtonSize(button, text);
            bool pressed = false;
            Point adjustedMousePosition = drawParam.CalculationRectangle(new Rectangle(Mouse.GetState().X, Mouse.GetState().Y,0,0)).Location;
            Texture2D usedFont = basePrint;
            if (adjustedMousePosition.X < button.Right &&
                adjustedMousePosition.X > button.Left &&
                adjustedMousePosition.Y < button.Bottom &&
                adjustedMousePosition.Y > button.Top && enabled)
            {
                usedFont = hoverPrint;
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    usedFont = pressPrint;
                    pressed = true;
                }
            }
            if (baseTexture != null)
                drawParam.Draw(button, baseTexture);
            printParam.Print(drawParam, usedFont, text, button.Location);

            if (pressed)
            {
                return true;
            }
            return false;
        }


    }
    /*
    public class Button 
    {

        AutomatedDraw drawButton;
        public Rectangle button;

        FontSettings printParam;
        string text;

        public bool pressedLeft;
        public bool pressedRight;

        public bool hovered;

        public bool loaded;


        public Button(AutomatedDraw DrawParameters, Rectangle Button, bool Loaded = true)
        {
            this.drawButton = DrawParameters;
            this.loaded = Loaded;
            this.button = Button;
        }
        public Button(AutomatedDraw DrawParameters, FontSettings PrintParam, Point TextButtonLocation, string Text, bool Loaded = true)
        {
            this.drawButton = DrawParameters;
            this.printParam = PrintParam;
            this.loaded = Loaded;
            this.button = new Rectangle(TextButtonLocation, PrintParam.PrintSize(Text));
            this.text = Text;
        }


        public void D_ButtonUpdate(Texture2D texture)
        {
            if (loaded)
            {
                pressedLeft = false;
                pressedRight = false;
                drawButton.Draw(button, texture);
                PressedCheck();
            }
        }
        public void TextButtonUpdate(Texture2D Font)
        {
            if (loaded)
            {
                pressedLeft = false;
                pressedRight = false;
                printParam.Print(drawButton, Font, text, button.Location);
                PressedCheck();
            }
        }

        public bool PressedCheck()
        {
            if (hovercheck())
            {
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    pressedLeft = true;
                    return true;
                }
                if (Mouse.GetState().RightButton == ButtonState.Pressed)
                {
                    pressedRight = true;
                    return true;
                }
            }
            return false;
        }
        public bool hovercheck()
        {
            button = drawButton.DisplayRectangle(button);
            if (Mouse.GetState().X < button.Right &&
                Mouse.GetState().X > button.Left &&
                Mouse.GetState().Y < button.Bottom &&
                Mouse.GetState().Y > button.Top)
            {
                hovered = true;
                return true;
            }
            return false;
        }
    }
    */
}
