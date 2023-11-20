using GameJom.classes.Basic_Graphics.UI.Buttons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJom
{
    class ScreenSwitch : IButton
    {
        public Rectangle Bounds { get ; set ; }
        string SwitchToScreen;
        string RemoveScreen;
        IScreen ButtonLocation;

        // place holder for animations
        Texture2D ButtonTexture;
        ICustomEffect pressEffect;
        ICustomEffect unpressEffect;
        ICustomEffect selectEffect;
        ICustomEffect unselectEffect;
        public void Pressed()
        {
            ButtonLocation.addScreen.Add(SwitchToScreen);
            ButtonLocation.removeScreen.Add(RemoveScreen);
        }

        public void Selected()
        {
            
        }

        public void Unpressed()
        {
            
        }

        public void Unselected()
        {
            
        }
    }
}
