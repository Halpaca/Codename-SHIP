using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;
using FarseerPhysics;

namespace ShipPrototype
{
    class Ship
    {
        private Block[] _blockData;
        private Body _body;

        private Texture2D _point;

        public Ship(Block[] blockData, World world)
        {
            _blockData = blockData;
            _body = BodyFactory.CreateBody(world);
            _body.BodyType = BodyType.Dynamic;
            _body.Position = ConvertUnits.ToSimUnits(new Vector2(500, 0));
            InitBlocks(world);
        }

        /// <summary>
        /// Helper function of constructor set all ship blocks to their activate state,
        /// so that they respond to their corresponding update cycle
        /// </summary>
        private void InitBlocks(World world)
        {
            foreach (Block b in _blockData)
            {
                b.AddFixtureToBody(_body);
                b.Activate();
            }
            _body.ResetMassData();
        }

        public void LoadContent(ContentManager content)
        {
            _point = content.Load<Texture2D>("Selection_Marker");
            foreach (Block b in _blockData)
                b.LoadContent(content);
        }

        public void Update()
        {
            UpdateBlocks();
        }

        private void UpdateBlocks()
        {
            foreach (Block b in _blockData)
                b.Update();
        }

        public void Draw(SpriteBatch batcher)
        {
            foreach (Block b in _blockData)
                b.Draw(batcher);

            batcher.Draw(texture: _point,
                         position: ConvertUnits.ToDisplayUnits(_body.Position),
                         origin: new Vector2(50, 50),
                         scale: new Vector2(0.2f, 0.2f));
            batcher.Draw(texture: _point,
                         position: ConvertUnits.ToDisplayUnits(_body.WorldCenter),
                         origin: new Vector2(50, 50),
                         scale: new Vector2(0.2f, 0.2f));
            batcher.Draw(texture: _point,
                         position: ConvertUnits.ToDisplayUnits(_body.LocalCenter),
                         origin: new Vector2(50, 50),
                         scale: new Vector2(0.2f, 0.2f));
        }
    }
}
