using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StrangerCade.Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.Minigames.FlySwat
{
    class FlySwat : Room
    {
        Random random = new Random();
        int respawndelay = 0;
        public override void Initialize()
        {
            for (int i = 0; i < 10; i++)
                SpawnFly();
            respawndelay = random.Next(10, 30);
            Mouse.SetCursor(new Sprite(Content.Load<Texture2D>("minigame/flyswat/vliegennmepper"), 4), 1, new Point(14,16));
        }

        public override void Update()
        {
            respawndelay--;
            if (respawndelay == 0)
            {
                respawndelay = random.Next(10, 30);
                SpawnFly();
            }
        }

        public override void Draw()
        {
        }

        private void SpawnFly()
        {
            Objects.Add(new Fly(new Vector2(random.Next(0, 1920), random.Next(0, 1080)), new Vector2(1, 1), random.Next()));
        }
    }
}
