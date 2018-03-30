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
using Lidgren.Network;
using System.Threading;

namespace Game1.Rooms
{
    class RoomMenu : Room
    {
        Object ListLock = new object();
        List<Room> Rooms = new List<Room>();
        
        private Texture2D BackBlur;
        private Texture2D BackTriangles;
        private Texture2D Logo;
        private Texture2D LockSmall;

        private SpriteFont Arial12;
        private SpriteFont Arial16;
        private SpriteFont Arial24;

        private TextBox SearchBox;

        private Random rand;

        private Vector2 BackTrianglesPosition;
        private Vector2 BackTrianglesSpeed;

        double lastRefresh = 0;

        public override void Initialize()
        {
            BackBlur = Content.Load<Texture2D>("RoomSelect/back");
            BackTriangles = Content.Load<Texture2D>("RoomSelect/triangle");
            Logo = Content.Load<Texture2D>("LogoBeta1_0");
            LockSmall = Content.Load<Texture2D>("RoomSelect/LockSmall");

            Arial12 = Content.Load<SpriteFont>("arial12");
            Arial16 = Content.Load<SpriteFont>("arial16");
            Arial24 = Content.Load<SpriteFont>("arial24");

            rand = new Random();

            BackTrianglesPosition = new Vector2(rand.Next(0, 1280), rand.Next(0, 720));
            BackTrianglesSpeed = new Vector2(((float)rand.Next(-200, 200)) / 200, ((float)rand.Next(-200, 200)) / 200);

            SearchBox = new TextBox(new Vector2(600, 56), 800, Arial16, "Search...")
            {
                ColorBackground = Color.Transparent,
                ColorBackgroundHover = Color.Transparent,
                ColorBackgroundUnfocussed = Color.Transparent,
                ColorBorderUnfocussed = Color.DimGray,
                ColorBorderHover = Color.LightGray,
                ColorBorder = Color.White,
                ColorText = Color.White,
                ColorTextHover = Color.LightGray,
                ColorTextUnfocussed = Color.LightGray,
                ColorSelect = new Color(0,0,0,64)
            };

            Objects.Add(SearchBox);

            Mouse.Cursor = Mouse.DefaultCursor;
            if (SocketHandler.Connected)
            {
                SocketHandler.SetHandler(PacketTypes.ROOMLIST, ParseRoomList);
                SocketHandler.SendMessage(PacketTypes.ROOMLIST);
            }
        }

        private void ParseRoomList(NetIncomingMessage inc)
        {
            lock (ListLock)
            {
                Rooms.Clear();
                int roomcount = inc.ReadInt32();
                for (int i = 0; i < roomcount; i++)
                {
                    int id = inc.ReadInt32();
                    string name = inc.ReadString();
                    bool locked = inc.ReadBoolean();
                    int playercount = inc.ReadInt32();
                    List<string> players = new List<string>();
                    for (int j = 0; j < playercount; j++)
                    {
                        players.Add(inc.ReadString());
                    }
                    Rooms.Add(new Room(id, name, players, locked));
                }
            }
        }

        public override void Update()
        {
            BackTrianglesPosition += BackTrianglesSpeed;
            if (BackTrianglesPosition.X > 1280) { BackTrianglesPosition.X -= 1280; }
            if (BackTrianglesPosition.X < 0) { BackTrianglesPosition.X += 1280; }
            if (BackTrianglesPosition.Y > 720) { BackTrianglesPosition.Y -= 720; }
            if (BackTrianglesPosition.Y < 0) { BackTrianglesPosition.Y += 720; }


            // Can't get gametime in Initialize :/
            if (lastRefresh == 0) { lastRefresh = GameTime.TotalGameTime.TotalSeconds; }

            // Refresh list every 5 seconds
            if (lastRefresh > 5)
            {
                if (SocketHandler.Connected)
                {
                    SocketHandler.SendMessage(PacketTypes.ROOMLIST);
                }
                lastRefresh = GameTime.TotalGameTime.TotalSeconds;
            }
        }

