using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace GameJom
{
    class LineClass
    {

        Collision collison;
        static SpriteBatch spriteBatch = Game1.spriteBatch;
        public Point start { get; private set; }
        public Point end { get; private set; }
        public int thiccness;



        Point relativePosition;
        public int length;
        public float angle;
        public float slope;
        public float yIntercept;

        public LineClass(Point Start, Point End, int Thiccness = 1)
        {
            this.thiccness = Thiccness;
            this.start = Start;
            this.end = End;
            this.relativePosition = this.RelativePosition(start, end);
            this.length = this.Length(relativePosition);
            this.angle = this.Angle(relativePosition, length);
            this.slope = (float)relativePosition.Y / (float)relativePosition.X;
            this.yIntercept = start.Y - start.X * slope;
        }

        public Point endFinder(Point Start, float Angle, int Length)
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
        
        public void DrawLine()
        {
            // TODO add integration for automated draw
            spriteBatch.Begin();
            spriteBatch.Draw(Game1.BasicTexture, null, new Rectangle(start.X, start.Y, length, thiccness), null, new Vector2(0, 0), angle, null, Color.White);
            spriteBatch.End();
        }
    }
}
