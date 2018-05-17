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
using Lidgren.Network;

namespace Game1.Rooms
{
    class DebugRoom : Room
    {
        SpriteFont Arial;
        static TextBox TextBoxIP;
        static TextBox TextBoxUUID;
        Button LoginButton;
        static string mplog = "";
        bool triedlogin = false;

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
            if (TextBoxUUID == null)
            {
                TextBoxUUID = new TextBox(new Vector2(1290, 75), 400, Arial, "UUID");
                TextBoxIP = new TextBox(new Vector2(1290, 105), 400, Arial, "IP Address", text: "127.0.0.1");
            }
            Objects.Add(TextBoxUUID);
            Objects.Add(TextBoxIP);
            LoginButton = new Button(new Rectangle(1700, 105, 150, 27), Arial, "Connect");
            LoginButton.OnClick += LoginButton_OnClick;
            Objects.Add(LoginButton);
        }

        private void LoggedIn(NetIncomingMessage msg)
        {
            if (msg.ReadByte() == 1)
            {
                try
                {
                    if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Kakoi/"))
                        Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Kakoi/");
                    File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Kakoi/token", TextBoxUUID.Text);
                }
                catch { }
                SocketHandler.UserId = msg.ReadInt32();
                SocketHandler.PlayerName = msg.ReadString();
                LoginButton.Activated = false;
                mplog += "Ingelogd als " + SocketHandler.PlayerName +"\n";
            }
        }

        private void LoginButton_OnClick(object sender, EventArgs e)
        {
            LoginButton.Position.Y = (LoginButton.Position.Y == 75) ? 105 : 75;
            new Thread(() => { SocketHandler.Connect(TextBoxUUID.Text, TextBoxIP.Text); }).Start();
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

            if (triedlogin == false)
            {
                SocketHandler.SetHandler(PacketTypes.LOGINSESSID, LoggedIn);
                if (!SocketHandler.Connected)
                {
                    if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Kakoi/token"))
                    {
                        try
                        {
                            TextBoxUUID.Text = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Kakoi/token");
                            if (TextBoxUUID.Text.Length == 32)
                            {
                                SocketHandler.Connect(TextBoxUUID.Text, TextBoxIP.Text);
                            }
                        }
                        catch (IOException)
                        { mplog += "Kon sessid bestand niet uitlezen!\n"; }
                    }
                }
                triedlogin = true;
            }
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
