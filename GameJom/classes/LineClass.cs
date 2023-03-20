using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace GameJom
{
    public class LineClass
    {
        public Point Start { get; private set; }
        public Point End { get; private set; }
        public int thiccness;



        public Point relativePosition;
        public int length;
        public float angle;
        public float slope;
        public float yIntercept;

        public LineClass(Point Start, Point End, int Thiccness = 1)
        {
            this.thiccness = Thiccness;
            this.Start = Start;
            this.End = End;
            this.relativePosition = this.RelativePosition(this.Start, this.End);
            this.length = this.Length(relativePosition);
            this.angle = this.Angle(relativePosition, length);
            this.slope = (float)relativePosition.Y / (float)relativePosition.X;
            this.yIntercept = this.Start.Y - this.Start.X * slope;
        }

        public Point EndFinder(Point Start, float Angle, int Length)
        {
            int x = (int)(Math.Cos(angle) * length);
            int y = (int)(Math.Sin(angle) * length);
            return new Point(x, y);
        }


        //public Rectangle LineSpace()
        //{
        //    return collison.CoveredDistance(new Rectangle(start, new Point(0,0)), end);
        //}
        public Point RelativePosition(Point start, Point end)
        {
            return new Point(end.X - start.X, end.Y - start.Y);
        }
        public int Length(Point RelativePosition)
        {
            return (int)TrigFun.pythag_hypotenus(new Vector2(RelativePosition.X, RelativePosition.Y));// pythagorean theorem hypotenus moment
        }
        public float Angle(Point RelativePosition, int Length)
        {
            float Angle = (float)Math.Asin((double)RelativePosition.Y / Length);
            if (RelativePosition.X < 0)
            {
                Angle = (float)((Math.PI) - Angle);
            }
            return Angle;
        }


        public float Function(float x)
        {
            return (yIntercept + (slope * x));
        }
    }
}
