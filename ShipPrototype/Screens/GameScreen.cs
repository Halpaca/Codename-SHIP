using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ShipPrototype
{
    class GameScreen : Screen
    {
        private Ship _ship;
        private World _world;

        private Body _floor;

        public GameScreen(ContentManager content, Grid grid) : base(content)
        {
            ConvertUnits.SetDisplayUnitToSimUnitRatio(64f); // 64 pixels = 1 Metre
            _world = new World(new Vector2(0, 9.8f));
            _ship = new Ship(grid.ToBlockArray(), _world);
        }

        public override void LoadContent()
        {
            _ship.LoadContent(_content);
            
            _floor = BodyFactory.CreateRectangle(_world, 20, 1, 1);
            _floor.BodyType = BodyType.Static;
            _floor.Position = ConvertUnits.ToSimUnits(new Vector2(640, 752));
        }

        public override void Update(GameTime time)
        {
            _ship.Update();
            _world.Step((float)time.ElapsedGameTime.TotalSeconds);
        }

        public override void Draw(SpriteBatch batcher)
        {
            _ship.Draw(batcher);
        }
    }
}
