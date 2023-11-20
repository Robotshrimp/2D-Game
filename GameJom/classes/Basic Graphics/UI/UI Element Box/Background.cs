using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJom
{
    internal class Background : ScreenFromat, IScreen
    {
        Texture2D BGImage;

        public void Draw()
        {
            Camera cam = new Camera();
            cam.Draw(Game1.ScreenBounds, BGImage, Color.White);
        }

        public void Initialize()
        {
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
