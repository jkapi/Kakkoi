using StrangerCade.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Game1.Minigames.FollowTheLeader
{
    class FollowTheLeader : Room
    {
        public Rectangle playField { get; private set; }

        public override void Initialize()
        {
            playField = new Rectangle(new Point(Graphics.PreferredBackBufferWidth/2 - (int)(Graphics.PreferredBackBufferWidth * 0.8)/2, Graphics.PreferredBackBufferHeight / 2 - (int)(Graphics.PreferredBackBufferHeight * 0.8) / 2), new Point((int)(Graphics.PreferredBackBufferWidth * 0.8), (int)(Graphics.PreferredBackBufferHeight * 0.8)));
        }
        public override void Update()
        {
            
        }
        public override void Draw()
        {
            View.DrawRectangle(playField, true, Color.Black);
        }
    }
}
