using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJom.classes.Basic_Graphics.UI.Buttons
{
    public interface IButtonEffect
    {
        //used to set a collection of animations used by the button when conditions are met
        void Pressed();
        void Unpressed();
        void Hover();
        void Unselected();
    }
}
