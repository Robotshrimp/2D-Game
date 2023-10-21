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
    public class GridTexture
    {
        AutomatedDraw DrawParameter;
        public Rectangle Grid;
        public GridTexture(AutomatedDraw drawParameter, Rectangle grid)
        {
            this.DrawParameter = drawParameter;
            this.Grid = grid;
        }
        public Point GridPoint(Point input) //converts a point into the tile coordinate the point is on
        {
            Point snapToPoint;
            snapToPoint.X = (int)Math.Floor((input.X - Grid.X) / (double)Grid.Width);
            snapToPoint.Y = (int)Math.Floor((input.Y - Grid.Y) / (double)Grid.Width);
            return snapToPoint;
        }
        public Rectangle GridRectangle(Rectangle input)
        {
            if (input.Width < 0)
            {
                input.X += input.Width;
                input.Width *= -1;
            }
            if (input.Height < 0)
            {
                input.Y += input.Height;
                input.Height *= -1;
            }

            return new Rectangle(GridPoint(input.Location), GridPoint(input.Location + input.Size) - GridPoint(input.Location) + new Point(1,1));/*the 1,1 point is here so it doesn't round down into the display location*/

        }
        public Rectangle GridToScreen(Rectangle gridRec)
        {
            return new Rectangle(gridRec.X * Grid.Width, gridRec.Y * Grid.Height, gridRec.Width * Grid.Width, gridRec.Height * Grid.Height);
        }
        public void Griddify(Texture2D cellTexture)
        {
            Rectangle displayLocation = DrawParameter.CalculationRectangle(DrawParameter.DisplayLocation); //centers a rectangle the sizde of display location on the 
            Rectangle gridRec = GridRectangle(displayLocation);
            for (int n = gridRec.Top; n < gridRec.Bottom; n++)
            {
                for (int m = gridRec.Left; m < gridRec.Right; m++)
                {
                    DrawParameter.Draw(GridToScreen(new Rectangle(m, n, 1, 1)), cellTexture);
                }
            }
        }
        public void ModularTexture(Texture2D wallTexture, Rectangle gridArea)
        {
            for (int n = gridArea.Top; n < gridArea.Bottom; n++)
            {
                for (int m = gridArea.Left; m < gridArea.Right; m++)
                {
                    Point Selection = new Point(1, 1);
                    if (n == gridArea.Top)
                        Selection.Y -= 1;
                    if (n == gridArea.Bottom - 1)
                        Selection.Y += 1;
                    if (m == gridArea.Left)
                        Selection.X -= 1;
                    if (m == gridArea.Right - 1)
                        Selection.X += 1;
                    Point dividedSize = new Point((int)(wallTexture.Width / 3), (int)(wallTexture.Height / 3));
                    Point remainderSize = new Point(wallTexture.Width - (2 * dividedSize.X), wallTexture.Height - (2 * dividedSize.Y));
                    DrawParameter.Draw(GridToScreen(new Rectangle(m, n, 1, 1)), wallTexture, new Rectangle(Selection.X * dividedSize.X, Selection.Y * dividedSize.Y, remainderSize.X, remainderSize.X), Color.White);
                }
            }

        }
    }
}
