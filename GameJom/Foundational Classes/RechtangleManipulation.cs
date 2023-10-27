using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJom
{
    public static class RechtangleManipulation
    {
        public static Rectangle Shrink(Rectangle original, Single shrinkMod)
        {
            int WidthShrink = (int)((float)original.Width * shrinkMod - original.Width);
            int HeightShrink = (int)((float)original.Height * shrinkMod - original.Height);
            return new Rectangle(original.X - (int)(WidthShrink / 2), original.Y - (int)(HeightShrink / 2), original.Width + WidthShrink, original.Height + HeightShrink);
        }
    }
}