        public override void Draw()
        {
            View.DrawTexture(BackBlur, new Vector2(-(Mouse.Position.X / 20), -Mouse.Position.Y / 20));

            Vector2 mouseOffset = new Vector2(-(Mouse.Position.X / 1000), -Mouse.Position.Y / 100);

            View.DrawTexture(BackTriangles, BackTrianglesPosition + mouseOffset);
            View.DrawTexture(BackTriangles, BackTrianglesPosition - BackTriangles.Bounds.Size.ToVector2() + mouseOffset);
            View.DrawTexture(BackTriangles, BackTrianglesPosition - new Vector2(BackTriangles.Bounds.Size.X, 0) + mouseOffset);
            View.DrawTexture(BackTriangles, BackTrianglesPosition - new Vector2(0, BackTriangles.Bounds.Size.Y) + mouseOffset);

            View.DrawRectangle(new Rectangle(0, 140, 1920, 1080), false, new Color(Color.Black, 0.1f));

            View.DrawTexture(Logo, new Vector2(20, 20), new Vector2(0.33f, 0.33f));

            DrawPlaceholderRadioBox(new Vector2(600, 90), "Show Friends");
            DrawPlaceholderRadioBox(new Vector2(800, 90), "Show Private");
            DrawPlaceholderRadioBox(new Vector2(1000, 90), "Show Full");

            if (SocketHandler.Connected)
            {
                lock (ListLock)
                {
                    int i = 0;
                    foreach (Room room in Rooms)
                    {
                        string owner = room.Players.Count > 0 ? room.Players[0] : "Server";
                        DrawRoom(new Vector2(255, 200 + i * 112), room.Name, room.Players.Count, owner, room.Locked, room.ID);
                        i++;
                    }
                }
            }
            else
            {
                View.DrawText(Arial24, "You are not connected to the server.", new Vector2(960 - Arial24.MeasureString("You are not connected to the server.").X / 2, 200), Color.LightGray);
            }
        }

        public void DrawPlaceholderRadioBox(Vector2 position, string text)
        {
            View.DrawRectangle(new Rectangle((int)position.X, (int)position.Y, 17, 17), true);

            View.DrawText(Arial12, text, new Vector2(position.X + 20, position.Y), Color.LightGray);
        }

        public void DrawRoom(Vector2 position, string name, int players, string owner, bool locked, int id)
        {
            Rectangle bounds = new Rectangle(position.ToPoint(), new Point(1400, 100));

            Color oldColor = View.DrawColor;
            View.DrawColor = bounds.Contains(Mouse.Position) ? Color.White : Color.LightGray;
            float opacity = bounds.Contains(Mouse.Position) && Mouse.Check(MouseButtons.Left) ? 0.3f: 0.2f;

            View.DrawRectangle(bounds, false, new Color(Color.Black, opacity));

            View.DrawText(Arial24, name, position + new Vector2(20, 20));
            View.DrawText(Arial16, "By " + owner, position + new Vector2(20, 60));

            float numWidth = Arial24.MeasureString(players + "/4").X;
            View.DrawText(Arial24, players + "/4", position + new Vector2(1380 - numWidth, 20), Color.LightGray);
            if (locked)
                View.DrawTexture(LockSmall, position + new Vector2(1359, 60));
            View.DrawColor = oldColor;

            if (Mouse.CheckReleased(MouseButtons.Left) && bounds.Contains(Mouse.Position))
            {
                SocketHandler.SendMessage(PacketTypes.JOINROOM, id);
                StrangerCade.Framework.Room.GotoRoom(typeof(Minigames.FlySwat.FlySwat));
            }
        }

        class Room
        {
            public bool Locked;
            public List<string> Players;
            public string Name;
            public int ID;

            public Room(int id, string name, List<string> players, bool locked)
            {
                ID = id;
                Locked = locked;
                Players = players;
                Name = name;
            }
        }
    }
}
