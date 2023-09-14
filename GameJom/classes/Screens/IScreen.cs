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

        void initialize(string folder);// intialization, ie, getting all assets
        void update(float delta);// update logic
        void draw(AutomatedDraw automatedDraw);// draws all nescessary screen objects
    }
}
