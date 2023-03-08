﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameJom
{
    public class AutomatedDraw
    {

        static SpriteBatch spriteBatch = Game1.spriteBatch;
        static GraphicsDeviceManager graphics = Game1.graphics;
        static double ScreenSizeAdjustment = Game1.ScreenSizeAdjustment;
        static Point CalculationScreenSize = Game1.calculationScreenSize;
        //constructor variables

        public Rectangle DisplayLocation;//bounding box of where the sprites will be rendered, anythng outside this rectangle is not drawn
        public Point Centering; // 
        Color Color;
        public double Zoom;
        public bool Drawn;
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
                  
                  //this code gets 3840 / 2 and 2160 / 2
                  new Point((int)(displayLocation.Width / 2 / ScreenSizeAdjustment), (int)(displayLocation.Height / 2 / ScreenSizeAdjustment)), 
                  
                  color, drawn, zoom)
        {

        }
        public AutomatedDraw()
            : this(Game1.ScreenBounds, Color.White)
        {
            
        }

        // the draw code that coombines individrual draw parameters and constructor parameters and draws the result

        public void mDraw(Rectangle locationShape, Texture2D texture, Rectangle usedTexture, Color color)
        {
            if (Drawn)
            {

                // the size, shape, and location of the object on the screen

                Rectangle Processed = DisplayRectangle(locationShape);

                // code that stops drawing objects that are offscreen

                if (!(Processed.Right < DisplayLocation.Left || Processed.Left > DisplayLocation.Right
                    || Processed.Bottom < DisplayLocation.Top || Processed.Top > DisplayLocation.Bottom))
                {
                    spriteBatch.Draw(texture, Processed, usedTexture, color);
                }
            }
        }
        // overload
        public void mDraw(Rectangle locationShape, Texture2D texture, Color color)
        {
            mDraw(locationShape, texture, new Rectangle(0, 0, texture.Width, texture.Height), color);
        }
        public void mDraw(Rectangle locationShape, Texture2D texture)
        {
            mDraw(locationShape, texture, Color);
        }


        public Rectangle RatioRectangle(Vector2 Location, Vector2 Size)// 
        {
            int x = (int)(graphics.PreferredBackBufferWidth * Location.X);
            int y = (int)(graphics.PreferredBackBufferHeight * Location.Y);
            return new Rectangle(x,y,(int)(graphics.PreferredBackBufferWidth * Size.X), (int)(graphics.PreferredBackBufferHeight * Size.Y));
        }


        public Point PointScale(Point point)
        {
            return new Point(
                (int)((point.X - Centering.X) * ScreenSizeAdjustment * Zoom + DisplayLocation.Width / 2) + DisplayLocation.X ,
                (int)((point.Y - Centering.Y) * ScreenSizeAdjustment * Zoom + DisplayLocation.Height / 2) + DisplayLocation.Y);
        }

        public Point PointUnScale(Point point)
        {
            return new Point(
                (int)((point.X - DisplayLocation.X - DisplayLocation.Width / 2) / (ScreenSizeAdjustment * Zoom)) + Centering.X,
                (int)((point.Y - DisplayLocation.Y - DisplayLocation.Height / 2) / (ScreenSizeAdjustment * Zoom)) + Centering.Y);
        }

        public int LenScale(int len)
        {
            return (int)(len * ScreenSizeAdjustment * Zoom);
        }
        public int LenUnScale(int len)
        {
            return (int)(len / (ScreenSizeAdjustment * Zoom));
        }








        public Rectangle DisplayRectangle(Rectangle locationShape) //changes the rectangle as it is represented in the computational side into the rechtangle as it should be on the screen
        {

            return new Rectangle(
                    PointScale(locationShape.Location),
                    new Point(LenScale(locationShape.Width), LenScale(locationShape.Height))
                    );
        }

        //for calculating where Calculation Rechtangle would be for the given

        public Rectangle DisplayToCalculation(Rectangle displayShape)
        {
            return new Rectangle(
                (int)((displayShape.X - DisplayLocation.X - DisplayLocation.Width / 2) / (ScreenSizeAdjustment * Zoom)) + Centering.X,
                (int)((displayShape.Y - DisplayLocation.Y - DisplayLocation.Height / 2) / (ScreenSizeAdjustment * Zoom)) + Centering.Y,
                (int)(displayShape.Width / (ScreenSizeAdjustment * Zoom)),
                (int)(displayShape.Height / (ScreenSizeAdjustment * Zoom))
                );

        }
    }
}