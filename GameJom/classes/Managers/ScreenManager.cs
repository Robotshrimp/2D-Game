using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameJom.classes.Screens
{
    internal class ScreenManager
    {
        HomeScreen homeScreen;
        IScreen usedScreen;
        public void Initalize()
        {
            usedScreen = homeScreen;
        }
        public void Update()
        {
            usedScreen.update();
        }
        public void Draw()
        {
            usedScreen.draw();
        }
    }
}
