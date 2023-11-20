using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJom.classes.Basic_Graphics.UI.Buttons
{
    interface IButton
    {
        Rectangle Bounds { get; set; }
        void Pressed();
        void Unpressed();
        void Selected();
        void Unselected();
    }
}
