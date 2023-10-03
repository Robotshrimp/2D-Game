using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameJom
{
    public class AutomatedDraw
    {

        SpriteBatch spriteBatch = Game1.spriteBatch;
        static GraphicsDeviceManager graphics = Game1.graphics;
        static double ScreenSizeAdjustment = Game1.ScreenSizeAdjustment;
        static Point CalculationScreenSize = Game1.calculationScreenSize;
        //constructor variables

        public Rectangle DisplayLocation;//bounding box of where the sprites will be rendered, anythng outside this rectangle is not drawn
        public Point Centering; // the point where the camera adjestments focus on
        Color Color = Color.White; // default color the program defauts to if no color is provided
        public double Zoom; // how much zooming is done, use the parallax class to fine tune this value in line with depth of layers
        public bool Drawn; // determines if any draw calls are allowed to be made using this instance
        // constructor, this takes the camera properties as paramters

        public AutomatedDraw(Rectangle displayLocation, Point centering, Color color, bool drawn = true, double zoom = 1)
        {
            this.Centering = centering;
            this.DisplayLocation = displayLocation;
            this.Color = color;
            this.Drawn = drawn;
            this.Zoom = zoom;
        }
        public AutomatedDraw(Rectangle displayLocation, Color color, bool drawn = true, double zoom = 1)
            : this(displayLocation, 
                  
                  //this code gets 3840 / 2 and 2160 / 2, the centering point of the normal screen coordnates
                  new Point((int)(displayLocation.Width / 2 / ScreenSizeAdjustment), (int)(displayLocation.Height / 2 / ScreenSizeAdjustment)), 
                  
                  color, drawn, zoom)
        {

        }
        public AutomatedDraw(bool drawn = true, double zoom = 1)
            : this(Game1.ScreenBounds, Color.White, drawn, zoom)
        {
            
        }

        // the draw code that coombines individrual draw parameters and constructor parameters and draws the result

        public void Draw(Rectangle locationShape, Texture2D texture, Rectangle usedTexture, Color color)
        {
            if (Drawn)
            {

                // the size, shape, and location of the object on the screen

                Rectangle Processed = DisplayRectangle(locationShape);

                // code that stops drawing objects that are offscreen

                if (!(Processed.Right < DisplayLocation.Left || Processed.Left > DisplayLocation.Right
                    || Processed.Bottom < DisplayLocation.Top || Processed.Top > DisplayLocation.Bottom))
                {
                    if (!(Processed.Right < DisplayLocation.Right && Processed.Left > DisplayLocation.Left
                    && Processed.Bottom < DisplayLocation.Bottom && Processed.Top > DisplayLocation.Top))
                    {
                        // code that stops drawing objects that are offscreen
                        Rectangle overlapArea = OverlapCheck.OverlappedArea(Processed, DisplayLocation); // gets overlapped area
                        // gets porpotions of the shrinking to use on texture shrink
                        float shrinkHeight = (float)overlapArea.Height / (float)Processed.Height;
                        float shrinkWidth = (float)overlapArea.Width / (float)Processed.Width;

                        //shrinks texture in porportion to draw space shrink
                        usedTexture.Y = (int)(usedTexture.Bottom - (usedTexture.Height * shrinkHeight));
                        usedTexture.X = (int)(usedTexture.Right - (usedTexture.Width * shrinkWidth));
                        usedTexture.Height = (int)(shrinkHeight * usedTexture.Height);
                        usedTexture.Width = (int)(shrinkWidth * usedTexture.Width);

                        Processed = overlapArea;
                    }

                    //draws the shape
                    spriteBatch.Draw(texture, Processed, usedTexture, color);
                }
            }
        }
        // overload
        public void Draw(Rectangle locationShape, Texture2D texture, Color color)
        {
            Draw(locationShape, texture, new Rectangle(0, 0, texture.Width, texture.Height), color);
        }
        public void Draw(Rectangle locationShape, Texture2D texture)
        {
            Draw(locationShape, texture, Color);
        }

        public void DrawLine(LineClass line)
        {
            if (Drawn)
            {
                // TODO add integration for automated draw
                line = new LineClass(DisplayPoint(line.Start), DisplayPoint(line.End), line.thiccness);
    #pragma warning disable CS0618 // unknown bug where program thinks a location Vector2 exists in the parameters given
                spriteBatch.Draw(texture: Game1.BlankTexture, destinationRectangle: new Rectangle(line.Start, new Point((int)line.length, line.thiccness)), rotation: line.angle, color: Color.White);
    #pragma warning restore CS0618
            }
        }

        public void SpriteBatchSpecification(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
        }

        public Rectangle RatioRectangle(Vector2 Location, Vector2 Size)// 
        {
            int x = (int)(graphics.PreferredBackBufferWidth * Location.X);
            int y = (int)(graphics.PreferredBackBufferHeight * Location.Y);
            return new Rectangle(x,y,(int)(graphics.PreferredBackBufferWidth * Size.X), (int)(graphics.PreferredBackBufferHeight * Size.Y));
        }


        public Point DisplayPoint(Point point)
        {
            return new Point(
                (int)((point.X - Centering.X) * ScreenSizeAdjustment / Zoom + DisplayLocation.Width / 2) + DisplayLocation.X ,
                (int)((point.Y - Centering.Y) * ScreenSizeAdjustment / Zoom + DisplayLocation.Height / 2) + DisplayLocation.Y);
        }

        public Point CalcPoint(Point point)
        {
            return new Point(
                (int)((point.X - DisplayLocation.X - DisplayLocation.Width / 2) / ScreenSizeAdjustment * Zoom) + Centering.X,
                (int)((point.Y - DisplayLocation.Y - DisplayLocation.Height / 2) / ScreenSizeAdjustment * Zoom) + Centering.Y);
        }

        public int LenScale(int len)
        {
            return (int)(len * ScreenSizeAdjustment / Zoom);
        }
        public int LenUnScale(int len)
        {
            return (int)(len / ScreenSizeAdjustment * Zoom);
        }








        public Rectangle DisplayRectangle(Rectangle locationShape) //changes the rectangle as it is represented in the computational side into the rechtangle as it should be on the screen
        {

            return new Rectangle(
                    DisplayPoint(locationShape.Location),
                    new Point(LenScale(locationShape.Width), LenScale(locationShape.Height))
                    );
        }

        //for calculating where Calculation Rechtangle would be for the given

        public Rectangle CalculationRectangle(Rectangle displayShape)
        {
            return new Rectangle(
                CalcPoint(displayShape.Location),
                new Point(LenUnScale(displayShape.Width), LenUnScale(displayShape.Height))
                );

        }
    }
}