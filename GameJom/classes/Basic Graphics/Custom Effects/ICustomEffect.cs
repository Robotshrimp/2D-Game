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
        string Name { get; } // should be set in class implimentation and used to designate JSON deserialization
        void Draw(Rectangle destination, Texture2D texture, Rectangle usedTexture, Color color, float angle = 0); // where a hypothetical basedraw effect would draw the object, if not basedraw, pass this on with alterations
        void Update(); // used for updating game draw functions, used for any effects that involves movement, useful to pause movement without stopping draw functions
        void GroupDraw(); // for draw operations that require stored data. this draw method allows the class a place to draw objects once, if implimented in normal draw objects stored in effect class would draw everytime the draw method was called
    }
}
