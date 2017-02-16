using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ShipPrototype
{
    class Wire : Block
    {
        private WireType _wireType;
        private Dictionary<Direction, Direction> _transferpoints;

        public Wire(WireType wireType)
        {
            _wireType = wireType;
            _transferpoints = new Dictionary<Direction, Direction>();
            SetTransferpoints();
        }

        /// <summary>
        /// Helper function of the Constructor to help initialize the wires tranferpoints.
        /// The first entry in _transferpoints.Add() defines where the power is going,
        /// where the second entry defines where the power should go after the wire is powered,
        /// </summary>
        private void SetTransferpoints()
        {
            switch(_wireType)
            {
                case WireType.HORIZONTAL:
                    _transferpoints.Add(Direction.LEFT, Direction.LEFT);
                    _transferpoints.Add(Direction.RIGHT, Direction.RIGHT);
                    break;
                case WireType.VERTICAL:
                    _transferpoints.Add(Direction.UP, Direction.UP);
                    _transferpoints.Add(Direction.DOWN, Direction.DOWN);
                    break;
                case WireType.CROSS:
                    _transferpoints.Add(Direction.UP, Direction.UP);
                    _transferpoints.Add(Direction.DOWN, Direction.DOWN);
                    _transferpoints.Add(Direction.LEFT, Direction.LEFT);
                    _transferpoints.Add(Direction.RIGHT, Direction.RIGHT);
                    break;
                case WireType.CURVED:
                    _transferpoints.Add(Direction.DOWN, Direction.RIGHT);
                    _transferpoints.Add(Direction.LEFT, Direction.UP);
                    _transferpoints.Add(Direction.UP, Direction.LEFT);
                    _transferpoints.Add(Direction.RIGHT, Direction.DOWN);
                    break;
                case WireType.CURVED_ALT:
                    _transferpoints.Add(Direction.DOWN, Direction.LEFT);
                    _transferpoints.Add(Direction.RIGHT, Direction.UP);
                    _transferpoints.Add(Direction.UP, Direction.RIGHT);
                    _transferpoints.Add(Direction.LEFT, Direction.DOWN);
                    break;
            }
        }

        public override void LoadContent(ContentManager content)
        {
            string s = "";
            switch (_wireType)
            {
                case WireType.HORIZONTAL:
                    s = "H";
                    break;
                case WireType.VERTICAL:
                    s = "V";
                    break;
                case WireType.CROSS:
                    s = "Cross";
                    break;
                case WireType.CURVED:
                    s = "Curved";
                    break;
                case WireType.CURVED_ALT:
                    s = "CurvedAlt";
                    break;
            }
            _texture = content.Load<Texture2D>("Block_Wire" + s);
        }

        /// <summary>
        /// The Wire powers itself and potentially transfes its power to a valid adjacent block
        /// </summary>
        public override void ReceivePower(Direction d)
        {
            base.ReceivePower(d);
            //test if the wire accepts power from the given direction
            if (_transferpoints.ContainsKey(d))
                TransferPower(_transferpoints[d]);
        }

        /// <summary>
        /// Gives power to a block adjacent to this block in a given direction
        /// </summary>
        private void TransferPower(Direction d)
        {
            //test if this wire has an adjacent neighbour where it can send the power to
            if (_adjacentBlocks.ContainsKey(d))
                //if true, transfer the power to the neighbours direction
                _adjacentBlocks[d].ReceivePower(d);
        }

        public override Block CreateCopy(ContentManager content)
        {
            Wire w = new Wire(_wireType);
            w.LoadContent(content);
            return w;
        }

        public override void RotateCW(ContentManager content)
        {
            switch (_wireType)
            {
                case WireType.HORIZONTAL:
                    _wireType = WireType.VERTICAL;
                    break;
                case WireType.VERTICAL:
                    _wireType = WireType.HORIZONTAL;
                    break;
                case WireType.CURVED:
                    _wireType = WireType.CURVED_ALT;
                    break;
                case WireType.CURVED_ALT:
                    _wireType = WireType.CURVED;
                    break;
            }
            LoadContent(content);
        }
    }
}
