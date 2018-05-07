using StrangerCade.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Rooms
{
    class LoginMenu : Room
    {
        public override void Initialize()
        {

        }

        public override void Update()
        {
            GotoRoom(typeof(RoomMenu));
        }

        public override void Draw()
        {
            
        }
    }
}
