using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameJom._3D_Because_Why_Not
{
    class Renderer3D
    {
        static GraphicsDeviceManager graphics = Game1.graphics;
        static Vector3 CameraLocation = new Vector3(0, 0, 0);
        static Vector3 CameraDirection = new Vector3(0, 0, 0);
        AutomatedDraw DrawParam;


        
        int fovHorizontal;
        int fovVertical;

         /* if these values ever diviates from 1: the person configuring the settings is clinically insane and should 
          * be put away in a maximum security mental hostpital or be put down to protect the rest of society, i have
          * made a severe and continious lapse in my judgement and i will not and do not expect god to be merciful*/

        public Renderer3D(AutomatedDraw drawParam, int FovHorozontal = 100, int FovVertical = 0)
        {
            fovHorizontal = FovHorozontal;
            if (FovVertical <= 0)
            {
                fovVertical = (int)(FovHorozontal * (2160.0 / 3840.0));
            }
            this.DrawParam = drawParam;



        }

        

        public void UpdateLocation(Vector3 newLocation)
        {
            CameraLocation = newLocation;
        }
        public void UpdateDirection(Vector3 Rotation) // fix plz UwU *nuzzles*
        {
            CameraDirection = Rotation;
        }
        private Point ScreenProjection(Vector2 Angle)
        {
            float radFOVX = ((float)fovHorizontal / 360) * (float)Math.PI;
            float radFOVY = ((float)fovVertical / 360) * (float)Math.PI;
            Point screenOffset = new Point(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2); // sets 0,0 to center of screen
            Point projectionLocation = new Point((int)(screenOffset.X * (Angle.X / radFOVX)), (int)(screenOffset.Y * (Angle.Y / radFOVY))); // gives angle coordinates an x,y coornet scaled with fov. screen offset is used because i'm a lazy bastard who is too lazy to get the value straight from the source, it has no computational value and probably weakens the code
            return new Point(projectionLocation.X + screenOffset.X, -projectionLocation.Y + screenOffset.Y);// screen offset is applied to center around middle of screen
        }
        public Vector2 CoordnetConvert(Vector3 coordnetLocation)
        {
            coordnetLocation -= CameraLocation;
            Vector2 angleLocation = TrigFun.Angle3(TrigFun.Rotate3D(coordnetLocation, new Vector3(0,0,0), CameraDirection)); // fix screen rotation later
            return angleLocation;
        }

        public void RenderLine(Vector3 start, Vector3 end, int thiccness = 1)
        {
            if (DrawParam.Drawn)
                DrawParam.DrawLine(new LineClass(ScreenProjection(CoordnetConvert(start)), ScreenProjection(CoordnetConvert(end)), thiccness));
        }
        public void RenderSegmentedLine(Vector3 start, Vector3 end, int segments, int thiccness = 1)
        {
            Vector3 beginning = start;
            for(int n = 0; n < segments; n++)
            {
                Vector3 traversed = new Vector3((end.X - start.X) / segments, (end.Y - start.Y) / segments, (end.Z - start.Z) / segments);
                RenderLine(beginning, beginning + traversed, thiccness);
                beginning += traversed;
            }
        }
    }
}
