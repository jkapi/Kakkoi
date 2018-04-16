using Microsoft.Xna.Framework;
using StrangerCade.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace Game1.Minigames.FollowTheLeader
{
    class PlayerFTL : GameObject
    {
        public int PlayerId { get; private set; }
        public Rectangle HitBox { get; private set; }
        public Rectangle PlayField { get; private set; }
        public int MovementSpeed { get; private set; }

        public PlayerFTL(int playerId, Rectangle playField, Vector2 position, int movementspeed) : base(position)
        {
            PlayerId = playerId;
            PlayField = playField;
            HitBox = new Rectangle(new Point((int)Position.X, (int)Position.Y), new Point(30, 30));
            MovementSpeed = movementspeed;
        }

        public void PlayerMovePosition()
        {
            int checkHitBoxY = HitBox.Y;
            int checkHitBoxX = HitBox.X;
            //snaps the hitbox to the max X playfield

            //Left side
            if (HitBox.X < PlayField.X)
            {
                HitBox = new Rectangle(new Point(PlayField.X, HitBox.Y), new Point(HitBox.Width, HitBox.Height));
            }
            //Right side
            if (HitBox.X+HitBox.Width > PlayField.X + PlayField.Width)
            {
                HitBox = new Rectangle(new Point(PlayField.X+PlayField.Width - HitBox.Width, HitBox.Y), new Point(HitBox.Width, HitBox.Height));
            }

            //snaps the hitbox to the max Y playfield

            //Up side
            if (HitBox.Y < PlayField.Y)
            {
                HitBox = new Rectangle(new Point(HitBox.X, PlayField.Y), new Point(HitBox.Width, HitBox.Height));
            }
            //Down side
            if (HitBox.Y+HitBox.Height > PlayField.Y + PlayField.Height)
            {
                HitBox = new Rectangle(new Point(HitBox.X, PlayField.Height + PlayField.Y - HitBox.Height), new Point(HitBox.Width, HitBox.Height));
            }

            if (HitBox.X + HitBox.Width <= (PlayField.X + PlayField.Width) & HitBox.X >= PlayField.X)
            {
                if (Keyboard.Check(Keys.Left))
                {
                    if ((HitBox.X - MovementSpeed) >= PlayField.X)
                    {
                        HitBox = new Rectangle(new Point((HitBox.X - MovementSpeed), HitBox.Y), new Point(HitBox.Width, HitBox.Height));
                    }
                    else
                    {
                        HitBox = new Rectangle(new Point((PlayField.X), HitBox.Y), new Point(HitBox.Width, HitBox.Height));
                    }
                }
                if (Keyboard.Check(Keys.Right))
                {
                    if ((HitBox.X + MovementSpeed + HitBox.Width) <= (PlayField.X + PlayField.Width))
                    {
                        HitBox = new Rectangle(new Point((HitBox.X) + MovementSpeed, HitBox.Y), new Point(HitBox.Width, HitBox.Height));
                    }
                    else
                    {
                        HitBox = new Rectangle(new Point(PlayField.X+PlayField.Width-HitBox.Width, HitBox.Y), new Point(HitBox.Width, HitBox.Height));
                    }
                }
            }

            if (HitBox.Y + HitBox.Height <= (PlayField.Y + PlayField.Height) & HitBox.Y >= PlayField.Y)
            {
                if (Keyboard.Check(Keys.Up))
                {
                    if (HitBox.Y - MovementSpeed >= PlayField.Y)
                    {
                        HitBox = new Rectangle(new Point(HitBox.X, HitBox.Y - MovementSpeed), new Point(HitBox.Width, HitBox.Height));
                    }
                    else
                    {
                        HitBox = new Rectangle(new Point(HitBox.X, PlayField.Y), new Point(HitBox.Width, HitBox.Height));
                    }
                }
                if (Keyboard.Check(Keys.Down))
                {
                    if (HitBox.Y + HitBox.Height + MovementSpeed <= (PlayField.Y + PlayField.Height))
                    {
                        HitBox = new Rectangle(new Point(HitBox.X, HitBox.Y + MovementSpeed), new Point(HitBox.Width, HitBox.Height));
                    }
                    else
                    {
                        HitBox = new Rectangle(new Point(HitBox.X, (PlayField.Y + PlayField.Height - HitBox.Height)), new Point(HitBox.Width, HitBox.Height));
                    }
                }
            }
        }
    }
}
