using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ShipPrototype.ScreenAssets;
using ShipPrototype.Screens;

namespace ShipPrototype
{
    class BuildingScreen : Screen
    {
        private Grid _grid;
        private Palette _palette;
        private BuildingMode _currentMode;

        private Button _backButton;
        private Button _playButton;
        private SwitchButton _modeButton;

        public BuildingScreen(ContentManager content) : base(content)
        {
            _grid = new Grid(new Point(9, 9), new Point(150, 50), 350);
            _palette = new Palette(750, 50, 0.5f);
            _currentMode = BuildingMode.BUILDING;
            _backButton = new Button(175, 450);
            _playButton = new Button(775, 450);
            _modeButton = new SwitchButton(800, 325);
        }

        public override void LoadContent()
        {
            _grid.LoadContent(_content);
            _palette.LoadContent(_content);
            _backButton.LoadContent(_content, "Button_Back");
            _playButton.LoadContent(_content, "Button_TestShip");
            _modeButton.LoadContent(_content, "SwitchButton_Build", "SwitchButton_Keys");
        }

        public override void Update(GameTime time)
        {
            _palette.Update(_currentMode, _content);
            _grid.Update(_currentMode, _palette.GetSelection(), _content);
            UpdateButtons();
        }

        private void UpdateButtons()
        {
            _backButton.Update();
            _playButton.Update();
            _modeButton.Update();
            if (_backButton.IsPressed())
                base.RequestScreen(new TitleScreen(base._content));
            if (_playButton.IsPressed() && _grid.HasOneCore())
                base.RequestScreen(new GameScreen(base._content, _grid));
            if (_modeButton.IsPressed())
                SwitchMode();
        }

        private void SwitchMode()
        {
            if (_currentMode == BuildingMode.BUILDING)
            {
                _currentMode = BuildingMode.KEYBINDING;
                _palette.ClearSelection();
            }
            else if (_currentMode == BuildingMode.KEYBINDING)
            {
                _currentMode = BuildingMode.BUILDING;
            }
        }

        public override void Draw(SpriteBatch batcher)
        {
            _backButton.Draw(batcher);
            _playButton.Draw(batcher);
            _modeButton.Draw(batcher);
            _grid.Draw(batcher);
            _palette.Draw(batcher);
        }
    }
}
