using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJom.classes.Basic_Graphics.Custom_Effects
{
    internal class ScreenShake : ICustomEffect
    {
        int YVariation;
        int XVariation;
        int YRValue;
        int XRValue;
        
        public List<ICustomEffect> CustomEffects { get ; set ; }
        public ScreenShake(int yVariation, int xVariation)
        {
            YVariation = yVariation;
            XVariation = xVariation;
        }


        public void Draw(Rectangle destination, Texture2D texture, Rectangle usedTexture, Color color, float angle = 0)
        {
            foreach (ICustomEffect customEffect in CustomEffects)
            {
                customEffect.Draw( new Rectangle(new Point(destination.X + XRValue, destination.Y + YRValue), destination.Size),  texture,  usedTexture,  color,  angle );
            }
        }

        public void GroupDraw()
        {
            foreach (ICustomEffect customEffect in CustomEffects)
            {
                customEffect.GroupDraw();
            }
        }

        public void Update()
        {
            Random random = new Random();
            YRValue = random.Next(YVariation);
            XRValue = random.Next(XVariation);
            foreach (ICustomEffect customEffect in CustomEffects)
            {
                customEffect.Update();
            }
        }
    }
}
