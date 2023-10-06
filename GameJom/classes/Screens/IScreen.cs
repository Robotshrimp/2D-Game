using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJom
{
    interface IScreen
    {
        void initialize();// intialization, ie, getting all assets
        void update();// update logic| use this to update screens?
        void draw();// draws all nescessary screen objects
        IScreen changeScreen(); // allows the class using IScreen to return a new screen to switch to
    }
}
