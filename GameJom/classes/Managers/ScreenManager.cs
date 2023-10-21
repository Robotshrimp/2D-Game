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
        Dictionary<string, IScreen> UsedScreens;
        List<string> ActiveScreens;
        public void Initalize()
        {
            UsedScreens.Add(homeScreen.name, homeScreen);
        }
        public void Update()
        {

            foreach (string activeScreen in ActiveScreens)
            {
                UsedScreens[activeScreen].Update();
                string newScreen = UsedScreens[activeScreen].ActivateScreen();
                if (newScreen != null && !ActiveScreens.Contains(newScreen)) // gets the new screen set by usedscreen if not null
                {
                    ActiveScreens.Add(newScreen);
                }
                if (UsedScreens[activeScreen].removeSelf()) // removed this usedScreen from usedScreens
                {
                    ActiveScreens.Remove(activeScreen);
                }

            }
        }
        public void Draw()
        {

            foreach (string activeScreen in ActiveScreens)
            {
                UsedScreens[activeScreen].Draw();
            }
        }
    }
}
