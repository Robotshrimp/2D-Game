using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
 

namespace GameJom._3D_Because_Why_Not
{
    class Cuboid : TrigFun
    {
        public void DrawCuboid(Vector3 location, Vector3 dementia, int fov, int segments, int thiccness, Vector3 rotation)
        {
            _3D_Renderer _3DEngine = new _3D_Renderer(fov);
            Vector3 center = new Vector3(location.X + (dementia.X / 2), location.Y + (dementia.Y / 2), location.Z + (dementia.Z / 2));
            Vector3 rotationCenter = center;
            Vector3 corner1_1 = Rotate3D(new Vector3(location.X + dementia.X, location.Y + dementia.Y, location.Z), rotationCenter, rotation);
            Vector3 corner1_2 = Rotate3D(new Vector3(location.X, location.Y + dementia.Y, location.Z), rotationCenter, rotation);
            Vector3 corner1_3 = Rotate3D(new Vector3(location.X, location.Y, location.Z), rotationCenter, rotation);
            Vector3 corner1_4 = Rotate3D(new Vector3(location.X + dementia.X, location.Y, location.Z), rotationCenter, rotation);
            Vector3 corner2_1 = Rotate3D(new Vector3(location.X + dementia.X, location.Y + dementia.Y, location.Z + dementia.Z), rotationCenter, rotation);
            Vector3 corner2_2 = Rotate3D(new Vector3(location.X, location.Y + dementia.Y, location.Z + dementia.Z), rotationCenter, rotation);
            Vector3 corner2_3 = Rotate3D(new Vector3(location.X, location.Y, location.Z + dementia.Z), rotationCenter, rotation);
            Vector3 corner2_4 = Rotate3D(new Vector3(location.X + dementia.X, location.Y, location.Z + dementia.Z), rotationCenter, rotation);

            



            _3DEngine.RenderSegmentedLine(corner1_1, corner2_1, segments, thiccness);
            _3DEngine.RenderSegmentedLine(corner1_2, corner2_2, segments, thiccness);
            _3DEngine.RenderSegmentedLine(corner1_4, corner2_4, segments, thiccness);
            _3DEngine.RenderSegmentedLine(corner1_3, corner2_3, segments, thiccness);

            _3DEngine.RenderSegmentedLine(corner1_1, corner1_2, segments, thiccness);
            _3DEngine.RenderSegmentedLine(corner1_1, corner1_4, segments, thiccness);
            _3DEngine.RenderSegmentedLine(corner1_3, corner1_2, segments, thiccness);
            _3DEngine.RenderSegmentedLine(corner1_3, corner1_4, segments, thiccness);

            _3DEngine.RenderSegmentedLine(corner2_1, corner2_2, segments, thiccness);
            _3DEngine.RenderSegmentedLine(corner2_1, corner2_4, segments, thiccness);
            _3DEngine.RenderSegmentedLine(corner2_3, corner2_2, segments, thiccness);
            _3DEngine.RenderSegmentedLine(corner2_3, corner2_4, segments, thiccness);
        }
    }
}
