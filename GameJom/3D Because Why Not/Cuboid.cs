using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
 

namespace GameJom._3D_Because_Why_Not
{
    class Cuboid : TrigFun
    {

        Renderer3D Render;
        public Cuboid(Renderer3D render)
        {
            //loads the engine, vroom vroom
            this.Render = render;
        }
        public void DrawCuboid(Vector3 center, Vector3 dimentions, int segments, int thickness, Vector3 rotation)
        {
            //finding the location of the cube based of the given center variable
            Vector3 location = new Vector3(center.X - (dimentions.X / 2), center.Y - (dimentions.Y / 2), center.Z - (dimentions.Z / 2));
            Vector3 rotationCenter = center;

            //setting location of each cube corner based off given parameters
            Vector3 corner1_1 = RotateAxies3D(new Vector3(location.X + dimentions.X, location.Y + dimentions.Y, location.Z), rotationCenter, rotation);
            Vector3 corner1_2 = RotateAxies3D(new Vector3(location.X, location.Y + dimentions.Y, location.Z), rotationCenter, rotation);
            Vector3 corner1_3 = RotateAxies3D(new Vector3(location.X, location.Y, location.Z), rotationCenter, rotation);
            Vector3 corner1_4 = RotateAxies3D(new Vector3(location.X + dimentions.X, location.Y, location.Z), rotationCenter, rotation);
            Vector3 corner2_1 = RotateAxies3D(new Vector3(location.X + dimentions.X, location.Y + dimentions.Y, location.Z + dimentions.Z), rotationCenter, rotation);
            Vector3 corner2_2 = RotateAxies3D(new Vector3(location.X, location.Y + dimentions.Y, location.Z + dimentions.Z), rotationCenter, rotation);
            Vector3 corner2_3 = RotateAxies3D(new Vector3(location.X, location.Y, location.Z + dimentions.Z), rotationCenter, rotation);
            Vector3 corner2_4 = RotateAxies3D(new Vector3(location.X + dimentions.X, location.Y, location.Z + dimentions.Z), rotationCenter, rotation);

            


            //uses the instance engine to render lines between select cubes
            Render.RenderSegmentedLine(corner1_1, corner2_1, segments, thickness);
            Render.RenderSegmentedLine(corner1_2, corner2_2, segments, thickness);
            Render.RenderSegmentedLine(corner1_4, corner2_4, segments, thickness);
            Render.RenderSegmentedLine(corner1_3, corner2_3, segments, thickness);

            Render.RenderSegmentedLine(corner1_1, corner1_2, segments, thickness);
            Render.RenderSegmentedLine(corner1_1, corner1_4, segments, thickness);
            Render.RenderSegmentedLine(corner1_3, corner1_2, segments, thickness);
            Render.RenderSegmentedLine(corner1_3, corner1_4, segments, thickness);

            Render.RenderSegmentedLine(corner2_1, corner2_2, segments, thickness);
            Render.RenderSegmentedLine(corner2_1, corner2_4, segments, thickness);
            Render.RenderSegmentedLine(corner2_3, corner2_2, segments, thickness);
            Render.RenderSegmentedLine(corner2_3, corner2_4, segments, thickness);

            /*
            Engine.RenderSegmentedLine(corner2_1, corner1_3, segments, thiccness);
            Engine.RenderSegmentedLine(corner2_2, corner1_4, segments, thiccness);
            Engine.RenderSegmentedLine(corner2_3, corner1_1, segments, thiccness);
            Engine.RenderSegmentedLine(corner2_4, corner1_2, segments, thiccness);
            */
        }

    }
}
