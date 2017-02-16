using FarseerPhysics;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace ShipPrototype
{
    abstract class Block
    {
        private Vector2 _relativePosition;
        private Vector2 _sizeInPixels;

        protected Texture2D _texture;
        private Rectangle _drawRectangle;

        protected Dictionary<Direction, Block> _adjacentBlocks;

        protected bool _activated;
        protected bool _receivedPower;
        private bool _powered;

        protected Body _body;
        protected Fixture _fixture;
        protected float _weight;

        protected Block()
        {
            _relativePosition = Vector2.Zero;
            _sizeInPixels = new Vector2(48, 48);
            _weight = 1;

            _drawRectangle = new Rectangle();
            _adjacentBlocks = new Dictionary<Direction, Block>();
            _activated = false;
            _receivedPower = false;
        }

        public void AddFixtureToBody(Body shipBody)
        {
            Vector2 simSize = ConvertUnits.ToSimUnits(_sizeInPixels);
            Vertices vertices = PolygonTools.CreateRoundedRectangle(simSize.X, simSize.Y,
                                                       (simSize.X / 4), (simSize.Y / 4), 1);
            vertices.Translate(ConvertUnits.ToSimUnits(_relativePosition * _sizeInPixels));
            _fixture = FixtureFactory.AttachPolygon(vertices, _weight, shipBody);
            //_fixture.Restitution = 0.65f;
        }

        public abstract void LoadContent(ContentManager content);

        public virtual void Update()
        {
            if (_receivedPower)
                _powered = true;
            else
                _powered = false;
            _receivedPower = false;
        }

        public virtual void Draw(SpriteBatch batcher)
        {
            Vector2 relpos = ConvertUnits.ToDisplayUnits(_fixture.Body.Position) + (_relativePosition * _sizeInPixels);

            Vector2 origin = ConvertUnits.ToDisplayUnits(_fixture.Body.WorldCenter);

            Vector2 positionMatrix = Vector2.Transform(relpos - origin, Matrix.CreateRotationZ(_fixture.Body.Rotation)) + origin;

            //Ship has body Draw from relativeposition to ship
            batcher.Draw(texture: _texture,
                         position: positionMatrix,
                         origin: new Vector2(50, 50),
                         scale: _sizeInPixels / 100,
                         rotation: _fixture.Body.Rotation);
        }

        public void SetRelativePosition(int x, int y)
        {
            _relativePosition = new Vector2(x, y);
        }

        public void addAdjacentBlock(Direction d, Block b)
        {
            _adjacentBlocks.Add(d, b);
        }

        public void Activate()
        {
            _activated = true;
        }

        public void Deactivate()
        {
            _activated = false;
        }

        public virtual void ReceivePower(Direction d)
        {
            if (_activated)
                _receivedPower = true;
        }

        public bool IsPowered()
        {
            if (_activated)
                return _powered;
            else
                return false;
        }

        public Texture2D GetTexture()
        {
            return _texture;
        }

        public bool HasBody()
        {
            return _body != null;
        }

        public Body GetBody()
        {
            return _body;
        }

        public abstract void RotateCW(ContentManager content);
        public abstract Block CreateCopy(ContentManager content);
    }
}
 