using GameJom.Foundational_Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace GameJom._3D_Because_Why_Not
{
    class Renderer3D
    {
        static GraphicsDeviceManager graphics = Game1.graphics;
        public  Vector3 CameraLocation = new Vector3(0, 0, 0);
        public  Vector3 CameraDirection = new Vector3(0, 0, 0);
        Camera DrawParam;

        /* if these values ever diviates from 0-1: the person configuring the settings is clinically insane and should 
       * be put away in a maximum security mental hostpital or be put down to protect the rest of society, i have
       * made a severe and continious lapse in my judgement and i will not and do not expect god to be merciful*/
        int fovHorizontal;
        int fovVertical;



        public Renderer3D(Camera drawParam, int FovHorozontal = 100, int FovVertical = 0)
        {
            fovHorizontal = FovHorozontal;
            if (FovVertical <= 0)
            {
                fovVertical = (int)(FovHorozontal * ((float)graphics.PreferredBackBufferHeight / (float)graphics.PreferredBackBufferWidth));
            }
            this.DrawParam = drawParam;



        }

        
        // camera manipulation(walter white reference) code
        public void UpdateLocation(Vector3 newLocation)
        {
            CameraLocation = newLocation;
        }
        public void UpdateDirection(Vector3 Rotation)
        {
            CameraDirection = Rotation;
        }
        // advanced camera manipulation 
        public void LookAt(Vector3 location)
        {
            Vector3 relativePos = location - CameraLocation;
            Vector2 direction = TrigFun.Point3DToAnglePair(relativePos);
            UpdateDirection(new Vector3(direction.Y, direction.X, CameraDirection.Z));
        }
        public Point ScreenProjection(Vector2 Angle)
        {
            float radFOVX = ((float)fovHorizontal / 360) * (float)Math.PI;
            float radFOVY = ((float)fovVertical / 360) * (float)Math.PI;
            Point screenOffset = new Point(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2); // sets 0,0 to center of screen
            Point projectionLocation = new Point((int)(screenOffset.X * (Angle.X / radFOVX)), (int)(screenOffset.Y * (Angle.Y / radFOVY))); // gives angle coordinates an x,y coornet scaled with fov. screen offset is used because i'm a lazy bastard who is too lazy to get the value straight from the source, it has no computational value and probably weakens the code
            return new Point(projectionLocation.X + screenOffset.X, -projectionLocation.Y + screenOffset.Y);// screen offset is applied to center around middle of screen
        }
        public Vector3 ScreenPointDirection(Point screenPoint) // gets the direction vector a point on the screen represents
        {
            float radFOVX = ((float)fovHorizontal / 360) * (float)Math.PI;
            float radFOVY = ((float)fovVertical / 360) * (float)Math.PI;
            Point screenOffset = new Point(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);
            float xAngle = (float)(screenPoint.X -screenOffset.X) / screenOffset.X * radFOVX;
            float yAngle = (float)-(screenPoint.Y - screenOffset.Y) / screenOffset.Y * radFOVY;
            Vector3 rayVector = TrigFun.AnglePairToPoint3D(new Vector2(xAngle, yAngle));
            return TrigFun.reverseEulerRotation(rayVector, new Vector3(), CameraDirection); // if camera location is added to the return value, re-projecting the value will yield same screenPoint
        }
        public Vector2 CoordnetConvert(Vector3 coordnetLocation)
        {
            Vector2 angleLocation = TrigFun.Point3DToAnglePair(TrigFun.eulerAngles3D(coordnetLocation - CameraLocation, new Vector3(), CameraDirection)); // fix screen rotation later
            return angleLocation;
        }

        public void RenderLine(Vector3 start, Vector3 end, int thiccness = 1)
        {
            DrawParam.DrawLine(new LineClass(ScreenProjection(CoordnetConvert(start)), ScreenProjection(CoordnetConvert(end)), thiccness));
        }
        public void RenderSegmentedLine(Vector3 start, Vector3 end, int segments, int thiccness = 1)
        {
            Vector3 beginning = start;
            for (int n = 0; n < segments; n++)
            {
                Vector3 traversed = new Vector3((end.X - start.X) / segments, (end.Y - start.Y) / segments, (end.Z - start.Z) / segments);
                RenderLine(beginning, beginning + traversed, thiccness);
                beginning += traversed;
            }
        }
        // TriRendering
        Color[] Render; // pixels from rendering tris are stored here and converted to a Texture2D when set to Rendered
        Texture2D Rendered; // the image of the 3D render that is drawn to screen 
        float[] RayDistance; // congruent to Render and tracks the distance of each pixel/ray
        public bool PlaneRenderingInstaniated = false; // makes sure the renderer has the parameters set for plane rendering, prevents unnescesary parameters for non-plane rendering
        Point RenderingResolution; // a resolution of what's rendered   
        public void InstaniatePlaneRendering(Point renderingResolution) // Instaniate the renderer with nescessary information for plane rendering
        {
            Rendered = new Texture2D(Game1.graphicsDevice, renderingResolution.X, renderingResolution.Y);
            RenderingResolution = renderingResolution;
            Render = new Color[RenderingResolution.X * RenderingResolution.Y]; // sets the confines of a color array used for composing the 2d texture of the render
            RayDistance = new float[RenderingResolution.X * RenderingResolution.Y];
            PlaneRenderingInstaniated = true;
        }
        public void RenderAll(Rectangle renderLocation) // only in charge of drawing the already made 2d texture image
        {
                Rendered.SetData(Render);
                DrawParam.Draw(renderLocation, Rendered);
        }
        public Point ScaleToResolution(Point preScale)
        {
            return new Point((int)((float)preScale.X / graphics.PreferredBackBufferWidth * RenderingResolution.X),
                (int)((float)preScale.Y / graphics.PreferredBackBufferHeight * RenderingResolution.Y));

        }
        public Point UnscaleResolution(Point preScale)
        {
            return new Point((int)((float)preScale.X /  RenderingResolution.X * graphics.PreferredBackBufferWidth),
                (int)((float)preScale.Y / RenderingResolution.Y * graphics.PreferredBackBufferHeight ));
        }
        public void RenderTri(TriPlane tri) // only adds the tri to the render image, does not draw anything on screen to prevent tri intersection ambiguity
        {
            if (PlaneRenderingInstaniated)
            {
                List<Point> points = new List<Point>();
                points.Add(ScaleToResolution(ScreenProjection(CoordnetConvert(tri.Point1))));
                points.Add(ScaleToResolution(ScreenProjection(CoordnetConvert(tri.Point2))));
                points.Add(ScaleToResolution(ScreenProjection(CoordnetConvert(tri.Point3))));
                Rectangle Bounds = GeometricFun.Bounds(points);


                Vector3 direction = tri.SeenSideDirection(CameraLocation);
                int recurance = 0;
                for (int y = 0; y < Bounds.Height; y++)
                {
                    for (int x = 0; x < Bounds.Width; x++)
                    {
                        Vector3 ray = ScreenPointDirection(UnscaleResolution(new Point(x + Bounds.X, y + Bounds.Y)));
                        // ray is vector from camera location to 1 away in a direction vector
                        float distance;
                        Vector3 intersectionPoint = tri.PlaneIntersection(CameraLocation, ray, out distance);
                        // intersection point is from origin to intersection point
                        // ray is not Q, it is a vector point towards it

                        if (tri.Inside(intersectionPoint + CameraLocation))
                        {
                            recurance++;
                            int pixelNum = (y + Bounds.Y) * RenderingResolution.X + (x + Bounds.X);
                            if (RayDistance[pixelNum] == 0 ||
                                distance < RayDistance[pixelNum]) // checks to see if the new pixel is closer than the current clossest pixel, the distance of each pixel starts at 0 and because of how unlikly it is for a float to land on 0, 0 is defacto null and singled out 
                            {
                                Render[pixelNum] = new Color((125 + (125 * direction.X)) / 255, (125 + (125 * direction.Y)) / 255, (125 + (125 * direction.Z)) / 255);
                                RayDistance[pixelNum] = distance;
                            }
                        }
                    }
                }
                recurance++;
            }
        }
    }
}
