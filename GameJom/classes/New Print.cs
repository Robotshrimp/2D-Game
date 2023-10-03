using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Xml.Schema;

namespace GameJom
{
    // a temperary class for new print implimentation, will be merged with regular print after screens are refactored
    public class FontPreset
    {
        int Spacing = 4; // spacing between letters as a ratio of letter width
        Color color = Color.White; // font color
        int FontSize = 32; // preset of fontSize
        AutomatedDraw DrawParam = new AutomatedDraw();
        Folder Font;


        public FontPreset(Folder font) // input the folder containing all used characters 
        {
            this.Font = font;
        }
        public void AdvancedPresets(AutomatedDraw drawParam, int fontSize, Color color, int spacing)
        {
            this.color = color;
            this.DrawParam = drawParam;
            this.FontSize = fontSize;
            this.Spacing = spacing;
        }


        public Point Print(string text, Point printLocation)
        {
            Folder CharType;
            int LineSize = 0; // denotes the place for the next character print
            for (int n = 0; n < text.Length; n++)
            {
                if (!char.IsLetter(text[n]))
                {
                    CharType = Font.SubFolders["Misc"];
                }
                else if (char.IsUpper(text[n]))
                {
                    CharType = Font.SubFolders["Upper"];
                }
                else 
                {
                    CharType = Font.SubFolders["Lower"];
                }
                if (CharType.Storage.ContainsKey(text[n].ToString()))
                {

                    Texture2D characterTexture = (Texture2D)CharType.Storage[text[n].ToString()];
                    float FontSizeRatio = FontSize / characterTexture.Height;
                    DrawParam.Draw(new Rectangle(printLocation.X + LineSize, printLocation.Y, (int)(characterTexture.Bounds.Width * FontSizeRatio), FontSize),
                        characterTexture, color);
                    LineSize += characterTexture.Width + Spacing;
                }
                else
                    LineSize += (int)FontSize / 2 + Spacing;
            }
            return new Point(LineSize, FontSize); // returns the size of the total print 
        }
        public Point GetPrintSize(string text) // returns the size of the text if printed in the current preset
        {
            int totalLength = 0;
            foreach(char n in  text)
            {
                Texture2D tempTexture = (Texture2D)Font.Storage[n.ToString()];
                totalLength += (tempTexture.Width * (FontSize / tempTexture.Height)) + Spacing;
            }
            return new Point(totalLength, FontSize);
        }
    }
}
