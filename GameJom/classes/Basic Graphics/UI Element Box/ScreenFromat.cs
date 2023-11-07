using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJom
{
    public class ScreenFromat
    {
        public HashSet<string> addScreen = new HashSet<string>(); // denotes the screen to switch to if not null
        public HashSet<string> ActivateScreens()
        {
            HashSet<string> placeHolderScreen = addScreen;
            addScreen = new HashSet<string>();
            return placeHolderScreen;
        }
        public HashSet<string> removeScreen = new HashSet<string>(); // removes screens in this list from activeScreens
        public HashSet<string> RemoveScreens()
        {
            return removeScreen;
        }
    }
}
