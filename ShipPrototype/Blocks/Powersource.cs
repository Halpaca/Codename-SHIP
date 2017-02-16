using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace ShipPrototype
{
    class Powersource : Block
    {
        protected KeyListener _keyListener;

        public Powersource()
        {
            _keyListener = new KeyListener();
        }

        public override void LoadContent(ContentManager content)
        {
            _texture = content.Load<Texture2D>("Block_Powersource");
        }

        public void SetKey(Keys k)
        {
            _keyListener.SetKey(k);
        }

        public void ClearKey()
        {
            _keyListener.Clear();
        }

        public bool KeyBinded()
        {
            return _keyListener.HasKey();
        }

        public override void Update()
        {
            if (_keyListener.KeyDown() && _activated)
            {
                _receivedPower = true;
                PowerAdjacentBlocks();
            }
            base.Update();
        }

        private void PowerAdjacentBlocks()
        {
            PowerAdjacentBlock(Direction.UP);
            PowerAdjacentBlock(Direction.DOWN);
            PowerAdjacentBlock(Direction.LEFT);
            PowerAdjacentBlock(Direction.RIGHT);
        }

        private void PowerAdjacentBlock(Direction d)
        {
            if (_adjacentBlocks.ContainsKey(d))
                _adjacentBlocks[d].ReceivePower(d);
        }

        public override Block CreateCopy(ContentManager content)
        {
            Powersource p = new Powersource();
            p.LoadContent(content);
            return p;
        }

        public override void RotateCW(ContentManager content)
        {
            //Powersource only has one orientation;
        }
    }
}
