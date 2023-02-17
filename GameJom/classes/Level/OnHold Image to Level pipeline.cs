using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameJom
{
    class ImageProcessor
    {
        public string LevelImageToString(Texture2D LevelData, LevelClass Level)
        {
            Color[] colorData = new Color[LevelData.Width * LevelData.Height];
            LevelData.GetData(colorData);
            string textFormat;
            for (int x = 0; x < LevelData.Width; x++)
            {
                for (int y = 0; y < LevelData.Width; y++)
                {
                    //textFormat += colorData
                }
            }
            return "placeholder";
        }
    }
}
