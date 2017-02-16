using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ShipPrototype.Screens;

namespace ShipPrototype
{
    abstract class Screen
    {
        protected ContentManager _content;

        private Screen _swapRequestScreen;
        private bool _hasSwapRequest;

        public Screen(ContentManager content)
        {
            _hasSwapRequest = false;
            _content = content;
        }

        public abstract void LoadContent();

        /// <summary>
        /// Tests if 
        /// </summary>
        public bool HasScreenSwapRequest()
        {
            return _hasSwapRequest;
        }

        public Screen GetScreenSwapRequest()
        {
            return _swapRequestScreen;
        }

        protected void RequestScreen(Screen s)
        {
            _swapRequestScreen = s;
            _hasSwapRequest = true;
        }

        public abstract void Update(GameTime time);
        public abstract void Draw(SpriteBatch batcher);
    }
}
