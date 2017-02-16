using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipPrototype
{
    /// <summary>
    /// Wrapper class that can store a Block inside itself
    /// for ship creation purposes
    /// </summary>
    class Tile
    {
        private Block _blockData;
        private Point _gridPosition;
        private Texture2D _texture;
        private Texture2D _keybindTexture;
        private Rectangle _drawRectangle;

        public Tile(int x, int y)
        {
            _gridPosition = new Point(x, y);
        }

        public Tile(int x, int y, Block blockData)
        {
            _gridPosition = new Point(x, y);
            SetBlockData(blockData);
        }

        public void LoadContent(ContentManager content)
        {
            _texture = content.Load<Texture2D>("GridTile");
            _keybindTexture = content.Load<Texture2D>("Tick_Blue");
            if (HasBlockData())
                _blockData.LoadContent(content);
        }

        public void Draw(SpriteBatch batcher, float absoluteX, float absoluteY, float scalar)
        {
            if (HasBlockData())
            {
                Block b = GetBlockData();
                DrawTexture(b.GetTexture(), batcher, absoluteX, absoluteY, scalar);
                if (IsPowerSourceOrChild(b) && ((Powersource)b).KeyBinded())
                    DrawTexture(_keybindTexture, batcher, absoluteX, absoluteY, scalar);
            }
            else
            {
                DrawTexture(_texture, batcher, absoluteX, absoluteY, scalar);
            }
        }

        private bool IsPowerSourceOrChild(Block b)
        {
            return b.GetType() == typeof(Powersource) || b.GetType().IsSubclassOf(typeof(Powersource));
        }

        private void DrawTexture(Texture2D texture, SpriteBatch batcher, float absoluteX, float absoluteY, float scalar)
        {
            _drawRectangle.Width = (int)(texture.Width * scalar);
            _drawRectangle.Height = (int)(texture.Height * scalar);
            _drawRectangle.X = (int)(absoluteX + (_gridPosition.X * _drawRectangle.Width));
            _drawRectangle.Y = (int)(absoluteY + (_gridPosition.Y * _drawRectangle.Height));
            batcher.Draw(texture: texture, destinationRectangle: _drawRectangle);
        }

        public bool HasBlockData()
        {
            return _blockData != null;
        }

        public Block GetBlockData()
        {
            return _blockData;
        }

        public void SetBlockData(Block b)
        {
            b.SetRelativePosition(_gridPosition.X, _gridPosition.Y);
            _blockData = b;
        }

        public void ClearBlockData()
        {
            _blockData = null;
        }

        public int GetX()
        {
            return _gridPosition.X;
        }

        public int GetY()
        {
            return _gridPosition.Y;
        }

        public bool LeftClick()
        {
            return (Mouse.GetState().LeftButton == ButtonState.Pressed &&
                    _drawRectangle.Contains(Mouse.GetState().Position));
        }

        public bool RightClick()
        {
            return (Mouse.GetState().RightButton == ButtonState.Pressed &&
                    _drawRectangle.Contains(Mouse.GetState().Position));
        }
    }
}
