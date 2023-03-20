using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
namespace GameJom
{
    public class Menu
    {
        Dictionary<string, int> buttonName= new Dictionary<string, int>();
        public List<Button> buttons;
        static Texture2D printParam;
        static Texture2D hoverPrint;
        static Texture2D pressPrint;
        int spacing;
        bool loaded;
        public Menu(AutomatedDraw DrawParameter, PrintManager Print, string[] Buttons, Point Location, int Spacing, 
            bool Loaded = true)
        {
            this.buttons = new List<Button>();
            for (int n = 0; n < Buttons.Length; n++)
            {
                this.buttons.Add(new Button(DrawParameter, Print, new Point(Location.X, Location.Y + ((n - 1) * spacing) + (n  * Print.fontSize.Y)), Buttons[n], Loaded));
                buttonName.Add(Buttons[n], n);
            }
            this.spacing = Spacing;
            this.loaded = Loaded;
        }
        public void Initialize(Texture2D PrintParam, Texture2D HoverPrint, Texture2D PressPrint)
        {
            printParam = PrintParam;
            hoverPrint = HoverPrint;
            pressPrint = PressPrint;

        }


        public void MenuUpdate()
        {
            if (loaded)
            {
                for (int n = 0; n < buttons.Count; n++)
                {
                    if (buttons[n].PressedCheck())
                    { buttons[n].TextButtonUpdate(pressPrint); }
                    else if (buttons[n].hovercheck())
                    { buttons[n].TextButtonUpdate(hoverPrint); }
                    else
                    { buttons[n].TextButtonUpdate(printParam); }
                }
            }
        }

        public bool check(string button)
        {
            return buttons[buttonName[button]].PressedCheck();
        }
    }
    public class AdvButton
    {
        Texture2D defaultB;
        Texture2D hovered;
        Texture2D pressed;

        public AdvButton(Texture2D DefaultB, Texture2D Hovered, Texture2D Pressed)
        {
            this.defaultB = DefaultB;
            this.hovered = Hovered;
            this.pressed = Pressed;
        }
        public void Drawbutton(Button button)
        {
            if (button.PressedCheck())
            { button.TextButtonUpdate(pressed); }
            else if (button.hovercheck())
            { button.TextButtonUpdate(hovered); }
            else
            { button.TextButtonUpdate(defaultB); }
        }
    }
    public class ButtonTemplate
    {

    }
    public class Button 
    {

        AutomatedDraw drawButton;
        public Rectangle button;

        PrintManager printParam;
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
        public Button(AutomatedDraw DrawParameters, PrintManager PrintParam, Point TextButtonLocation, string Text, bool Loaded = true)
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
}
