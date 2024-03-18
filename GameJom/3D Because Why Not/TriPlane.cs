using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Reflection;

namespace GameJom._3D_Because_Why_Not
{
    class TriPlane // a triangle based plane system
    {

        //TODO : make private points for setting all new points without calculating plane 3 times
        private Vector3 point1;
        public Vector3 Point1
        {
            // gets the normal value of the point but sets both this point and update the direction of the plane
            get
            { 
                return point1;  
            } 
            set 
            { 
                point1 = value;
                Direction = TrigFun.PlaneDirection(Point1 - Point3, Point2 - Point3); 
            }
        }
        private Vector3 point2;
        public Vector3 Point2
        {
            // gets the normal value of the point but sets both this point and update the direction of the plane
            get
            {
                return point2;
            }
            set
            {
                point2 = value;
                Direction = TrigFun.PlaneDirection(Point1 - Point3, Point2 - Point3);
            }
        }
        private Vector3 point3;
        public Vector3 Point3
        {
            // gets the normal value of the point but sets both this point and update the direction of the plane
            get
            {
                return point3;
            }
            set
            {
                point3 = value;
                Direction = TrigFun.PlaneDirection(Point1 - Point3, Point2 - Point3);
            }
        }
        public Vector3 Direction { get; set; }
        public TriPlane(Vector3 point1, Vector3 point2, Vector3 point3)
        { 
            Point1 = point1;
            Point2 = point2;
            Point3 = point3;
            Direction = TrigFun.PlaneDirection(point2 - point1, point3 - point1);
        }
        public Vector3 SeenSideDirection(Vector3 ViewPoint) // returns the direction the plane is facing from the ViewPoint, makes sure the drawn object shading is based off the correct side
        {
            return Direction * SideOf(ViewPoint);
        }
        public int SideOf(Vector3 Point) // returns 1 if the direction vector is already facing the point, -1 if not
        {
            if (Vector3.Dot(Point - Point1, Direction) < 0) // the Point needs to be subtracted by any point on the plane to get it's direction relative to the plane. the exact point doesn't matter since we are calculating for the side of the whole plane on which the three points are only arbitrary 
                return -1;
            return 1;
        }
        public bool Inside(Vector3 point)
        {
            if (Vector3.Dot(Vector3.Cross(Point2 - Point1, point - Point1), Direction) >= 0 &&
                Vector3.Dot(Vector3.Cross(Point3 - Point2, point - Point2), Direction) >= 0 &&
                Vector3.Dot(Vector3.Cross(Point1 - Point3, point - Point3), Direction) >= 0)
                return true;
            return false;
        }
        public Vector3 PlaneIntersection(Vector3 rayStartPoint, Vector3 rayDirection, out float distance)
        {
            rayDirection.Normalize();
            distance = Vector3.Dot((rayDirection - Direction), rayStartPoint) / Vector3.Dot(Direction, rayDirection);
            return rayDirection * distance;
        }
    }
}
