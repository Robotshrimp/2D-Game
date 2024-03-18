using GameJom._3D_Because_Why_Not;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJom.classes.Input
{
    public enum inputs // outside class as non serializeable object
    {
        //movement
        up = 1,
        left = 2,
        right = 4,
        down = 8,
        //advanced
        jump = 16,
        LMBClick = 32,
        RMBClick = 64,

        //menu shortcuts
        exit = 128,


        // debug
        debug = 256,

    }
    public class InputMap // preferably non static for different playtime inputs(good if multiple genres are mixed)
    {
        public int InputAgrigate { get; private set; } // stores all inputs detected on keyboard with binary voodoo
        Dictionary<Keys, List<inputs>> KeyMapKeyboard = new Dictionary<Keys, List<inputs>>() // list of keyboard button input maps, limits to one input per key
        {
            // default settings
            {Keys.W, new List < inputs >() { inputs.up } },
            {Keys.A, new List < inputs >() { inputs.left }},
            {Keys.D, new List < inputs >() { inputs.right }},
            {Keys.S, new List < inputs >() { inputs.down }},
            {Keys.Space, new List < inputs >() { inputs.jump }},
            {Keys.Escape, new List < inputs >() { inputs.exit }},
            {Keys.OemTilde, new List < inputs >() { inputs.debug }},
        };
        List<inputs> RMBInputs = new List<inputs>() // list of input actions preformed by the right mouse button, can support multiple inputs per mouse click
        {
            // default settings
            {inputs.RMBClick },
        };
        List<inputs> LMBInputs = new List<inputs>() // list of input actions preformed by the left mouse button, can support multiple inputs per mouse click
        {
            // default settings
            {inputs.LMBClick },
        };
        public InputMap()// do not use constructor for instancing, load from json 
        {
        }
        public void Update()
        {
            Keys[] KeyInputs = Keyboard.GetState().GetPressedKeys();
            InputAgrigate = 0;
            foreach (Keys key in KeyInputs) 
            {
                if (KeyMapKeyboard.ContainsKey(key))
                {
                    foreach(int input in KeyMapKeyboard[key])
                    {
                        InputAgrigate |= input;
                    }
                }
            }
            if(Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                foreach (int input in LMBInputs) { InputAgrigate |= input;}
            }
            if (Mouse.GetState().RightButton == ButtonState.Pressed)
            {
                foreach (int input in RMBInputs) { InputAgrigate |= input; }
            }
        }
        public bool Inputed(bool combined = false, bool exclusive = false, params inputs[] checkedInput) // store presets of this as replacement for inputs enum?
        {
            List<int> validInputs = new List<int>();   
            if(combined)
            {
                int combInput = 0;
                foreach (int input in checkedInput)
                    combInput |= input;
                validInputs.Add(combInput);
            }
            else
            {
                foreach (int input in checkedInput)
                {
                    validInputs.Add(input);
                }
            }
            foreach (int input in validInputs)
            {
                if (exclusive)
                {
                    if (input == InputAgrigate)
                        return true;
                }
                else if ((input & InputAgrigate) == input)
                    return true;
            }
            return false;
        }
    }
    public static class InputForm
    {
        static Dictionary<Keys, bool> PreviousKeyState = new Dictionary<Keys, bool>(); // tracks the previous states of any used keys

        public static bool Click(Keys key)
        {
            if (!PreviousKeyState.ContainsKey(key)) // adds key to keytracker
            {
                PreviousKeyState.Add(key, false);
            }
            if (Keyboard.GetState().IsKeyDown(key))
            {
                PreviousKeyState[key] = true;
                if (!PreviousKeyState[key])
                {
                    return true;
                }

            }
            else
                PreviousKeyState[key] = false;
            return false;
        }
        public static void Hold()
        {

        }
        public static void Release()
        {

        }
        
    }
}
