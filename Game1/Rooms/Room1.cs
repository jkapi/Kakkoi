using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StrangerCade.Framework.UI;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using StrangerCade.Framework;
using Microsoft.Xna.Framework.Input;
using StrangerCade.Framework.Multiplayer;
using System.Threading;

<<<<<<< HEAD:Game1/Room1.cs

namespace Game1
=======
namespace Game1.Rooms
>>>>>>> 531dac4a5dc8eb6c51ff3c87db1ba19ea9418d6a:Game1/Rooms/Room1.cs
{
    class Room1 : Room
    {
        SpriteFont Arial;

        public override void Initialize()
        {
            Graphics.ApplyChanges();//*/
            Mouse.Cursor = MouseCursor.FromTexture2D(Content.Load<Texture2D>("transparant"),0,0);
            View.RotationMode = View.RotationType.Degrees;
            Arial = Content.Load<SpriteFont>("arial");
            Objects.Add(new MainBoard());
        }

        public override void Update()
        {

        }

        public override void Draw()
        {

        }
    }
}
