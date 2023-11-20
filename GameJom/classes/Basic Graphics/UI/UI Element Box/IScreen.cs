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
        HashSet<string> addScreen { get; set; }
        HashSet<string> ActivateScreens(); // adds new screen for screen manager to process
        HashSet<string> removeScreen { get; set; }
        HashSet<string> RemoveScreens(); // removes any screens returned
    }
}
