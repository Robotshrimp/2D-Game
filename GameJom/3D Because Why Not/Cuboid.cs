using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
 

namespace GameJom._3D_Because_Why_Not
{
    class Cuboid : TrigFun
    {

        Renderer3D Engine;
        public Cuboid(Renderer3D engine)
        {
            //loads the engine, vroom vroom
            this.Engine = engine;
        }
        public void DrawCuboid(Vector3 center, Vector3 dementia, int segments, int thiccness, Vector3 rotation)
        {
            //finding the location of the cube based of the given center variable
            Vector3 location = new Vector3(center.X - (dementia.X / 2), center.Y - (dementia.Y / 2), center.Z - (dementia.Z / 2));
            Vector3 rotationCenter = center;

            //setting location of each cube corner based off given parameters
            Vector3 corner1_1 = Rotate3D(new Vector3(location.X + dementia.X, location.Y + dementia.Y, location.Z), rotationCenter, rotation);
            Vector3 corner1_2 = Rotate3D(new Vector3(location.X, location.Y + dementia.Y, location.Z), rotationCenter, rotation);
            Vector3 corner1_3 = Rotate3D(new Vector3(location.X, location.Y, location.Z), rotationCenter, rotation);
            Vector3 corner1_4 = Rotate3D(new Vector3(location.X + dementia.X, location.Y, location.Z), rotationCenter, rotation);
            Vector3 corner2_1 = Rotate3D(new Vector3(location.X + dementia.X, location.Y + dementia.Y, location.Z + dementia.Z), rotationCenter, rotation);
            Vector3 corner2_2 = Rotate3D(new Vector3(location.X, location.Y + dementia.Y, location.Z + dementia.Z), rotationCenter, rotation);
            Vector3 corner2_3 = Rotate3D(new Vector3(location.X, location.Y, location.Z + dementia.Z), rotationCenter, rotation);
            Vector3 corner2_4 = Rotate3D(new Vector3(location.X + dementia.X, location.Y, location.Z + dementia.Z), rotationCenter, rotation);

            


            //uses the instance engine to render lines between select cubes
            Engine.RenderSegmentedLine(corner1_1, corner2_1, segments, thiccness);
            Engine.RenderSegmentedLine(corner1_2, corner2_2, segments, thiccness);
            Engine.RenderSegmentedLine(corner1_4, corner2_4, segments, thiccness);
            Engine.RenderSegmentedLine(corner1_3, corner2_3, segments, thiccness);
            
            Engine.RenderSegmentedLine(corner1_1, corner1_2, segments, thiccness);
            Engine.RenderSegmentedLine(corner1_1, corner1_4, segments, thiccness);
            Engine.RenderSegmentedLine(corner1_3, corner1_2, segments, thiccness);
            Engine.RenderSegmentedLine(corner1_3, corner1_4, segments, thiccness);
            
            Engine.RenderSegmentedLine(corner2_1, corner2_2, segments, thiccness);
            Engine.RenderSegmentedLine(corner2_1, corner2_4, segments, thiccness);
            Engine.RenderSegmentedLine(corner2_3, corner2_2, segments, thiccness);
            Engine.RenderSegmentedLine(corner2_3, corner2_4, segments, thiccness);

            /*
            Engine.RenderSegmentedLine(corner2_1, corner1_3, segments, thiccness);
            Engine.RenderSegmentedLine(corner2_2, corner1_4, segments, thiccness);
            Engine.RenderSegmentedLine(corner2_3, corner1_1, segments, thiccness);
            Engine.RenderSegmentedLine(corner2_4, corner1_2, segments, thiccness);
            */
        }

    }
}
