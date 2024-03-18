using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJom.Foundational_Classes
{
    internal class GeometricFun
    {
        public static Rectangle VBounds(List<Vector2> points)
        {
            float lowest = points[0].Y;
            float highest = points[0].Y;
            float leftmost = points[0].X;
            float rightmost = points[0].X;
            foreach (Vector2 point in points)
            {
                if (point.X < leftmost)
                    leftmost = point.X;
                else if (point.X > rightmost)
                    rightmost = point.X;
                if (point.Y < highest)
                    highest = point.Y;
                else if (point.Y > lowest)
                    lowest = point.Y;
            }
            return new Rectangle((int)leftmost, (int)highest, (int)(rightmost - leftmost), (int)(lowest - highest));
        }
        public static Rectangle Bounds(List<Point> points)
        {
            int lowest = points[0].Y;
            int highest = points[0].Y;
            int leftmost = points[0].X;
            int rightmost = points[0].X;
            foreach (Point point in points) 
            {
                if (point.X < leftmost)
                    leftmost = point.X;
                else if (point.X > rightmost)
                    rightmost = point.X;
                if (point.Y < highest)
                    highest = point.Y;
                else if (point.Y > lowest)
                    lowest = point.Y;
            }
            return new Rectangle(leftmost, highest, rightmost - leftmost, lowest - highest);
        }
        public static float TriangleAreaFinder(Vector2[] traingleVerticies)
        {
            //WIP, could help ruleout misses in 3D renderer and fill in holes
            return 0;
        }
    }
}
