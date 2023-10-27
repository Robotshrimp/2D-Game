using GameJom.classes.Basic_Graphics.Custom_Effects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace GameJom
{
    public class Camera : Global
    {
        // 
        // this class is in charge of modifying draw rectangles to conform to settings defined in the constructor and the screen size offset
        // any drawing should be done through here to ensure correct location. this function supports custom effects so any class built with 
        // ICustomEffect can be added to the CustomEffects List to be called when an object is drawn using this class
        //
        static double ScreenSizeAdjustment = Game1.ScreenSizeAdjustment; // change in screen size to fit the current screen size of the hardware
        static Point CalculationScreenSize = Game1.calculationScreenSize; // unused point value of the screen size value the game interprets as full screen
        

        public Rectangle DisplayLocation;//bounding box of where the sprites will be rendered, anythng outside this rectangle is not drawn
        public Point Centering; // the point where the camera adjestments focus on
        Color Color = Color.White; // default color the program defauts to if no color is provided
        public double Zoom; // how much zooming is done, use the parallax class to fine tune this value in line with depth of layers
        public bool Drawn; // determines if any draw calls are allowed to be made using this instance
        public List<ICustomEffect> CustomEffects = new List<ICustomEffect> (); // default effect list



        #region Constructors
        public Camera(Rectangle displayLocation, Point centering, Color color, bool drawn = true, double zoom = 1)
        {
            this.Centering = centering;
            this.DisplayLocation = displayLocation;
            this.Color = color;
            this.Drawn = drawn;
            this.Zoom = zoom;
            CustomEffects.Add(new BaseDraw());
        }
        public Camera(Rectangle displayLocation, Color color, bool drawn = true, double zoom = 1) // autofills values for the previous constructor
            : this(displayLocation, 
                  
                  new Point((int)(displayLocation.Width / 2 / ScreenSizeAdjustment), (int)(displayLocation.Height / 2 / ScreenSizeAdjustment)), 
                  
                  color, drawn, zoom)
        {

        }
        public Camera(bool drawn = true, double zoom = 1) // autofills values for the previous constructor
            : this(Game1.ScreenBounds, Color.White, drawn, zoom)
        {
            
        }
        #endregion
        #region Draw
        public void Draw(Rectangle destination, Texture2D texture, Rectangle usedTexture, Color color, float angle = 0, List<ICustomEffect> additionalEffects = null, string reserveStorage = null)
        {
            if (Drawn)
            {
                if (additionalEffects != null)
                {
                    CustomEffects.AddRange(additionalEffects);
                }
                // the size, shape, and location of the object on the screen

                Rectangle Processed = DisplayRectangle(destination);

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
                        float shrinkTop = (float)(Processed.Bottom - overlapArea.Y) / (float)Processed.Height;
                        float shrinkLeft = (float)(Processed.Right - overlapArea.X) / (float)Processed.Width;
                        float shrinkHeight = (float)overlapArea.Height / (float)Processed.Height;
                        float shrinkWidth = (float)overlapArea.Width / (float)Processed.Width;

                        //shrinks texture in porportion to draw space shrink
                        usedTexture.Y = (int)(usedTexture.Bottom - (usedTexture.Height * shrinkTop));
                        usedTexture.X = (int)(usedTexture.Right - (usedTexture.Width * shrinkLeft));
                        usedTexture.Height = (int)(shrinkHeight * usedTexture.Height);
                        usedTexture.Width = (int)(shrinkWidth * usedTexture.Width);

                        Processed = overlapArea;
                    }

                    //draws the shape
                    foreach (ICustomEffect effect in CustomEffects)
                    {
                        effect.Draw(Processed, texture, usedTexture, color, angle, reserveStorage);
                    }
                }
            }
        }
        public void Draw(Rectangle destination, Texture2D texture, Color color, float angle = 0, List<ICustomEffect> additionalEffects = null, string reserveStorage = null)
        {
            Draw(destination, texture, new Rectangle(0, 0, texture.Width, texture.Height), color, angle, additionalEffects, reserveStorage);
        }
        public void Draw(Rectangle destination, Texture2D texture, float angle = 0, List<ICustomEffect> additionalEffects = null, string reserveStorage = null)
        {
            Draw(destination, texture, Color, angle, additionalEffects, reserveStorage);
        }

        public void DrawLine(LineClass line, string name = null)
        {
            if (Drawn)
            {
                // TODO add integration for automated draw
                line = new LineClass(/*DisplayPoint*/(line.Start), /*DisplayPoint*/(line.End), line.thiccness);// 3D renderer renders based off screen size already so no need for this extra scaling
                this.Draw(texture: Game1.BlankTexture, destination: new Rectangle(line.Start, new Point((int)line.length, line.thiccness)), angle: line.angle, color: Color.White, reserveStorage: name);
            }
        }
        public void ScaleableTexture(Rectangle drawTo, Texture2D texture)
        {
            int cornerWidth = Math.Min((int)(drawTo.Width / 2f), (int)(texture.Width / 3f));
            int cornerHeight = Math.Min((int)(drawTo.Height / 2f), (int)(texture.Height / 3f));

        }
        Dictionary<string, List<Rectangle>> StaggeredDrawStorages = new Dictionary<string, List<Rectangle>>();
        #endregion
        public Rectangle RatioRectangle(Vector2 Location, Vector2 Size)// draws using the percentage of the screen width
        {
            int x = (int)(graphics.PreferredBackBufferWidth * Location.X);
            int y = (int)(graphics.PreferredBackBufferHeight * Location.Y);
            return new Rectangle(x,y,(int)(graphics.PreferredBackBufferWidth * Size.X), (int)(graphics.PreferredBackBufferHeight * Size.Y));
        }
        #region calc-display switching
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
        #endregion
    }
}