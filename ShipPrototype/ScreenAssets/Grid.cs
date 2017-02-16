using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Content;
using ShipPrototype.ScreenAssets;
using Microsoft.Xna.Framework.Input;
using ShipPrototype.Screens;

namespace ShipPrototype
{
    class Grid
    {
        private Tile[,] _gridData;
        private Point _position; //TODO: change to rectangle to calculate scale on its own
        private float _scale;
        private Keys _lastKeyPressed;

        public Grid(Point gridSize, Point position, int size)
        {
            _position = position;
            CalculateScale(size, gridSize);
            InitGrid(gridSize.X, gridSize.Y);
        }

        private void CalculateScale(int size, Point gridSize)
        {
            if (gridSize.X >= gridSize.Y)
                _scale = (float)(size / gridSize.X) / 100;
            else
                _scale = (float)(size / gridSize.Y) / 100;
        }

        /// <summary>
        /// Helper function for initializing the 2D Grid Array
        /// </summary>
        private void InitGrid(int xTiles, int yTiles)
        {
            _gridData = new Tile[xTiles, yTiles];
            for (int i = 0; i < xTiles; i++)
                for (int j = 0; j < yTiles; j++)
                    _gridData[i, j] = new Tile(i, j);
        }

        public void LoadContent(ContentManager content)
        {
            foreach (Tile g in _gridData)
                g.LoadContent(content);
        }

        public void Update(BuildingMode mode, Tile selection, ContentManager content)
        {
            foreach (Tile t in _gridData)
            {
                if (mode == BuildingMode.BUILDING)
                {
                    UpdateBuilding(t, selection, content);
                }
                else if (mode == BuildingMode.KEYBINDING)
                {
                    FindLastKeyPressed();
                    UpdateKeybinding(t);
                }
            }
        }

        private void UpdateBuilding(Tile t, Tile selection, ContentManager content)
        {
            if (t.LeftClick())
            {
                if (selection.HasBlockData())
                    t.SetBlockData(selection.GetBlockData().CreateCopy(content));
                else
                    t.ClearBlockData();
            }
            else if (t.RightClick())
            {
                t.ClearBlockData();
            }
        }

        private void FindLastKeyPressed()
        {
            Keys[] keys = Keyboard.GetState().GetPressedKeys();
            if (keys.GetLength(0) > 0)
                _lastKeyPressed = keys[0];
        }

        private void UpdateKeybinding(Tile t)
        {
            if (HasValidBlockType(t))
            {
                if (t.LeftClick())
                    ((Powersource)t.GetBlockData()).SetKey(_lastKeyPressed);
                else if (t.RightClick())
                    ((Powersource)t.GetBlockData()).ClearKey();
            }
        }

        private bool HasValidBlockType(Tile t)
        {
            if (t.HasBlockData())
                return (t.GetBlockData().GetType() == typeof(Powersource) ||
                        t.GetBlockData().GetType().IsSubclassOf(typeof(Powersource)));
            else
                return false;
        }

        public void Draw(SpriteBatch batcher)
        {
            foreach (Tile t in _gridData)
                t.Draw(batcher, _position.X, _position.Y, _scale);
        }

        public bool HasOneCore()
        {
            int numberOfCores = 0;
            foreach (Tile t in _gridData)
                if (t.HasBlockData())
                    if (t.GetBlockData().GetType() == typeof(Core))
                        numberOfCores++;
            return numberOfCores == 1;
        }

        public Block[] ToBlockArray()
        {
            LinkedList<Block> blocks = new LinkedList<Block>();
            for(int i = 0; i < _gridData.GetLength(0); i++)
                for(int j = 0; j < _gridData.GetLength(1); j++)
                    if(_gridData[i, j].HasBlockData())
                        blocks.AddLast(UnwrapGridTile(i, j));
            return blocks.ToArray();
        }

        /// <summary>
        /// Helper function of ConvertToBlockData that unwraps a GridTile.
        /// This function uses the given GridTile to extract its Block and Positiondata,
        /// uses this data to set the Block's relative position, and finally adds the block's adjacent blocks.
        /// </summary>
        private Block UnwrapGridTile(int x, int y)
        {
            Block b = _gridData[x, y].GetBlockData();
            //Set any adjacent blocks for the given block
            if (TileIsValid(x, (y - 1)))
                b.addAdjacentBlock(Direction.UP, _gridData[x, (y - 1)].GetBlockData());
            if (TileIsValid(x, (y + 1)))
                b.addAdjacentBlock(Direction.DOWN, _gridData[x, (y + 1)].GetBlockData());
            if (TileIsValid((x - 1), y))
                b.addAdjacentBlock(Direction.LEFT, _gridData[(x - 1), y].GetBlockData());
            if (TileIsValid((x + 1), y))
                b.addAdjacentBlock(Direction.RIGHT, _gridData[(x + 1), y].GetBlockData());
            return b;
        }

        /// <summary>
        /// Helper function of UnwrapGridTile that tests if a tile is within the range of the 2D grid, 
        /// then tests if that given tile had blockdata
        /// </summary>
        private bool TileIsValid(int x, int y)
        {
            //check if the neighbouring Tile is in the range of the 2D grid
            if ((x >= 0) && (y >= 0) && (x < _gridData.GetLength(0)) && (y < _gridData.GetLength(1)))
                //if so, check if the Tile has BlockData
                return _gridData[x, y].HasBlockData();
            else
                return false;
        }
    }
}
