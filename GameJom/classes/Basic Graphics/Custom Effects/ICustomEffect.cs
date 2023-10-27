using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJom
{
    public interface ICustomEffect // replaces the spritbatch draw in DrawLocation 
    {
        void Draw(Rectangle destination, Texture2D texture, Rectangle usedTexture, Color color, float angle = 0, string callKey = null);
    }
}
