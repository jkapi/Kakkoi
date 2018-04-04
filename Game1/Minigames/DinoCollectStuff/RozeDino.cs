using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StrangerCade.Framework;

namespace Game1.Minigames.DinoCollectStuff
{
    class RozeDino : GameObject
    {
        Sprite DinoSprite;
        float xleft;
        float xright;
        float xspeed;

        public RozeDino(Vector2 position, float xleft, float xright, float startspeed) : base(position)
        {
            this.xleft = xleft;
            this.xright = xright;
            this.xspeed = startspeed;
        }

        public override void Initialize()
        {
            DinoSprite = new Sprite(Room.Content.Load<Texture2D>("minigame/dinozooi/clown"));
        }
        public override void Update()
        {
            if (Position.X <= xleft || Position.X >= xright)
            {
                xspeed *= -1;
            }
            Position.X += xspeed;
        }

        public override void Draw()
        {
            if (xspeed > 0)
                View.DrawSpriteStretched(DinoSprite, 0, Position, new Vector2(44,70));
            else
                View.DrawSpriteStretched(DinoSprite, 0, Position, new Vector2(44, 70), 0, null, 0, null, SpriteEffects.FlipHorizontally);
        }
    }
}
