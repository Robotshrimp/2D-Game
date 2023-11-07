using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GameJom
{
    public  class UIBoxElement
    {
        public Rectangle ElementBox;
        public Camera Camera;
        MouseState mouseState = new MouseState();
        public void resize()
        {
            mouseState = Mouse.GetState();
            if(mouseState.LeftButton == ButtonState.Pressed)
            {

            }
        }
        public void move()
        {
            mouseState = Mouse.GetState();

        }
    }
}
