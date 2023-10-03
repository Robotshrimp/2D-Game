using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameJom
{
    static class OverlapCheck
    {
        static public bool Overlapped(Rectangle Base, Rectangle Selection) // boolian value on if the rectangles overlap
        {
            if (Selection.Bottom > Base.Top &&
                Selection.Left > Base.Right &&
                Selection.Top < Base.Bottom &&
                Selection.Right < Base.Left)
                return true;
            return false;
        }
        static public Point IntersectionLocation(LineClass Line1, LineClass Line2) // point denoting where two lines intersect if they continued indefinitly
        {
            float x = Line1.slope - Line2.slope;
            float num = Line2.yIntercept - Line1.slope;
            float location = num / x;

            return new Point((int)location, (int)Line1.LinearFunction(location));
        }
        static public bool CheckIntersect(LineClass CrossingLine, LineClass TraversedLine) // bool denoting if two lines intercept, needs to be checked before checking intersection between two line segments as Intersection location assumes line go on indefinitly
        {
            bool StartHigher = false;
            bool EndHigher = false;
            if (CrossingLine.Start.Y > TraversedLine.LinearFunction(CrossingLine.Start.X))
                StartHigher = true;
            if (CrossingLine.End.Y > TraversedLine.LinearFunction(CrossingLine.End.X))
                StartHigher = true;
            if (StartHigher != EndHigher)
            {
                return true;
            }
            return false;
        }
        static public Rectangle OverlappedArea(Rectangle Base, Rectangle Selection) // gets the overlaping area between two rectangles
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
