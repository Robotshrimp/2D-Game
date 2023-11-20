using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJom.classes.Input
{
    public class InputMap
    {
        enum inputs
        {
            //movement
            up = 1,
            left = 2,
            right = 4,
            down = 8,
            //advanced
            jump = 16,
            shoot = 32,
            dash = 64,

            //menu shortcuts
            exit = 128,


            // debug
            debug = 256,

        }
        public int Inputs { get; set; }
        Dictionary<Keys, inputs> KeyMapKeyboard = new Dictionary<Keys, inputs>()
        { 
            {Keys.W, inputs.up},
            {Keys.A, inputs.left},
            {Keys.D, inputs.right},
            {Keys.S, inputs.down},
            {Keys.Space, inputs.jump},
            {Keys.X, inputs.shoot},
            {Keys.Z, inputs.dash},
            {Keys.Escape, inputs.exit},
            {Keys.F1, inputs.debug},
        };
        public InputMap() 
        {
        }
        public void Update()
        {
            Keys[] KeyInputs = Keyboard.GetState().GetPressedKeys();
            Inputs = 0;
            foreach (Keys key in KeyInputs) 
            {
                Inputs += (int)KeyMapKeyboard[key];
            }
        }

    }
}
