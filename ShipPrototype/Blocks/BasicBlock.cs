using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ShipPrototype.Blocks
{
    class BasicBlock : Block
    {
        public override void LoadContent(ContentManager content)
        {
            _texture = content.Load<Texture2D>("Block_Basic");
        }

        public override Block CreateCopy(ContentManager content)
        {
            BasicBlock b = new BasicBlock();
            b.LoadContent(content);
            return b;
        }

        public override void RotateCW(ContentManager content)
        {
            //BasicBlock only has one orientation;
        }
    }
}
