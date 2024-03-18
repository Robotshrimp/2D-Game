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
    class ScreenSwitch
    {
        public Rectangle Bounds { get ; set ; }
        string SwitchToScreen;
        string RemoveScreen;
        IScreen ButtonLocation;

        // place holder for animations
        Texture2D ButtonTexture;
        IButtonEffect pressEffect;
        IButtonEffect unpressEffect;
        IButtonEffect selectEffect;
        IButtonEffect unselectEffect;
        public void Pressed()
        {
            ButtonLocation.addScreen.Add(SwitchToScreen);
            ButtonLocation.removeScreen.Add(RemoveScreen);
        }

        public void Hover()
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
