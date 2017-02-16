using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace ShipPrototype.ScreenAssets
{
    class Button
    {
        protected Rectangle _drawRectangle;
        protected Texture2D _texture;

        private MouseState _currentMouseState;
        private MouseState _previousMouseState;
        private bool _pressed;

        public Button(int x, int y)
        {
            _drawRectangle = new Rectangle(x, y, 0, 0);
            _pressed = false;
        }

        public void LoadContent(ContentManager content, string textureName)
        {
            _texture = content.Load<Texture2D>(textureName);
        }

        public void Update()
        {
            _currentMouseState = Mouse.GetState();
            if (LeftClick())
                OnLeftClick();
            else
                _pressed = false;
            _previousMouseState = Mouse.GetState();
        }

        private bool LeftClick()
        {
            return _currentMouseState.LeftButton == ButtonState.Pressed &&
                   _previousMouseState.LeftButton != ButtonState.Pressed &&
                   _drawRectangle.Contains(_currentMouseState.Position);
        }

        protected virtual void OnLeftClick()
        {
            _pressed = true;
        }

        public virtual void Draw(SpriteBatch batcher)
        {
            _drawRectangle.Width = _texture.Width;
            _drawRectangle.Height = _texture.Height;
            batcher.Draw(texture: _texture, destinationRectangle: _drawRectangle);
        }

        public bool IsPressed()
        {
            return _pressed;
        }
    }
}
