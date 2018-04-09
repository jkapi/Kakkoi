using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StrangerCade.Framework;
using StrangerCade.Framework.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using System.IO;
using StrangerCade.Framework.Multiplayer;
using System.Net.Http;
using Lidgren.Network;
using Game1.GameObjects;

namespace Game1.Rooms
{
    class LoginMenu : Room
    {
        private Texture2D Logo;

        private SpriteFont Arial12;
        private SpriteFont Arial16;
        private SpriteFont Arial24;
        

        private TextBox UserBox;
        private TextBox PassBox;
        private TextBox MailBox;

        private Button LoginButton;

        bool registerpage = false;

        private string sessid = "";

        public override void Initialize()
        {
            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Kakoi/token"))
            {
                try
                {
                    sessid = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Kakoi/token");
                    SocketHandler.SetHandler(PacketTypes.LOGINSESSID, LoggedIn);
                    SocketHandler.Connect(sessid, "127.0.0.1");
                }
                catch
                { }
            }
            Logo = Content.Load<Texture2D>("LogoBeta1_0");

            Arial12 = Content.Load<SpriteFont>("arial12");
            Arial16 = Content.Load<SpriteFont>("arial16");
            Arial24 = Content.Load<SpriteFont>("arial24");
            
            UserBox = new TextBox(new Vector2(810, 650), 300, Arial16, "Username");
            PassBox = new TextBox(new Vector2(810, 690), 300, Arial16, "Password", true);
            MailBox = new TextBox(new Vector2(810, 730), 300, Arial16, "E-mail");
            LoginButton = new Button(new Vector2(810, 730), new Vector2(300, PassBox.Height), Arial16, "Login");
            LoginButton.OnClick += LoginButton_OnClick;
            Objects.Add(UserBox);
            Objects.Add(PassBox);
            Objects.Add(MailBox);
            Objects.Add(LoginButton);
        }

        private void LoggedIn(NetIncomingMessage msg)
        {
            if (msg.ReadByte() == 1)
            {
                try
                {
                    File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Kakoi/token", sessid);
                }
                catch { }
                SocketHandler.UserId = msg.ReadInt32();
                SocketHandler.PlayerName = msg.ReadString();
                GotoRoom(typeof(RoomMenu));
            }
        }

        private async void LoginButton_OnClick(object sender, EventArgs e)
        {
            using (var client = SocketHandler.HttpClient)
            {
                var response = await client.PostAsync("https://kakoi.ml/login.php?getsessid", new FormUrlEncodedContent(new Dictionary<string, string>() { { "user", UserBox.Text }, { "pass", PassBox.Text } }));

                sessid = await response.Content.ReadAsStringAsync();
                SocketHandler.Connect(sessid, "127.0.0.1");
            }
        }

        public override void Update()
        {
            if (new Rectangle(960, 570, 190, 60).Contains(Mouse.Position) && Mouse.Check(MouseButtons.Left))
            {
                registerpage = true;
            }
            else if (new Rectangle(770, 570, 190, 60).Contains(Mouse.Position) && Mouse.CheckPressed(MouseButtons.Left))
            {
                registerpage = false;
            }

            if (Keyboard.CheckTriggered(Keys.Tab))
            {
                if (UserBox.Focussed)
                {
                    UserBox.Focussed = false;
                    PassBox.Focussed = true;
                }
                else if (PassBox.Focussed == true)
                {
                    UserBox.Focussed = true;
                    PassBox.Focussed = false;
                }
            }
            if (Keyboard.CheckPressed(Keys.Enter))
            {
                LoginButton.Clicked = true;
            }
        }

        public override void Draw()
        {
            MovingBackground.Draw(this);
            View.DrawTexture(Logo, new Vector2(Graphics.PreferredBackBufferWidth / 2 - Logo.Width / 2, 75));

            if (registerpage)
            {
                View.DrawRectangle(new Rectangle(770, 570, 380, 220), false, new Color(255, 225, 177));
            }
            else
            {
                View.DrawRectangle(new Rectangle(770, 570, 380, 220), false, new Color(177, 245, 249));
            }

            View.DrawRectangle(new Rectangle(770, 570, 190, 60), false, new Color(177, 245, 249));
            View.DrawRectangle(new Rectangle(960, 570, 190, 60), false, new Color(255, 225, 177));

            View.DrawText(Arial24, "Login", new Vector2(860, 600), Color.Black, 0, Arial24.MeasureString("Login") / 2);
            View.DrawText(Arial24, "Register", new Vector2(1060, 600), Color.Black, 0, Arial24.MeasureString("Register") / 2);
        }
    }
}
