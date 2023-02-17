using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameJom
{
    class AutomatedDraw : Game1
    {
        static GraphicsDeviceManager graphics = Game1.graphics;
        //constructor variables

        Vector Centering;
        Vector UiOffset;
        double Zoom;
        Color Color;
        bool Drawn;

        // constructor, this takes the camera properties as paramters

        public AutomatedDraw(Vector centering, Vector uiOffset, Color color, bool drawn = true, double zoom = 1)
        {
            this.Centering = centering;
            this.UiOffset = uiOffset;
            this.Zoom = zoom;
            this.Color = color;
            this.Drawn = drawn;
        }

        // overloads for constructor

        public AutomatedDraw(Vector uiOffset, Color color, bool drawn = true, double zoom = 1)
            : this(new Vector(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2), uiOffset, color, drawn, zoom) { 
        }
        public AutomatedDraw(bool drawn = true, double zoom = 1)
            : this(new Vector(0 ,0), Color.White, drawn, zoom) { 
        }

        // the draw code that coombines individrual draw parameters and constructor parameters and draws the result

        public void draw(Rectangle locationShape, Texture2D texture, Rectangle usedTexture)
        {
            if (Drawn)
            {
                spriteBatch.Begin();

                // the size, shape, and location of the object on the screen

                Rectangle Processed = new Rectangle(
                    (int)(((locationShape.X - Centering.X) * Zoom) + (graphics.PreferredBackBufferWidth / 2) + UiOffset.X * ScreenSizeAdjustment),
                    (int)(((locationShape.Y - Centering.Y) * Zoom) + (graphics.PreferredBackBufferHeight / 2) + UiOffset.Y * ScreenSizeAdjustment),
                    (int)((locationShape.Width) * Zoom * ScreenSizeAdjustment),
                    (int)((locationShape.Height) * Zoom * ScreenSizeAdjustment));

                // code that stops drawing objects that are offscreen

                if (!(Processed.Right < 0 || Processed.Left > graphics.PreferredBackBufferWidth
                    || Processed.Bottom < 0 || Processed.Top > graphics.PreferredBackBufferHeight))
                {
                spriteBatch.Draw(texture, Processed, usedTexture, Color);
                }
                spriteBatch.End();
            }
        }

        // overload

        public void draw(Rectangle locationShape, Texture2D texture)
        {
            draw(locationShape, texture, new Rectangle(0, 0, texture.Width, texture.Height));
        }
    }
}
