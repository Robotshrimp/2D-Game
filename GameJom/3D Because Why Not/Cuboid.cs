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
        public void DrawCuboid(Vector3 center, Vector3 dimentions, int segments, int thickness, Vector3 rotationVector, float rotation = 0)
        {
            //finding the location of the cube based of the given center variable
            Vector3 location = new Vector3(center.X - (dimentions.X / 2), center.Y - (dimentions.Y / 2), center.Z - (dimentions.Z / 2)); // the close upper left corner of the unrotated cube

            //setting location of each cube corner based off given parameters
            Vector3 corner1_1 = eulerAngles3D(new Vector3(location.X + dimentions.X, location.Y + dimentions.Y, location.Z), center, rotationVector);
            Vector3 corner1_2 = eulerAngles3D(new Vector3(location.X, location.Y + dimentions.Y, location.Z), center, rotationVector);
            Vector3 corner1_3 = eulerAngles3D(new Vector3(location.X, location.Y, location.Z), center, rotationVector);
            Vector3 corner1_4 = eulerAngles3D(new Vector3(location.X + dimentions.X, location.Y, location.Z), center, rotationVector);
            Vector3 corner2_1 = eulerAngles3D(new Vector3(location.X + dimentions.X, location.Y + dimentions.Y, location.Z + dimentions.Z), center, rotationVector);
            Vector3 corner2_2 = eulerAngles3D(new Vector3(location.X, location.Y + dimentions.Y, location.Z + dimentions.Z), center, rotationVector);
            Vector3 corner2_3 = eulerAngles3D(new Vector3(location.X, location.Y, location.Z + dimentions.Z), center, rotationVector);
            Vector3 corner2_4 = eulerAngles3D(new Vector3(location.X + dimentions.X, location.Y, location.Z + dimentions.Z), center, rotationVector);

            


            //uses the instance engine to render lines between select cubes
                Render.RenderTri(new TriPlane(corner1_1, corner1_2, corner1_3));
                Render.RenderTri(new TriPlane(corner1_1, corner1_4, corner1_3));

                Render.RenderTri(new TriPlane(corner2_2, corner2_1, corner2_4));
                Render.RenderTri(new TriPlane(corner2_2, corner2_3, corner2_4));

                Render.RenderTri(new TriPlane(corner1_1, corner1_2, corner2_2));
                Render.RenderTri(new TriPlane(corner1_1, corner2_1, corner2_2));

                Render.RenderTri(new TriPlane(corner1_3, corner1_2, corner2_2));
                Render.RenderTri(new TriPlane(corner1_3, corner2_3, corner2_2));

                Render.RenderTri(new TriPlane(corner1_1, corner2_1, corner2_4));
                Render.RenderTri(new TriPlane(corner1_1, corner1_4, corner2_4));

                Render.RenderTri(new TriPlane(corner1_3, corner1_4, corner2_4));
                Render.RenderTri(new TriPlane(corner1_3, corner2_3, corner2_4));

            /*
            Engine.RenderSegmentedLine(corner2_1, corner1_3, segments, thiccness);
            Engine.RenderSegmentedLine(corner2_2, corner1_4, segments, thiccness);
            Engine.RenderSegmentedLine(corner2_3, corner1_1, segments, thiccness);
            Engine.RenderSegmentedLine(corner2_4, corner1_2, segments, thiccness);
            */
        }
        // add a system to tie each point to a vector relative to the center of the object and auto rotate based off that
        public Vector3[] rotateList(Vector3[] vectorList, Vector3 rotationVector, float rotation)
        {
            return new Vector3[3]; // temp holder
        }
    }
}
