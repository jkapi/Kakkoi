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
        public override void Initialize()
        {
            Objects.Add(new GameObjects.GameList(Vector2.Zero, 6, false));
        }
    }
}
