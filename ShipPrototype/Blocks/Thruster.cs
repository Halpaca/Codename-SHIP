using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ShipPrototype
{
    class Thruster : Block
    {
        private Direction _direction;
        private Vector2 _force;

        public Thruster(Direction direction)
        {
            _direction = direction;
            InitForce(50);
        }

        private void InitForce(float force)
        {
            switch (_direction)
            {
                case Direction.UP:
                    _force = new Vector2(0, force);
                    break;
                case Direction.DOWN:
                    _force = new Vector2(0, -force);
                    break;
                case Direction.LEFT:
                    _force = new Vector2(force, 0);
                    break;
                case Direction.RIGHT:
                    _force = new Vector2(-force, 0);
                    break;
            }
        }

        public override void Update()
        {
            base.Update();
            if(IsPowered())
            {
                Console.WriteLine("Powered!");
            }
        }

        public override void LoadContent(ContentManager content)
        {
            string s = "";
            switch (_direction)
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
            _texture = content.Load<Texture2D>("Block_Thruster" + s);
        }

        public override Block CreateCopy(ContentManager content)
        {
            Thruster t = new Thruster(_direction);
            t.LoadContent(content);
            return t;
        }

        public override void RotateCW(ContentManager content)
        {
            switch(_direction)
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
