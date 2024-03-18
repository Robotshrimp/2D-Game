using System;
using System.Security.Policy;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
            if (length == 0) // here to prevent divide by 0 error, also allows the code to function even when points overlap
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
        
        public static Vector2 RotateAround(Vector2 startingPoint, Vector2 rotationOrigin, float rotation, float horizontalRotationScale = 1, float vertialRotationScale = 1)
        {
            return matrixRotation(startingPoint - rotationOrigin, rotation, horizontalRotationScale, vertialRotationScale) + rotationOrigin;

        }
        public static Vector2 matrixRotation(Vector2 startingPoint, float rotation, float horizontalRotationScale = 1, float vertialRotationScale = 1) // finds the location of a point after rotation 
        {
            return new Vector2(
                (float)(startingPoint.X * Math.Cos(rotation) + (startingPoint.Y * -Math.Sin(rotation))) * horizontalRotationScale, 
                (float)(startingPoint.X * Math.Sin(rotation) + (startingPoint.Y * Math.Cos(rotation))) * vertialRotationScale);
        }
        public static Vector2 angleToPoint(float angle, double length, float horizontalScale = 1, float verticalScale = 1)// find the coordnet location of a 2d point when given the angle and distance from origin
        {
            return new Vector2((float)(Math.Cos(angle) * length * horizontalScale), (float)(Math.Sin(angle) * length * verticalScale));
        }
        #region 3d-screen conversions
        public static Vector2 Point3DToAnglePair(Vector3 Coordnet) // finds the angles of deviation from the x axis 
        {
            double XZAngle = Angle2(new Vector2(Coordnet.Z, Coordnet.X));
            double hypotenus = pythag_hypotenus(new Vector2(Coordnet.Z, Coordnet.X));
            return new Vector2((float)XZAngle, Angle2(new Vector2((float)hypotenus, Coordnet.Y))/*this is only for calculating the angle on the z axis, values outside of 90 to -90 degree should be impossible because then the angle on the x-y plain would change instead*/);
        }
        public static Vector3 AnglePairToPoint3D(Vector2 angle)
        {
            Vector2 zxValue = matrixRotation(new Vector2(1, 0), angle.X);
            float yValue = (float)Math.Sin(angle.Y);
            zxValue *= (float)Math.Cos(angle.Y);
            return new Vector3(zxValue.Y, yValue, zxValue.X);
        }
        public static Vector3 rotate3Dw2D(Vector3 focalPoint, float distance,Vector2 rotaion) // generates a 3d point from a distance and the angle from the z axis
        {
            float z = (float)Math.Cos(rotaion.X);
            float y = (float)Math.Cos(rotaion.Y);
            return new Vector3(focalPoint.X - distance * (float)Math.Sin(rotaion.X) * y, focalPoint.Y - distance * (float)Math.Sin(rotaion.Y), focalPoint.Z - distance * (z * y));
        }
        #endregion
        #region 3d point rotation
        public static Vector3 eulerAngles3D(Vector3 startingPoint,Vector3 axelPoint, Vector3 baseRotations)// x is for up down, y is for side to side, z is for turning (not a mistake, x and y are switched because rotation is calculated as rotation around an axis)
        {
            startingPoint -= axelPoint;
            Vector2 xypoint = matrixRotation(new Vector2(startingPoint.X, startingPoint.Z), baseRotations.Y); // yaw
            startingPoint.X = xypoint.X;
            startingPoint.Z = xypoint.Y;
            Vector2 zypoint = matrixRotation(new Vector2(startingPoint.X, startingPoint.Y), baseRotations.Z); // roll
            startingPoint.X = zypoint.X;
            startingPoint.Y = zypoint.Y;
            Vector2 xzpoint = matrixRotation(new Vector2(startingPoint.Y, startingPoint.Z), baseRotations.X); // pitch
            startingPoint.Y = xzpoint.X;
            startingPoint.Z = xzpoint.Y;
            return startingPoint + axelPoint;
        }
        public static Vector3 reverseEulerRotation(Vector3 startingPoint, Vector3 axelPoint, Vector3 baseRotations)
        {
            startingPoint -= axelPoint;
            Vector2 xzpoint = matrixRotation(new Vector2(startingPoint.Y, startingPoint.Z), -baseRotations.X); // pitch
            startingPoint.Y = xzpoint.X;
            startingPoint.Z = xzpoint.Y;
            Vector2 zypoint = matrixRotation(new Vector2(startingPoint.X, startingPoint.Y), -baseRotations.Z); // roll
            startingPoint.X = zypoint.X;
            startingPoint.Y = zypoint.Y;
            Vector2 xypoint = matrixRotation(new Vector2(startingPoint.X, startingPoint.Z), -baseRotations.Y); // yaw
            startingPoint.X = xypoint.X;
            startingPoint.Z = xypoint.Y;
            return startingPoint + axelPoint;

        }
        public static Vector3 RotateLadLon3D(Vector3 startingPoint, Vector3 axelPoint, Vector2 LongditudeLaditude) // useless for now due to north/south pole squishing, might be useful for rotation in a non axial plain
        {
            startingPoint -= axelPoint;
            Vector2 equatorialRotation = matrixRotation(new Vector2(startingPoint.Z, startingPoint.X), LongditudeLaditude.X);
            startingPoint.X = equatorialRotation.Y;
            startingPoint.Z = equatorialRotation.X;
            Vector2 laditudeRotation = matrixRotation(new Vector2(1, startingPoint.Y), LongditudeLaditude.Y);
            startingPoint.X *= laditudeRotation.X;
            startingPoint.Z *= laditudeRotation.X;
            startingPoint.Y = laditudeRotation.Y;
            return startingPoint;
        }
        #region 3D non-cardinal rotation
        public static Vector3 QuaternionRotation(Vector3 startingPoint, float rotation, Vector3 rotationVector, Vector3 originPoint) // returns a point rotated around the the direction of the rotationVector passing through the originPoint
        {
            Quaternion baselineQuat = new Quaternion(startingPoint - originPoint, 0);
            rotationVector.Normalize();
            Quaternion rotationQuat = new Quaternion(rotationVector, (float)Math.Cos(rotation / 2));
            baselineQuat *= rotationQuat; // rotates the startingPoint around the originPoint through the plain prependicular to the rotationVector by the rotation radian
            Vector3 newPoint;
            baselineQuat.Deconstruct(out newPoint.X,out newPoint.Y,out newPoint.Z, out float discard);
            return newPoint;
        }
        public static Vector3 RotateTowards(Vector3 startingPoint, Vector3 axelPoint, Vector3 targetPoint, float newAngle) // rotating on the plane containing the startingPoint, targetPoint, and origin with the rotation being the angle from the startingPoint to the targetPoint
        {
            return new Vector3(); // To Be Implimented
        }
        #endregion
        #endregion
        #region planes
        public static Vector3 PlaneDirection(Vector3 index, Vector3 middle) // Finds the dirction of a plane using two point on the plain when the plane passes through the origin
        {
            Vector3 thumb = Vector3.Cross(index, middle);
            thumb.Normalize();
            return thumb;
        }
        #endregion
    }
}
