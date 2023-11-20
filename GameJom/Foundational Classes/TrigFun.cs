using System;
using System.Windows.Forms;
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
        public static Vector2 angletToPoint(float angle, double length, float horizontalScale = 1, float verticalScale = 1)// find the coordnet location of a 2d point when given the angle and distance from origin
        {
            return new Vector2((float)(Math.Cos(angle) * length * horizontalScale), (float)(Math.Sin(angle) * length * verticalScale));
        }
        #region 3d-screen conversions
        public static Vector2 Angle3D(Vector3 Coordnet) // finds the angles of deviation from the x axis 
        {
            double XZAngle = Angle2(new Vector2(Coordnet.Z, Coordnet.X));
            double hypotenus = pythag_hypotenus(new Vector2(Coordnet.Z, Coordnet.X));
            return new Vector2((float)XZAngle, Angle2(new Vector2((float)hypotenus, Coordnet.Y))/*this is only for calculating the angle on the z axis, values outside of 90 to -90 degree should be impossible because then the angle on the x-y plain would change instead*/);
        }
        public static Vector3 rotate3Dw2D(Vector3 focalPoint, float distance,Vector2 rotaion) // generates a 3d point from a distance and the angle from the z axis
        {
            float z = (float)Math.Cos(rotaion.X);
            float y = (float)Math.Cos(rotaion.Y);
            return new Vector3(focalPoint.X - distance * (float)Math.Sin(rotaion.X) * y, focalPoint.Y - distance * (float)Math.Sin(rotaion.Y), focalPoint.Z - distance * (z * y));
        }
        #endregion
        #region 3d point rotation
        public static Vector3 RotateAxies3D(Vector3 startingPoint,Vector3 focalPoint, Vector3 baseRotations)// x is for camera up down, y is for camera side to side, z is for turning the camera
        {
            startingPoint -= focalPoint;
            Vector2 zypoint = rotate(new Vector2(startingPoint.X, startingPoint.Y), baseRotations.Z);
            startingPoint.X = zypoint.X;
            startingPoint.Y = zypoint.Y;
            Vector2 xypoint = rotate(new Vector2(startingPoint.X, startingPoint.Z), baseRotations.Y);
            startingPoint.X = xypoint.X;
            startingPoint.Z = xypoint.Y;
            Vector2 xzpoint = rotate(new Vector2(startingPoint.Y, startingPoint.Z), baseRotations.X);
            startingPoint.Y = xzpoint.X;
            startingPoint.Z = xzpoint.Y;
            return startingPoint + focalPoint;
        }
        public static Vector3 RotateLadLon3D(Vector3 startingPoint, Vector3 focalPoint, Vector2 LongditudeLaditude) // useless for now due to north/south pole squishing, might be useful for rotation in a non axial plain
        {
            startingPoint -= focalPoint;
            Vector2 equatorialRotation = rotate(new Vector2(startingPoint.Z, startingPoint.X), LongditudeLaditude.X);
            startingPoint.X = equatorialRotation.Y;
            startingPoint.Z = equatorialRotation.X;
            Vector2 laditudeRotation = rotate(new Vector2(1, startingPoint.Y), LongditudeLaditude.Y);
            startingPoint.X *= laditudeRotation.X;
            startingPoint.Z *= laditudeRotation.X;
            startingPoint.Y = laditudeRotation.Y;
            return startingPoint;
        }
        public static Vector3 RotateNonAxialPlain(Vector3 startingPoint, Vector3 focalPoint, Vector2 LongditudeLaditude)
        {
            return new Vector3();
        }
        #endregion


        public static Vector2 rotate(Vector2 startingPoint, float rotation, float horizontalRotationScale = 1, float vertialRotationScale = 1) // finds the location of a point after rotation
        {
            double length = pythag_hypotenus(startingPoint);
            float startingRotation = Angle2(startingPoint); // angle of rotation for the original point
            Vector2 endPoint = angletToPoint(startingRotation + rotation, length, horizontalRotationScale, vertialRotationScale);

            return endPoint;
        }
    }
}
