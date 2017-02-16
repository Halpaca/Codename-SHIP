using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace ShipPrototype
{
    class Core : Powersource
    {
        public Core()
        {

        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            _texture = content.Load<Texture2D>("Block_Core");
        }

        public void Recall()
        {
            //stub
        }

        public override Block CreateCopy(ContentManager content)
        {
            Core c = new Core();
            c.LoadContent(content);
            return c;
        }
    }
}
