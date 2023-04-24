using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace GameJom.classes
{
    static class InputManager
    {
        static Dictionary<Keys, bool> pressedLastFrame = new Dictionary<Keys, bool>();
        static Dictionary<Keys, Keys> keyBind = new Dictionary<Keys, Keys>();
        static public bool SingleTapCheck(Keys key)
        {
            if (!pressedLastFrame.ContainsKey(key))
                pressedLastFrame.Add(key, true);
            if (!keyBind.ContainsKey(key))
                keyBind.Add(key, key);
            if (Keyboard.GetState().IsKeyDown(key) & !pressedLastFrame[key])
                return true; else return false;
        }
        static public void Update()
        {
            foreach(Keys key in pressedLastFrame.Keys.ToList()) 
            {
                if (Keyboard.GetState().IsKeyDown(key))
                    pressedLastFrame[key] = true;
                else
                    pressedLastFrame[key] = false;
            }
        }
    }
}
