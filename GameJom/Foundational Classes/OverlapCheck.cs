using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameJom
{
    class OverlapCheck
    {
        public bool Overlapped(Rectangle Base, Rectangle Selection)
        {
            if (Selection.Bottom > Base.Top &&
                Selection.Left > Base.Right &&
                Selection.Top < Base.Bottom &&
                Selection.Right < Base.Left)
                return true;
            return false;
        }
        public Point IntersectionLocation(LineClass Line1, LineClass Line2)
        {
            float x = Line1.slope - Line2.slope;
            float num = Line2.yIntercept - Line1.slope;
            float location = num / x;

            return new Point((int)location, (int)Line1.Function(location));
        }
        public bool Intersect(LineClass CrossingLine, LineClass TraversedLine)
        {
            if (LineCross(CrossingLine, TraversedLine) && LineCross(CrossingLine, TraversedLine))
            {
                return true;
            }
            return false;
        }
        public bool LineCross(LineClass CrossingLine, LineClass TraversedLine)
        {
            bool StartHigher = false;
            bool EndHigher = false;
            if (CrossingLine.start.Y > TraversedLine.Function(CrossingLine.start.X))
                StartHigher = true;
            if (CrossingLine.end.Y > TraversedLine.Function(CrossingLine.end.X))
                StartHigher = true;
            if (StartHigher != EndHigher)
            {
                return true;
            }
            return false;
        }
        public Rectangle OverlappedArea(Rectangle Base, Rectangle Selection)
        {
            if (Overlapped(Base, Selection))
                return new Rectangle();
            int top = Math.Max(Selection.Top, Base.Top);
            int left = Math.Max(Selection.Left, Base.Left);
            int width = Math.Min(Selection.Right, Base.Right) - left;
            int height = Math.Min(Selection.Bottom, Base.Bottom) - top;
            return new Rectangle(left, top, width, height);
        }
    }
}
