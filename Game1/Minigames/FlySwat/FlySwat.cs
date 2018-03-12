using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StrangerCade.Framework;
using Microsoft.Xna.Framework;

namespace Game1.Minigames.FlySwat
{
    class FlySwat : Room
    {
        public override void Initialize()
        {
            Objects.Add(new Fly(new Vector2(new Random().Next(0, 1920), new Random().Next(0, 1080)), new Vector2(1,1), 418, new Rectangle(0, 0, 1920, 1080)));
        }

        public override void Update()
        {

        }

        public override void Draw()
        {

        }
    }
}
