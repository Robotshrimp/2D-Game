using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameJom
{
    public class ScreenManager
    {
        Dictionary<string, IScreen> UsedScreens = new Dictionary<string, IScreen>();
        HashSet<string> ActiveScreens = new HashSet<string>();
        public void Initialize(string StartScreen)
        {

            HomeScreen homeScreen = new HomeScreen();
            UsedScreens.Add(HomeScreen.name, homeScreen);
            EditorSelectorScreen editorSelectorScreen = new EditorSelectorScreen();
            UsedScreens.Add(EditorSelectorScreen.name, editorSelectorScreen);
            foreach (string screen in UsedScreens.Keys)
            {
                UsedScreens[screen].Initialize();
            }
            ActiveScreens.Add(StartScreen);
        }
        public void Update()
        {
            HashSet<string> ActiveScreensThisTick = new HashSet<string>(ActiveScreens);
            foreach (string activeScreen in ActiveScreensThisTick)
            {
                UsedScreens[activeScreen].Update();
                HashSet<string> newScreens = UsedScreens[activeScreen].ActivateScreens();
                foreach (string newScreen in newScreens)
                {
                    ActiveScreens.Add(newScreen);
                }
                HashSet<string> removeScreens = UsedScreens[activeScreen].RemoveScreens();
                foreach (string removeScreen in removeScreens)
                {
                    ActiveScreens.Remove(removeScreen);
                }
                
                if (UsedScreens[activeScreen].RemoveScreens().Count > 0) // removed this usedScreen from usedScreens
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
