using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameJom
{
    class AutomatedDraw
    {

        static SpriteBatch spriteBatch = Game1.spriteBatch;
        static GraphicsDeviceManager graphics = Game1.graphics;
        static double ScreenSizeAdjustment = Game1.ScreenSizeAdjustment;
        static Rectangle screenBounds = Game1.ScreenBounds;
        static Vector correctScreenSize = Game1.correctScreenSize;
        //constructor variables

        Vector Centering;
        Vector UiOffset;
        double Zoom;
        Color Color;
        bool Drawn;
        // constructor, this takes the camera properties as paramters

        public AutomatedDraw(Vector centering, Color color, Vector uiOffset, bool drawn = true, double zoom = 1)
        {
            this.Centering = centering;
            this.UiOffset = new Vector(uiOffset.X + screenBounds.X, uiOffset.Y + screenBounds.Y);
            this.Zoom = zoom;
            this.Color = color;
            this.Drawn = drawn;
        }

        // overloads for constructor

        public AutomatedDraw(Color color, Vector uiOffset, bool drawn = true, double zoom = 1)
            : this(new Vector(

                //get's middle of screen

                (correctScreenSize.X / 2), 
                (correctScreenSize.Y / 2)), 
                  
                  color, uiOffset,  drawn, zoom) { 
        }
        public AutomatedDraw(bool drawn = true, double zoom = 1)
            : this( Color.White,new Vector(0 ,0), drawn, zoom) { 
        }

        // the draw code that coombines individrual draw parameters and constructor parameters and draws the result

        public void draw(Rectangle locationShape, Texture2D texture, Rectangle usedTexture, Color color)
        {
            if (Drawn)
            {



                // the size, shape, and location of the object on the screen

                Rectangle Processed = DisplayRectangle(locationShape);

                // code that stops drawing objects that are offscreen

                if (!(Processed.Right < 0 || Processed.Left > graphics.PreferredBackBufferWidth
                    || Processed.Bottom < 0 || Processed.Top > graphics.PreferredBackBufferHeight))
                {
                    spriteBatch.Begin();
                    spriteBatch.Draw(texture, Processed, usedTexture, color);
                    spriteBatch.End();
                }
            }
        }
        public Rectangle DisplayRectangle(Rectangle locationShape)
        {
            return new Rectangle(

                    (int)(((locationShape.X - Centering.X) * Zoom + UiOffset.X
                    + (correctScreenSize.X / 2))
                    * ScreenSizeAdjustment
                    ),

                    (int)(((locationShape.Y- Centering.Y) * Zoom + UiOffset.Y
                    + (correctScreenSize.Y / 2) )
                    * ScreenSizeAdjustment
                    ),

                    (int)((locationShape.Width) * Zoom
                    * ScreenSizeAdjustment
                    ),
                    (int)((locationShape.Height) * Zoom
                    * ScreenSizeAdjustment
                    ));
        }
        // overload
        public void draw(Rectangle locationShape, Texture2D texture, Color color)
        {
            draw(locationShape, texture, new Rectangle(0, 0, texture.Width, texture.Height), color);
        }
        public void draw(Rectangle locationShape, Texture2D texture)
        {
            draw(locationShape, texture, Color);
        }
    }
}
