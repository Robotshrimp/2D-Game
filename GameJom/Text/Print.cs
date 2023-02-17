using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameJom
{
    public class PrintManager
    {
        Texture2D font;
        int spacing;
        Color color;
        public Point fontSize;
        public PrintManager(Texture2D Font, int Spacing, Color Color, Point FontSize)
        {
            this.font = Font;
            this.spacing = Spacing;
            this.color = Color;
            this.fontSize = FontSize;
        }



        public void Print(AutomatedDraw DrawParam, Texture2D Font, string Text, Point PrintLocation)
        {
            if (Text == "start")
            {

            }
            for (int n = 0; n < Text.Length; n++)
            {

                D_printCharacter(
                    DrawParam,
                    new Rectangle(PrintLocation.X + ((fontSize.X + spacing) * n), PrintLocation.Y, fontSize.X, fontSize.Y),
                    Font,
                    Text[n],
                    color);

            }
        }
        public void Print(AutomatedDraw DrawParam, string Text, Point PrintLocation)
        {
            Print(DrawParam, font, Text, PrintLocation);
        }

        public Rectangle PrintLocation(AutomatedDraw draw, Point PrintLocation)
        {
            return draw.DisplayRectangle(new Rectangle(PrintLocation.X , PrintLocation.Y, fontSize.X, fontSize.Y));
        }

        public Point PrintSize(string text)
        {
            return new Point(text.Length * (spacing + fontSize.X) - spacing, fontSize.Y);
        }








        public void D_printCharacter(AutomatedDraw Draw,Rectangle Location, Texture2D Font, char Character, Color Color)
        {
            Draw.mDraw(Location, Font, letterFinder(Character), Color);
        }

        public Rectangle letterIndex(Point letterCoord)
        {
            Rectangle fontSheet = font.Bounds;
            int width = (int)(fontSheet.Width / 36);
            int height = (int)(fontSheet.Height / 2);



            return new Rectangle(letterCoord.X * width + 1, letterCoord.Y * height + 1, width - 2, height - 2);

            // above return code is temp fix for texture bug, will replace later when propper text is used


            //return new Rectangle(letterCoord.X * width, letterCoord.Y * height, width, height);
        }
        public Rectangle letterFinder(char Character)
        {
            Point character;
            int capital = 1;
            int lowerCase = 0;
            switch (Character)
            {
                case 'a': character = new Point(0, lowerCase); break;
                case 'b': character = new Point(1, lowerCase); break;
                case 'c': character = new Point(2, lowerCase); break;
                case 'd': character = new Point(3, lowerCase); break;
                case 'e': character = new Point(4, lowerCase); break;
                case 'f': character = new Point(5, lowerCase); break;
                case 'g': character = new Point(6, lowerCase); break;
                case 'h': character = new Point(7, lowerCase); break;
                case 'i': character = new Point(8, lowerCase); break;
                case 'j': character = new Point(9, lowerCase); break;
                case 'k': character = new Point(10, lowerCase); break;
                case 'l': character = new Point(11, lowerCase); break;
                case 'm': character = new Point(12, lowerCase); break;
                case 'n': character = new Point(13, lowerCase); break;
                case 'o': character = new Point(14, lowerCase); break;
                case 'p': character = new Point(15, lowerCase); break;
                case 'q': character = new Point(16, lowerCase); break;
                case 'r': character = new Point(17, lowerCase); break;
                case 's': character = new Point(18, lowerCase); break;
                case 't': character = new Point(19, lowerCase); break;
                case 'u': character = new Point(20, lowerCase); break;
                case 'v': character = new Point(21, lowerCase); break;
                case 'w': character = new Point(22, lowerCase); break;
                case 'x': character = new Point(23, lowerCase); break;
                case 'y': character = new Point(24, lowerCase); break;
                case 'z': character = new Point(25, lowerCase); break;

                case 'A': character = new Point(0, capital); break;
                case 'B': character = new Point(1, capital); break;
                case 'C': character = new Point(2, capital); break;
                case 'D': character = new Point(3, capital); break;
                case 'E': character = new Point(4, capital); break;
                case 'F': character = new Point(5, capital); break;
                case 'G': character = new Point(6, capital); break;
                case 'H': character = new Point(7, capital); break;
                case 'I': character = new Point(8, capital); break;
                case 'J': character = new Point(9, capital); break;
                case 'K': character = new Point(10, capital); break;
                case 'L': character = new Point(11, capital); break;
                case 'M': character = new Point(12, capital); break;
                case 'N': character = new Point(13, capital); break;
                case 'O': character = new Point(14, capital); break;
                case 'P': character = new Point(15, capital); break;
                case 'Q': character = new Point(16, capital); break;
                case 'R': character = new Point(17, capital); break;
                case 'S': character = new Point(18, capital); break;
                case 'T': character = new Point(19, capital); break;
                case 'U': character = new Point(20, capital); break;
                case 'V': character = new Point(21, capital); break;
                case 'W': character = new Point(22, capital); break;
                case 'X': character = new Point(23, capital); break;
                case 'Y': character = new Point(24, capital); break;
                case 'Z': character = new Point(25, capital); break;

                case '0': character = new Point(26, lowerCase); break;
                case '1': character = new Point(27, lowerCase); break;
                case '2': character = new Point(28, lowerCase); break;
                case '3': character = new Point(29, lowerCase); break;
                case '4': character = new Point(30, lowerCase); break;
                case '5': character = new Point(31, lowerCase); break;
                case '6': character = new Point(32, lowerCase); break;
                case '7': character = new Point(33, lowerCase); break;
                case '8': character = new Point(34, lowerCase); break;
                case '9': character = new Point(35, lowerCase); break;

                case '!': character = new Point(26, capital); break;
                case '#': character = new Point(27, capital); break;
                case '$': character = new Point(28, capital); break;
                case '%': character = new Point(29, capital); break;
                case '^': character = new Point(30, capital); break;
                case '&': character = new Point(31, capital); break;
                case '*': character = new Point(32, capital); break;
                case '(': character = new Point(33, capital); break;
                case ')': character = new Point(34, capital); break;
                case ' ': character = new Point(35, capital); break;

                default: character = new Point(35, capital); break;
            }

            return letterIndex(character);
        }
    }
}
