using System;
using Microsoft.Xna.Framework;

namespace GameJom
{
    class TrigFun
    {
        public static double pythag_hypotenus(Vector2 Bases)
        {
            return Math.Sqrt(Bases.X * Bases.X + Bases.Y * Bases.Y);
        }
        public static float DistanceFinder(Vector3 start, Vector3 end)
        {
            Vector3 diff = start - end;
            return (float)pythag_hypotenus(new Vector2((float)pythag_hypotenus(new Vector2(diff.X, diff.Y)), diff.Z));

        }
        public static float Angle2(Vector2 Coordnet) // find the angle of the 2d coordnet from the positive x axis
        {
            double length = pythag_hypotenus(Coordnet);
            if (length == 0)
            {
                return 0;
            }
            float rotation = (float)Math.Asin((double)Coordnet.Y / length);
            if (Coordnet.X < 0)
            {
                rotation = (float)((Math.PI) - rotation);
            }
            return rotation;
        }
        public static Vector2 angletToPoint(float angle, double length)// find the coordnet location of a 2d point when given the angle and distance from origin
        {
            return new Vector2((float)(Math.Cos(angle) * length), (float)(Math.Sin(angle) * length));
        }
        public static Vector2 Angle3(Vector3 Coordnet) // finds the angles of deviation from the x axis 
        {
            double XZAngle = Angle2(new Vector2(Coordnet.Z, Coordnet.X));
            double hypotenus = pythag_hypotenus(new Vector2(Coordnet.Z, Coordnet.X));
            return new Vector2((float)XZAngle, Angle2(new Vector2((float)hypotenus, Coordnet.Y))/*this is only for calculating the angle on the z axis, values outside of 90 to -90 degree should be impossible because then the angle on the x-y plain would change instead*/);
        }
        public static Vector3 Rotate3D(Vector3 staringPoint,Vector3 focalPoint, Vector3 baseRotations)
        {
            staringPoint -= focalPoint;
            Vector2 zypoint = rotate(new Vector2(staringPoint.Z, staringPoint.Y), baseRotations.Z);
            staringPoint.Z = zypoint.X;
            staringPoint.Y = zypoint.Y;
            Vector2 xypoint = rotate(new Vector2(staringPoint.X, staringPoint.Y), baseRotations.Y);
            staringPoint.X = xypoint.X;
            staringPoint.Y = xypoint.Y;
            Vector2 xzpoint = rotate(new Vector2(staringPoint.X, staringPoint.Z), baseRotations.X);
            staringPoint.X = xzpoint.X;
            staringPoint.Z = xzpoint.Y;
            return staringPoint + focalPoint;
        }
        public static Vector3 rotate3Dw2D(Vector3 focalPoint, float distance,Vector2 rotaion)
        {
            float z = (float)Math.Cos(rotaion.X);
            float y = (float)Math.Cos(rotaion.Y);
            return new Vector3(focalPoint.X - distance * (float)Math.Sin(rotaion.X) * y, focalPoint.Y - distance * (float)Math.Sin(rotaion.Y), focalPoint.Z - distance * (z * y));
        }
        public static Vector2 rotate(Vector2 startingPoint, float rotation) // finds the location of a point after rotation
        {
            double length = pythag_hypotenus(startingPoint);
            float startingRotation = Angle2(startingPoint); // angle of rotation for the original point
            Vector2 endPoint = angletToPoint(startingRotation + rotation, length);

            return endPoint;
        }
    }
}
