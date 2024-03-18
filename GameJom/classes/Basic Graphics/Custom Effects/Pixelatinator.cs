using GameJom.Foundational_Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJom.classes.Basic_Graphics.Custom_Effects
{
    public class Pixelatinator : ICustomEffect 
    {
        public string Name { get; } = "Pixelate";
        public Point resolution { get; set; } = new Point(); // sets the resolution for the pixilator to scale to


        public void Draw(Rectangle destination, Texture2D texture, Rectangle usedTexture, Color color, float angle = 0)
        {

            List<Vector2> rotatedBounds = new List<Vector2>();
            rotatedBounds.Add(new Vector2(destination.Left, destination.Top)); // upper left, left as is due to rotation happening around the upper left corner
            rotatedBounds.Add(TrigFun.RotateAround(new Vector2(destination.Right, destination.Bottom), new Vector2(destination.Top, destination.Left), angle)); // lower right
            rotatedBounds.Add(TrigFun.RotateAround(new Vector2(destination.Right, destination.Top), new Vector2(destination.Top, destination.Left), angle)); // upper right
            rotatedBounds.Add(TrigFun.RotateAround(new Vector2(destination.Left, destination.Bottom), new Vector2(destination.Top, destination.Left), angle)); // lower left

            Rectangle bounds = GeometricFun.VBounds(rotatedBounds);
            //calculate location all verticies and get the highest in each direction
        }

        public void GroupDraw()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
