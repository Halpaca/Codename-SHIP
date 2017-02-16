using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ShipPrototype.ScreenAssets;

namespace ShipPrototype.Screens
{
    class TitleScreen : Screen
    {
        private Button _buildingButton;

        public TitleScreen(ContentManager content) : base(content)
        {
            _buildingButton = new Button(480, 360);
        }

        public override void LoadContent()
        {
            _buildingButton.LoadContent(_content, "Button_BuildShip");
        }

        public override void Update(GameTime time)
        {
            _buildingButton.Update();
            if(_buildingButton.IsPressed())
            {
                base.RequestScreen(new BuildingScreen(base._content));
            }
        }

        public override void Draw(SpriteBatch batcher)
        {
            _buildingButton.Draw(batcher);
        }
    }
}
