using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameJom.classes.Basic_Graphics.Custom_Effects;

namespace GameJom
{
    public class AfterImage : Global, ICustomEffect
    {
        public int AfterImageLength { get; set; } // number of after images
        public int AfterImageDuration { get; set; } // number of frames between each after image
        public Single TrailShrinkOff { get; set; } // multiplied by the previous image size to produce next image size
        public float TrailFadeOff { get; set; } // adds this to the alpha channel of the previous image to get the next image's alpha channel value
        public List<ColorFrameData> ColorKeys { get; set; } // the key denotes the frame, the color denotes what color the sprite should be by that frame
        public string EffectName { get; set; } // the name the program uses to get this effect

        public string Name { get; } = "AfterImage";

        // ICustomEffect does not need to be loaded for an effect format, it only needs to exist when effects is compiled within an effect tree

        public List<ICustomEffect> CustomEffects = new List<ICustomEffect>(); // list of effects to pass the draw param to for further processing, multiple ICustomEffect objects may result in multiple draw operations on the same draw instance
        List<AfterImageData> AfterImages = new List<AfterImageData>(); // stored trail data of every call instance
        int tick = 0; // helps keep track of when the next afterimage should be added in conjunction with AfterImageDuration


        public AfterImage(int afterImageLength = 20, int afterImageDuration = 1, Single trailShrinkOff = 0.9f, Single trailFadeOff = 0.9f, List<ColorFrameData> colorKeys = null) 
        {
            AfterImageLength = afterImageLength; AfterImageDuration = afterImageDuration; TrailShrinkOff = trailShrinkOff; TrailFadeOff = trailFadeOff;  ColorKeys = colorKeys;
            
            CustomEffects.Add(new BaseDraw());
        }
        public void Draw(Rectangle destination, Texture2D texture, Rectangle usedTexture, Color color, float angle = 0)
        {
            // to alter the output frame by frame, nest this custom effect into the desired effect(ie: if you wanted each frame to be offset by x pixels randomly, pass the input for this method through the screenshake effect)
            AfterImages.Insert(0, new AfterImageData(destination, texture, angle, usedTexture, 1, color));
        }
        public void GroupDraw() // handles all the actrual drawing
        {
            foreach (AfterImageData afterImage in AfterImages)
            {
                foreach (ICustomEffect customEffect in CustomEffects) // effect draw should always be where the draw is outputed in the effect, if an effect outputs in draw this foreach loop would be location in the draw method
                {
                    customEffect.Draw(afterImage.Rectangle, afterImage.Texture, afterImage.UsedTexture, afterImage.CurrentColor * afterImage.Alpha, afterImage.Rotation);
                }
            }
            foreach(ICustomEffect customEffect in CustomEffects)
            {
                customEffect.GroupDraw();
            }
        }
        public void Update() // use for once per frame calculations, should only be used once per frame durring implimentation
        {
            List<AfterImageData> removeList = new List<AfterImageData>();
            foreach (AfterImageData afterImage in AfterImages)
            {
                afterImage.Alpha = afterImage.Alpha * TrailFadeOff;
                afterImage.Frame++;
                afterImage.CurrentColor = GenerateColor(afterImage.StartColor, afterImage.Frame);
                if (afterImage.Frame == AfterImageLength)
                {
                    removeList.Add(afterImage);
                }
            }
            foreach (AfterImageData afterImage in removeList)
            {
                AfterImages.Remove(afterImage);
            }
                tick++;
            foreach (ICustomEffect customeEffect in CustomEffects)
            {
                customeEffect.Update();
            }
        }
        Color GenerateColor(Color color, int frame)
        {
            if (ColorKeys.Count == 0) return color;
            Color Output = color;
            int currentKeyColor = 0;
            Color PreviousColor = color;
            Color NextColor = ColorKeys[currentKeyColor].FrameColor;
            int progressToNextColor = 0;
            int nextColorDistance = ColorKeys[currentKeyColor].Frames;
            for(int i = 0;i <= frame;i++)
            {
                if (progressToNextColor == nextColorDistance)
                {
                    progressToNextColor = 0;
                    currentKeyColor++;
                    int withinListBounds = Math.Min(currentKeyColor, ColorKeys.Count - 1);
                    nextColorDistance = ColorKeys[withinListBounds].Frames;
                    PreviousColor = NextColor;
                    NextColor = ColorKeys[withinListBounds].FrameColor;
                }
                if (i == frame)
                {
                    Output = new Color((PreviousColor.R + ((float)(NextColor.R - PreviousColor.R) * ((float)progressToNextColor / (float)nextColorDistance))) / 255,
                        (float)(PreviousColor.G + ((float)(NextColor.G - PreviousColor.G) * ((float)progressToNextColor / (float)nextColorDistance))) / 255,
                        (PreviousColor.B + ((float)(NextColor.B - PreviousColor.B) * ((float)progressToNextColor / (float)nextColorDistance))) / 255);
                }
                progressToNextColor++;
            }
            return Output;
        }
    }
    internal class AfterImageData // storage class for all variables needed to run the draw method 
    {
        public Rectangle Rectangle { get; set; }
        public Texture2D Texture { get; set; }
        public float Rotation { get; set; }
        public Rectangle UsedTexture { get; set; }
        public Single Alpha { get; set; }
        public Color StartColor { get; set; }
        public Color CurrentColor { get; set; }
        public int Frame { get; set; }
        public AfterImageData(Rectangle rectangle, Texture2D texture, float rotation, Rectangle usedTexture, Single alpha, Color startColor, int frame = 0) // use for quick assign
        { 
            Rectangle = rectangle; Texture = texture; Rotation = rotation; UsedTexture = usedTexture; Alpha = alpha; StartColor = startColor; Frame = frame;
        }
    }
    public class ColorFrameData // class used to store a color value and a int value used to mark the frame
    {
        public Color FrameColor { get; set; }
        public int Frames { get; set; } // marks for the number of frames it should take to get to this, not which frame this color will be fully realized
    }
}
