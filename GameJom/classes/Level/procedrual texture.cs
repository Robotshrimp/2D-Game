using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameJom
{
    class InfiniTexture
    {
        AutomatedDraw DrawParameter;
        public InfiniTexture(AutomatedDraw drawParameter)
        {
            this.DrawParameter = drawParameter;
        }
        public void griddify(Point cellSize, Texture2D cellTexture, Point gridFocalPoint)
        {
            Rectangle displayLocation = new Rectangle(DrawParameter.Centering - new Point(1920, 1080), new Point(3840, 2160));
            Point lowestTestureLocation;
            lowestTestureLocation.X = (int)Math.Floor((displayLocation.X - gridFocalPoint.X) / (double)cellSize.X);
            lowestTestureLocation.Y = (int)Math.Floor((displayLocation.Y - gridFocalPoint.Y) / (double)cellSize.Y);
            Point highestTextureLocation;
            highestTextureLocation.X = (int)Math.Ceiling((displayLocation.X + displayLocation.Width - gridFocalPoint.X) / (double)cellSize.X);
            highestTextureLocation.Y = (int)Math.Ceiling((displayLocation.Y + displayLocation.Height - gridFocalPoint.Y) / (double)cellSize.Y);
            for (int n = lowestTestureLocation.Y; n < highestTextureLocation.Y; n++)
            {
                for (int m = lowestTestureLocation.X; m < highestTextureLocation.X; m++)
                {
                    DrawParameter.mDraw(new Rectangle(m * cellSize.X, n * cellSize.Y, cellSize.X, cellSize.Y), cellTexture);
                }
            }
        }
    }
}
