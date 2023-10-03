using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameJom
{
    public class FontTexture
    {
        public Texture2D Font;
        public Texture2D HoverFont;
        public Texture2D PressFont;
        public FontTexture(Texture2D font1, Texture2D font2, Texture2D font3) 
        {
            Font = font1;
            HoverFont = font2;
            PressFont = font3;
        }
    }
    public class FontSettings 
    {
        int spacing;
        Color color;
        public Point fontSize;
        public FontSettings(int spacing, Color color, Point fontSize)
        {
            this.spacing = spacing;
            this.color = color;
            this.fontSize = fontSize;
        }



        public void Print(AutomatedDraw drawParam, Texture2D font, string text, Point printLocation)
        {
            for (int n = 0; n < text.Length; n++)
            {
                PrintCharacter(
                    drawParam,
                    new Rectangle(printLocation.X + ((fontSize.X + spacing) * n), printLocation.Y, fontSize.X, fontSize.Y),
                    font,
                    text[n],
                    color);
            }
        }

        public Rectangle PrintLocation(AutomatedDraw draw, Point printLocation)
        {
            return draw.DisplayRectangle(new Rectangle(printLocation.X , printLocation.Y, fontSize.X, fontSize.Y));
        }

        public Point PrintSize(string text)
        {
            return new Point(text.Length * (spacing + fontSize.X) - spacing, fontSize.Y);
        }








        public void PrintCharacter(AutomatedDraw draw,Rectangle location, Texture2D font, char character, Color color)
        {
            draw.Draw(location, font, LetterFinder(character, font), color);
        }

        public Rectangle LetterIndex(Point letterCoord, Texture2D font)
        {
            Rectangle fontSheet = font.Bounds;
            int width = (int)(fontSheet.Width / 36);
            int height = (int)(fontSheet.Height / 2);



            return new Rectangle(letterCoord.X * width, letterCoord.Y * height, width, height);
        }
        public Rectangle LetterFinder(char character, Texture2D font)
        {
            Point characterIndex;
            int capital = 1;
            int lowerCase = 0;
            switch (character)
            {
                case 'a': characterIndex = new Point(0, lowerCase); break;
                case 'b': characterIndex = new Point(1, lowerCase); break;
                case 'c': characterIndex = new Point(2, lowerCase); break;
                case 'd': characterIndex = new Point(3, lowerCase); break;
                case 'e': characterIndex = new Point(4, lowerCase); break;
                case 'f': characterIndex = new Point(5, lowerCase); break;
                case 'g': characterIndex = new Point(6, lowerCase); break;
                case 'h': characterIndex = new Point(7, lowerCase); break;
                case 'i': characterIndex = new Point(8, lowerCase); break;
                case 'j': characterIndex = new Point(9, lowerCase); break;
                case 'k': characterIndex = new Point(10, lowerCase); break;
                case 'l': characterIndex = new Point(11, lowerCase); break;
                case 'm': characterIndex = new Point(12, lowerCase); break;
                case 'n': characterIndex = new Point(13, lowerCase); break;
                case 'o': characterIndex = new Point(14, lowerCase); break;
                case 'p': characterIndex = new Point(15, lowerCase); break;
                case 'q': characterIndex = new Point(16, lowerCase); break;
                case 'r': characterIndex = new Point(17, lowerCase); break;
                case 's': characterIndex = new Point(18, lowerCase); break;
                case 't': characterIndex = new Point(19, lowerCase); break;
                case 'u': characterIndex = new Point(20, lowerCase); break;
                case 'v': characterIndex = new Point(21, lowerCase); break;
                case 'w': characterIndex = new Point(22, lowerCase); break;
                case 'x': characterIndex = new Point(23, lowerCase); break;
                case 'y': characterIndex = new Point(24, lowerCase); break;
                case 'z': characterIndex = new Point(25, lowerCase); break;

                case 'A': characterIndex = new Point(0, capital); break;
                case 'B': characterIndex = new Point(1, capital); break;
                case 'C': characterIndex = new Point(2, capital); break;
                case 'D': characterIndex = new Point(3, capital); break;
                case 'E': characterIndex = new Point(4, capital); break;
                case 'F': characterIndex = new Point(5, capital); break;
                case 'G': characterIndex = new Point(6, capital); break;
                case 'H': characterIndex = new Point(7, capital); break;
                case 'I': characterIndex = new Point(8, capital); break;
                case 'J': characterIndex = new Point(9, capital); break;
                case 'K': characterIndex = new Point(10, capital); break;
                case 'L': characterIndex = new Point(11, capital); break;
                case 'M': characterIndex = new Point(12, capital); break;
                case 'N': characterIndex = new Point(13, capital); break;
                case 'O': characterIndex = new Point(14, capital); break;
                case 'P': characterIndex = new Point(15, capital); break;
                case 'Q': characterIndex = new Point(16, capital); break;
                case 'R': characterIndex = new Point(17, capital); break;
                case 'S': characterIndex = new Point(18, capital); break;
                case 'T': characterIndex = new Point(19, capital); break;
                case 'U': characterIndex = new Point(20, capital); break;
                case 'V': characterIndex = new Point(21, capital); break;
                case 'W': characterIndex = new Point(22, capital); break;
                case 'X': characterIndex = new Point(23, capital); break;
                case 'Y': characterIndex = new Point(24, capital); break;
                case 'Z': characterIndex = new Point(25, capital); break;

                case '0': characterIndex = new Point(26, lowerCase); break;
                case '1': characterIndex = new Point(27, lowerCase); break;
                case '2': characterIndex = new Point(28, lowerCase); break;
                case '3': characterIndex = new Point(29, lowerCase); break;
                case '4': characterIndex = new Point(30, lowerCase); break;
                case '5': characterIndex = new Point(31, lowerCase); break;
                case '6': characterIndex = new Point(32, lowerCase); break;
                case '7': characterIndex = new Point(33, lowerCase); break;
                case '8': characterIndex = new Point(34, lowerCase); break;
                case '9': characterIndex = new Point(35, lowerCase); break;

                case '!': characterIndex = new Point(26, capital); break;
                case '#': characterIndex = new Point(27, capital); break;
                case '$': characterIndex = new Point(28, capital); break;
                case '%': characterIndex = new Point(29, capital); break;
                case '^': characterIndex = new Point(30, capital); break;
                case '&': characterIndex = new Point(31, capital); break;
                case '*': characterIndex = new Point(32, capital); break;
                case '(': characterIndex = new Point(33, capital); break;
                case ')': characterIndex = new Point(34, capital); break;
                case ' ': characterIndex = new Point(35, capital); break;

                default: characterIndex = new Point(35, capital); break;
            }

            return LetterIndex(characterIndex, font);
        }
    }
}
