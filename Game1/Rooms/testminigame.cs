using StrangerCade.Framework.Multiplayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StrangerCade.Framework;
using StrangerCade.Framework.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;
using System.Net.Http;
using Game1.Helpers;
using System.Diagnostics;

namespace Game1.Rooms
{
    class Testminigame : Room
    {
        Sprite Covers;
        int totalCovers = 8;
        float speed;
        float slowing;
        float tt = 0;

        public override void Initialize()
        {
            Covers = new Sprite(Content.Load<Texture2D>("RoomSelect/GameCovers"), 4);
            Random r = new Random();
            speed = (float)r.NextDouble() * 4 + 1;
            slowing = (float)r.NextDouble() * 0.5f + 0.1f;
        }

        public override void Draw()
        {
            tt += speed * (float)GameTime.ElapsedGameTime.TotalSeconds;
            float offset = (float)(Math.PI * 2) / totalCovers;
            Dictionary<int, Vector2> positions = new Dictionary<int, Vector2>();
            for(int i = 0; i < totalCovers; i++)
            {
                positions.Add(i, new Vector2(500 + (float)Math.Sin(tt + offset * i) * 600, 250 + (float)Math.Cos(tt + offset * i) * 200));
            }

            var sortedpositions = from entry in positions orderby entry.Value.Y ascending select entry;

            foreach (var entry in sortedpositions)
            {
                View.DrawSprite(Covers, entry.Key % Covers.SubImages.Count, entry.Value + new Vector2(200), new Vector2((entry.Value.Y + 200) / 650), 0, Covers.Center);
            }
            speed -= slowing * (float)GameTime.ElapsedGameTime.TotalSeconds;
            if (speed < 0)
                speed = 0;
        }
    }
}
