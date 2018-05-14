using StrangerCade.Framework.Multiplayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StrangerCade.Framework;
using StrangerCade.Framework.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Game1.Rooms
{
    class DebugRoom : Room
    {
        SpriteFont Arial;
        TextBox TextBoxPassword;
        TextBox TextBoxUsername;
        Button LoginButton;
        string mplog = "";
        public override void Initialize()
        {
            //Graphics.PreferredBackBufferWidth = 1920;
            //Graphics.PreferredBackBufferHeight = 1080;
            //Graphics.IsFullScreen = false;
            //Graphics.ApplyChanges();
            //Mouse.Cursor = Mouse.DefaultCursor;
            var rooms = GetAllRooms();
            for (int i = 0; i < rooms.Count; i++)
            {
                Arial = Content.Load<SpriteFont>("arial16");
                var btn = new Button(new Rectangle(15, 35 + (i * 28), 500, 25), Arial, rooms[i].FullName.Substring(6));
                btn.OnClick += RoomButtonHandler;
                Objects.Add(btn);
            }
            TextBoxUsername = new TextBox(new Vector2(1290, 75), 400, Arial, "Username/UUID");
            TextBoxPassword = new TextBox(new Vector2(1290, 105), 400, Arial, "IP Address", text: "127.0.0.1");
            Objects.Add(TextBoxUsername);
            Objects.Add(TextBoxPassword);
            LoginButton = new Button(new Rectangle(1700, 105, 150, 27), Arial, "Connect");
            LoginButton.OnClick += LoginButton_OnClick;
            Objects.Add(LoginButton);
            //new Thread(SocketIOHandler.Connect).Start();
        }

        private void LoginButton_OnClick(object sender, EventArgs e)
        {
            LoginButton.Position.Y = (LoginButton.Position.Y == 75) ? 105 : 75;
            new Thread(() => { SocketHandler.Connect(TextBoxUsername.Text, TextBoxPassword.Text); }).Start();
        }

        private void RoomButtonHandler(object sender, EventArgs e)
        {
            Type room = Type.GetType("Game1." + ((Button)sender).Text);
            if (typeof(Room).IsAssignableFrom(room))
                GotoRoom(room);
            else
                throw new NotImplementedException("kan nog niet maat, btw, fuck jou Nick");
        }

        public override void Update()
        {
            Graphics.ApplyChanges();
        }

        public void LoginOk(string sessid)
        {
            LoginButton.Activated = false;
            mplog += "Ingelogd met sessid: " + sessid + "\n";
        }

        public void LoginError()
        {
            mplog += "Kon niet inloggen\n";
        }

        public override void Draw()
        {
            View.DrawLine(new Vector2(640, 0), new Vector2(640, 1080));
            View.DrawLine(new Vector2(1280, 0), new Vector2(1280, 1080));
            View.DrawText(Arial, "Rooms: ", new Vector2(12, 10));
            View.DrawText(Arial, "Multiplayer", new Vector2(1290, 10), null, 0, null, 0, new Vector2(2));
            View.DrawText(Arial, "Login: ", new Vector2(1290, 50));
            View.DrawText(Arial, mplog, new Vector2(1290, 200));
            View.DrawText(Arial, GraphicsDevice.Viewport, new Vector2(650, 10));
        }

        public List<Type> GetAllRooms()
        {
            return AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                 .Where(x => (typeof(Room).IsAssignableFrom(x) | typeof(System.Windows.Forms.Form).IsAssignableFrom(x)) && !x.IsInterface && !x.IsAbstract && x.FullName.StartsWith("Game1"))
                 .Select(x => x).ToList();
        }
    }
}
