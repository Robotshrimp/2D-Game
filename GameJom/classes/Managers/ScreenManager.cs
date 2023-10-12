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
        List<IScreen> usedScreens;
        public void Initalize()
        {
            foreach (IScreen usedScreen in usedScreens)
            {
                usedScreen.Initialize();
            }
        }
        public void Update()
        {

            foreach (IScreen usedScreen in usedScreens)
            {
                usedScreen.Update();
                IScreen newScreen = usedScreen.newScreen();
                if (newScreen != null && !usedScreens.Contains(newScreen)) // gets the new screen set by usedscreen if not null
                {
                    usedScreen.newScreen().Initialize();
                    usedScreens.Add(usedScreen.newScreen());
                }
                if(usedScreen.removeSelf()) // removed this usedScreen from usedScreens
                {
                    usedScreens.Remove(usedScreen);
                }

            }
        }
        public void Draw()
        {

            foreach (IScreen usedScreen in usedScreens)
            {
                usedScreen.Draw();
            }
        }
    }
}
