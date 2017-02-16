using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipPrototype.ScreenAssets
{
    class SwitchButton : Button
    {
        private Texture2D _switchTexture;
        private Texture2D _currentTexture;

        public SwitchButton(int x, int y) : base(x, y)
        {
        }
        
        public void LoadContent(ContentManager content, string textureName, string switchTextureName)
        {
            base._texture = content.Load<Texture2D>(textureName);
            _switchTexture = content.Load<Texture2D>(switchTextureName);
            _currentTexture = base._texture;
        }

        protected override void OnLeftClick()
        {
            base.OnLeftClick();
            Switch();
        }

        private void Switch()
        {
            if (_currentTexture == base._texture)
                _currentTexture = _switchTexture;
            else if (_currentTexture == _switchTexture)
                _currentTexture = base._texture;
        }

        public override void Draw(SpriteBatch batcher)
        {
            _drawRectangle.Width = _texture.Width;
            _drawRectangle.Height = _texture.Height;
            batcher.Draw(texture: _currentTexture, destinationRectangle: base._drawRectangle);
        }
    }
}
