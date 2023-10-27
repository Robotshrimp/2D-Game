using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJom.classes.Basic_Graphics.Custom_Effects
{
    internal class BaseDraw : Global, ICustomEffect
    {
        public void Draw(Rectangle destination, Texture2D texture, Rectangle usedTexture, Color color, float angle = 0, string callKey = null)
        {
            spriteBatch.Draw(texture, destinationRectangle: destination, rotation: angle, sourceRectangle: usedTexture, color: color);
        }
    }
}
