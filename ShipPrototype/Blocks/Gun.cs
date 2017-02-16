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
    class Gun : Block
    {
        private Direction _direction;

        public Gun(Direction direction)
        {
            _direction = direction;
        }

        public override void LoadContent(ContentManager content)
        {
            string s = "";
            switch(_direction)
            {
                case Direction.UP:
                    s = "Up";
                    break;
                case Direction.DOWN:
                    s = "Down";
                    break;
                case Direction.LEFT:
                    s = "Left";
                    break;
                case Direction.RIGHT:
                    s = "Right";
                    break;
            }
            _texture = content.Load<Texture2D>("Block_Gun" + s);
        }
        
        public override Block CreateCopy(ContentManager content)
        {
            Gun g = new Gun(_direction);
            g.LoadContent(content);
            return g;
        }

        public override void RotateCW(ContentManager content)
        {
            switch (_direction)
            {
                case Direction.UP:
                    _direction = Direction.RIGHT;
                    break;
                case Direction.RIGHT:
                    _direction = Direction.DOWN;
                    break;
                case Direction.DOWN:
                    _direction = Direction.LEFT;
                    break;
                case Direction.LEFT:
                    _direction = Direction.UP;
                    break;
            }
            LoadContent(content);
        }
    }
}
