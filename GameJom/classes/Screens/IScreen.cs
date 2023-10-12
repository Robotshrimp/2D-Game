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
        void Initialize(); // intialization, ie, getting all assets
        void Update(); // update logic| use this to update screens?
        void Draw(); // draws all nescessary screen objects
        IScreen newScreen(); // adds new screen for screen manager to process
        bool removeSelf(); // removed self from active screen manager screens
    }
}
