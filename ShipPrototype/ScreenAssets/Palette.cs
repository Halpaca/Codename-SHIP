using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ShipPrototype.Blocks;
using ShipPrototype.Screens;
using System.Collections.Generic;
using System.Linq;

namespace ShipPrototype.ScreenAssets
{
    class Palette
    {
        private Tile[] _palette;
        private Tile _selectedTile;
        private Point _position;
        private float _scale;

        //Include Mousestates to know when the palette needs to know when to rotate the block at the mouse
        private MouseState _currentMouseState;
        private MouseState _previousMouseState;

        public Palette(int x, int y, float scale)
        {
            _position = new Point(x, y);
            _scale = scale;
            _palette = InitTiles();
            _selectedTile = new Tile(0, 0);
            _currentMouseState = Mouse.GetState();
            _previousMouseState = Mouse.GetState();
        }

        private Tile[] InitTiles()
        {
            LinkedList<Tile> tileData = new LinkedList<Tile>();
            tileData.AddLast(new Tile(0, 1, new Core()));
            tileData.AddLast(new Tile(2, 1, new Powersource()));
            tileData.AddLast(new Tile(4, 1, new Gun(Direction.UP)));
            tileData.AddLast(new Tile(6, 1, new Thruster(Direction.DOWN)));
            tileData.AddLast(new Tile(0, 3, new Wire(WireType.HORIZONTAL)));
            tileData.AddLast(new Tile(2, 3, new Wire(WireType.CROSS)));
            tileData.AddLast(new Tile(4, 3, new Wire(WireType.CURVED)));
            tileData.AddLast(new Tile(6, 3, new BasicBlock()));
            return tileData.ToArray();
        }

        public void LoadContent(ContentManager content)
        {
            foreach (Tile t in _palette)
                t.LoadContent(content);
        }

        public void Update(BuildingMode mode, ContentManager content)
        {
            if (mode == BuildingMode.BUILDING)
                foreach (Tile t in _palette)
                    if (t.LeftClick())
                        CopyDataToSelection(t, content);
            UpdateMouse(content);
        }

        private void CopyDataToSelection(Tile t, ContentManager content)
        {
            if (t.HasBlockData())
                _selectedTile.SetBlockData(t.GetBlockData().CreateCopy(content));
            else
                _selectedTile.ClearBlockData();
        }

        private void UpdateMouse(ContentManager content)
        {
            _currentMouseState = Mouse.GetState();
            if (_selectedTile.HasBlockData() && RightClick())
            {
                _selectedTile.GetBlockData().RotateCW(content);
            }
            _previousMouseState = Mouse.GetState();
        }

        private bool RightClick()
        {
            return _currentMouseState.RightButton == ButtonState.Pressed &&
                   _previousMouseState.RightButton != ButtonState.Pressed;
        }

        public void Draw(SpriteBatch batcher)
        {
            foreach (Tile t in _palette)
                t.Draw(batcher, _position.X, _position.Y, _scale);
            if(_selectedTile.HasBlockData())
                _selectedTile.Draw(batcher, Mouse.GetState().X + 12, Mouse.GetState().Y + 10, 0.25f);
        }

        public Tile GetSelection()
        {
            return _selectedTile;
        }

        public void ClearSelection()
        {
            _selectedTile.ClearBlockData();
        }
    }
}
