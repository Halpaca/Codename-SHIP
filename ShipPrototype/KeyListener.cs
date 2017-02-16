using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace ShipPrototype
{
    class KeyListener
    {
        private Keys _key;

        public KeyListener()
        {
            _key = Keys.None;
        }

        public bool KeyDown()
        {
            KeyboardState state = Keyboard.GetState();
            return state.IsKeyDown(_key);
        }

        public void SetKey(Keys k)
        {
            _key = k;
        }

        public void Clear()
        {
            _key = Keys.None;
        }

        public bool HasKey()
        {
            return _key != Keys.None;
        }
    }
}
