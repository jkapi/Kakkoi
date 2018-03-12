using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StrangerCade.Framework;
using StrangerCade.Framework.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class DebugRoom : Room
    {
        SpriteFont Arial;
        public override void Initialize()
        {
            Graphics.PreferredBackBufferWidth = 1920;
            Graphics.PreferredBackBufferHeight = 1080;
            Graphics.IsFullScreen = true;
            Mouse.Cursor = Mouse.DefaultCursor;
            var rooms = GetAllRooms();
            for (int i = 0; i < rooms.Count; i++)
            {
                Arial = Content.Load<SpriteFont>("Arial");
                var btn = new Button(new Rectangle(15, 35 + (i * 28), 500, 25), Arial, rooms[i].FullName.Substring(6));
                btn.OnClick += RoomButtonHandler;
                Objects.Add(btn);
            }
            Objects.Add(new TextBox(new Vector2(1290, 75), 400, Arial, "username/uuid"));
            var loginButton = new Button(new Rectangle(1700, 75, 150, 27), Arial, "Connect");
            loginButton.OnClick += LoginButton_OnClick;
            Objects.Add(loginButton);
        }

        private void LoginButton_OnClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void RoomButtonHandler(object sender, EventArgs e)
        {
            Type room = Type.GetType("Game1." + ((Button)sender).Text);
            GotoRoom(room);
        }

        public override void Update()
        {

        }

        public override void Draw()
        {
            View.DrawLine(new Vector2(640, 0), new Vector2(640, 1080));
            View.DrawLine(new Vector2(1280, 0), new Vector2(1280, 1080));
            View.DrawText(Arial, "Rooms: ", new Vector2(12, 10));
            View.DrawText(Arial, "Multiplayer", new Vector2(1290, 10), null, 0, null, 0, new Vector2(2));
            View.DrawText(Arial, "Login: ", new Vector2(1290, 50));
        }

        public List<Type> GetAllRooms()
        {
            return AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                 .Where(x => typeof(Room).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract && !x.FullName.StartsWith("Stranger"))
                 .Select(x => x).ToList();
        }
    }
}
