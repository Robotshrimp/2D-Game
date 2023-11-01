using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJom
{
    public class Global
    {
        private static SpriteBatch SpriteBatch = Game1.spriteBatch;
        public static SpriteBatch spriteBatch { get { return SpriteBatch; } }

        private static GraphicsDevice GraphicDevice = Game1.graphicsDevice;
        public static GraphicsDevice graphicDevice { get { return GraphicDevice; } }

        private static GraphicsDeviceManager Graphics = Game1.graphics;
        public static GraphicsDeviceManager graphics { get { return Graphics; } }

        public static AfterImage afterImage = new AfterImage(colorKeys: new List<ColorFrameData>(), trailShrinkOff: 1f, afterImageDuration: 5, afterImageLength: 40, trailFadeOff: 0.889f);
    }
}
