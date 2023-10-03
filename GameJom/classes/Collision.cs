using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace GameJom
{
    class Collision
    {
        public Collision(float Bounce)
        {

        }
        public bool CollisionCheck(Rectangle HeavyRectangle, Point HeavyRectangleMovement, Rectangle LightRectangle, ref Point LightRectangleMovement)
        {
            Rectangle HeavyRectangleDestination = new Rectangle(HeavyRectangle.X + HeavyRectangleMovement.X, HeavyRectangle.Y + HeavyRectangleMovement.Y, HeavyRectangle.Width, HeavyRectangle.Height);
            Rectangle LightRectangleDestination = new Rectangle(LightRectangle.X + LightRectangleMovement.X, LightRectangle.Y + LightRectangleMovement.Y, LightRectangle.Width, LightRectangle.Height);
            LineClass HeavyRectanglePath = new LineClass(HeavyRectangle.Center, HeavyRectangle.Center + HeavyRectangleMovement);
            LineClass LightRectanglePath = new LineClass(LightRectangle.Center, LightRectangle.Center + LightRectangleMovement);
            if (OverlapCheck.Overlapped(HeavyRectangle, CoveredDistance(LightRectangle, LightRectangleMovement)))
            {
                
            }
            if (OverlapCheck.Overlapped(HeavyRectangle, CoveredDistance(LightRectangle, LightRectangleMovement)))
            {

            }
            return false;
        }
        public void CollisionCalculations(Rectangle MovingRectangle, Rectangle StationalyRectangle, ref Point Movement)
        {
            
        }
        private Point pointCollisionSlanted(Point Corner, LineClass Side, ref Point Direction) // unfinished
        {

            LineClass MovementLine = new LineClass(Corner, Corner + Direction);
            if (OverlapCheck.CheckIntersect(MovementLine, Side))
            {
                Point intersectionLocation = OverlapCheck.IntersectionLocation(MovementLine, Side);
                float angle = Side.angle - MovementLine.angle;
                float newMovement = (float)(Math.Cos(angle) * MovementLine.length);
                
                Point newLocation = Side.EndFinder(intersectionLocation, angle, (int)(Math.Cos(angle) * new LineClass(intersectionLocation, MovementLine.End).length)); 
            }
            return Corner + Direction;
        }
        public Rectangle CoveredDistance(Rectangle StartLocation, Point TravelDistance)
        {
            Rectangle destination = new Rectangle(StartLocation.X + TravelDistance.X, StartLocation.Y + TravelDistance.Y, StartLocation.Width, StartLocation.Height);
            int left = Math.Min(StartLocation.Left, destination.Left);
            int right = Math.Max(StartLocation.Right, destination.Right);
            int top = Math.Min(StartLocation.Top, destination.Top);
            int bottom = Math.Max(StartLocation.Bottom, destination.Bottom);
            int width = right - left;
            int height = bottom - top;
            return new Rectangle(left, top, width, height);
        }
    }
}
