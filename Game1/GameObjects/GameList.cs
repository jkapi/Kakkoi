using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StrangerCade.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.GameObjects
{
    class GameList : GameObject
    {
        Sprite Covers;
        int totalCovers;
        public float speed;
        float slowing;
        float tt = 0;
        float starttt;
        bool automove;
        Vector2 mousestartpos;

        public int bottompanel;

        IOrderedEnumerable<KeyValuePair<int, Vector2>> sortedpositions;

        public GameList(Vector2 position, int count, bool automatic, float rotationposition = 0, float? speed = null, float? slowing = null) : base(position)
        {
            Random r = new Random();
            this.speed = speed ?? (float)r.NextDouble() * 5 + 3;
            this.slowing = slowing ?? (float)r.NextDouble() * 0.6f + 0.3f;
            automove = automatic;
            totalCovers = count;
        }

        public override void Initialize()
        {
            Covers = new Sprite(Room.Content.Load<Texture2D>("RoomSelect/GameCovers"), 6);
        }

        public override void Update()
        {
            float offset = (float)(Math.PI * 2) / totalCovers;
            Dictionary<int, Vector2> positions = new Dictionary<int, Vector2>();
            for (int i = 0; i < totalCovers; i++)
            {
                positions.Add(i, new Vector2(500 + (float)Math.Sin(tt + offset * i) * 450, 250 + (float)Math.Cos(tt + offset * i) * 200));
            }

            sortedpositions = from entry in positions orderby entry.Value.Y ascending select entry;
            bottompanel = sortedpositions.Last().Key;

            if (!automove && Position.X < 10)
            {
                if (Mouse.CheckPressed(MouseButtons.Left))
                {
                    mousestartpos = Mouse.Position;
                    starttt = tt;
                }
                if (Mouse.Check(MouseButtons.Left))
                    tt = starttt + (Mouse.Position.X - mousestartpos.X) / 500;
                if (Mouse.CheckReleased(MouseButtons.Left))
                {
                    Dictionary<int, float> distances = new Dictionary<int, float>();
                    if (Math.Abs(Mouse.Position.X - mousestartpos.X) < 5)
                    {
                        foreach(var position in sortedpositions)
                        {
                            if (position.Value.Y > 40)
                            {
                                if (new Rectangle((position.Value + new Vector2(460, 200) + Position - Covers.Center).ToPoint(),Covers.Size.ToPoint()).Contains(Mouse.Position))
                                {
                                    distances.Add(position.Key, Vector2.Distance(position.Value + new Vector2(460, 200) + Position, Mouse.Position));
                                }
                            }
                        }
                    }
                    if (distances.Count > 0)
                    {
                        var clickeditem = distances.OrderBy(kvp => kvp.Value).First();
                        switch(clickeditem.Key)
                        {
                            case 0: Room.GotoRoom(typeof(Minigames.ClimbTheMountain.ClimbTheMountain)); break;
                            case 1: Room.GotoRoom(typeof(Minigames.FlySwat.FlySwat)); break;
                            case 2: Room.GotoRoom(typeof(Minigames.DontTapWhite.Donttapwhite)); break;
                            case 3: Room.GotoRoom(typeof(Minigames.Quiz.Quiz)); break;
                            case 4: Room.GotoRoom(typeof(Minigames.DinoCollectStuff.DinoCollectStuff)); break;
                            case 5: Room.GotoRoom(typeof(Rooms.Room1)); break;
                            default: throw new IndexOutOfRangeException("Tried to open a room that does not exist");
                        }
                    }
                }
            }
        }

        public override void Draw()
        {
            foreach (var entry in sortedpositions)
            {
                View.DrawSprite(Covers, entry.Key % Covers.SubImages.Count, entry.Value + new Vector2(460, 200) + Position, new Vector2((entry.Value.Y + 200) / 650), 0, Covers.Center);
            }
            if (automove)
            {
                tt += speed * (float)GameTime.ElapsedGameTime.TotalSeconds;
                speed -= slowing * (float)GameTime.ElapsedGameTime.TotalSeconds;
                if (speed < 0)
                    speed = 0;
            }
        }
    }
}
