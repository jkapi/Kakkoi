using Lidgren.Network;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StrangerCade.Framework;
using StrangerCade.Framework.Multiplayer;
using StrangerCade.Framework.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.GameObjects
{
    class RoomList : GameObject
    {
        Object ListLock = new object();
        List<RoomData> Rooms = new List<RoomData>();

        private Texture2D Logo;
        private Texture2D LockSmall;

        private SpriteFont Arial12;
        private SpriteFont Arial16;
        private SpriteFont Arial24;
        private RenderTarget2D roomListRenderTarget;
        private TextBox SearchBox;
        private CheckBox CheckboxFriends;
        double lastRefresh = 0;

        float ListYOffsetTarget = 0;
        float ListYOffset = 0;
        int ScrollWheelOffset = 0;

        float logoPosition = 0;
        float targetLogoPosition = 0;
        private CheckBox CheckboxPrivate;
        private CheckBox CheckboxFull;

        public RoomList() : base(Vector2.Zero)
        {

        }

        public override void Initialize()
        {
            if (!SocketHandler.Connected)
            {
                // Als niet online, stop maar met heel dit object te gebruiken....
                Activated = false;
                return;
            }
            Position = new Vector2(0, 0);
            Logo = Room.Content.Load<Texture2D>("LogoBeta1_0");
            LockSmall = Room.Content.Load<Texture2D>("RoomSelect/LockSmall");

            Arial12 = Room.Content.Load<SpriteFont>("opensans13");
            Arial16 = Room.Content.Load<SpriteFont>("arial16");
            Arial24 = Room.Content.Load<SpriteFont>("arial24");

            roomListRenderTarget = new RenderTarget2D(Room.GraphicsDevice, 1920, 1080);

            SearchBox = new TextBox(new Vector2(600, 56) + Position, 800, Arial16, "Search...")
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
                ColorSelect = new Color(0, 0, 0, 64)
            };
            CheckboxFriends = new CheckBox(new Vector2(600, 90) + Position) { ColorBackground = Color.Transparent, ColorBorder = Color.DimGray, Text = "Show Friends", ColorText = Color.LightGray };
            CheckboxPrivate = new CheckBox(new Vector2(800, 90) + Position, true) { ColorBackground = Color.Transparent, ColorBorder = Color.DimGray, Text = "Show Private", ColorText = Color.LightGray };
            CheckboxFull = new CheckBox(new Vector2(1000, 90) + Position, true) { ColorBackground = Color.Transparent, ColorBorder = Color.DimGray, Text = "Show Full", ColorText = Color.LightGray };

            Room.Objects.AddRange(new GameObject[]{ CheckboxFriends, CheckboxPrivate, CheckboxFull, SearchBox});

            SocketHandler.SetHandler(PacketTypes.ROOMLIST, ParseRoomList);
            SocketHandler.SendMessage(PacketTypes.ROOMLIST);
        }

        public override void Update()
        {
            SearchBox.Position = new Vector2(600 , 56) + Position;
            CheckboxFriends.Position = new Vector2(600, 90) + Position;
            CheckboxPrivate.Position = new Vector2(800, 90) + Position;
            CheckboxFull.Position = new Vector2(1000, 90) + Position;

            // Can't get gametime or mouse in Initialize :/
            if (lastRefresh == 0)
            {
                ScrollWheelOffset = Mouse.ScrollWheelValue;
            }

            // Refresh list every 5 seconds
            if (lastRefresh > 5)
            {
                if (SocketHandler.Connected)
                {
                    SocketHandler.SendMessage(PacketTypes.ROOMLIST);
                }
                lastRefresh = 0;
            }

            ListYOffsetTarget = (Mouse.ScrollWheelValue - ScrollWheelOffset) / 120 * 112;
            if (ListYOffsetTarget > 0)
            {
                ScrollWheelOffset += (int)ListYOffsetTarget;
                ListYOffsetTarget = 0;
            }

            int listScrollHeight = Math.Max(0, Rooms.Count * 112 - 800);
            if (ListYOffsetTarget < -listScrollHeight)
            {
                // Todo: Set scrollwheeloffset to stop "Scrolling any further"
                ListYOffsetTarget = -listScrollHeight;
            }

            // Slowly scroll to right position
            ListYOffset += (ListYOffsetTarget - ListYOffset) / 7;

            lastRefresh += GameTime.ElapsedGameTime.TotalSeconds;

            if (Position.Y < 300)
            {
                targetLogoPosition = 480;
            }
            else
            {
                targetLogoPosition = 0;
            }
            if (logoPosition != targetLogoPosition)
            {
                logoPosition += 0.25f * (targetLogoPosition - logoPosition);
            }
        }

        public override void PreDraw()
        {
            View.SwitchToRenderTarget(roomListRenderTarget, true, Color.Transparent);
            if (SocketHandler.Connected)
            {
                lock (ListLock)
                {
                    int i = 0;
                    foreach (RoomData room in Rooms)
                    {
                        string owner = room.Players.Count > 0 ? room.Players[0] : "Server";
                        DrawRoom(new Vector2(255, ListYOffset + 60 + i * 112), room.Name, room.Players.Count, owner, room.Locked, room.ID);
                        i++;
                    }
                }
            }
            else
            {
                View.DrawText(Arial24, "You are not connected to the server.", new Vector2(960 - Arial24.MeasureString("You are not connected to the server.").X / 2, 200), Color.LightGray);
            }
            View.SwitchToRenderTarget(null);
        }

        public override void Draw()
        {
            View.DrawRectangle(new Vector2(0, 140) + Position, new Vector2(1920, 1080), false, new Color(Color.Black, 0.1f));

            View.DrawTexture(Logo, new Vector2(-450 + logoPosition, 20) + Position, new Vector2(0.33f, 0.33f));

            View.DrawRenderTarget(roomListRenderTarget, new Vector2(0, 140) + Position);
        }
        public void DrawPlaceholderRadioBox(Vector2 position, string text)
        {
            View.DrawRectangle(new Rectangle((int)position.X, (int)position.Y, 17, 17), true);

            View.DrawText(Arial12, text, new Vector2(position.X + 20, position.Y - 3), Color.LightGray);
        }

        public void DrawRoom(Vector2 position, string name, int players, string owner, bool locked, int id)
        {
            Rectangle bounds = new Rectangle(position.ToPoint() + new Point(0, 140) + Position.ToPoint(), new Point(1400, 100));

            Color oldColor = View.DrawColor;
            View.DrawColor = bounds.Contains(Mouse.Position) && Mouse.Position.Y > 140 ? Color.White : Color.LightGray;
            float opacity = bounds.Contains(Mouse.Position) && Mouse.Position.Y > 140 ? 0.25f : 0.2f;
            opacity = bounds.Contains(Mouse.Position) && Mouse.Check(MouseButtons.Left) && Mouse.Position.Y > 140 ? 0.3f : opacity;

            View.DrawRectangle(position, new Vector2(1400, 100), false, new Color(Color.Black, opacity));

            View.DrawText(Arial24, name, position + new Vector2(20, 20));
            View.DrawText(Arial16, "By " + owner, position + new Vector2(20, 60));

            float numWidth = Arial24.MeasureString(players + "/4").X;
            View.DrawText(Arial24, players + "/4", position + new Vector2(1380 - numWidth, 20), Color.LightGray);
            if (locked)
                View.DrawTexture(LockSmall, position + new Vector2(1359, 60));
            View.DrawColor = oldColor;

            if (Mouse.CheckReleased(MouseButtons.Left) && bounds.Contains(Mouse.Position) && Mouse.Position.Y > 140)
            {
                SocketHandler.SendMessage(PacketTypes.JOINROOM, id);
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
                    Rooms.Add(new RoomData(id, name, players, locked));
                }
            }
        }

        class RoomData
        {
            public bool Locked;
            public List<string> Players;
            public string Name;
            public int ID;

            public RoomData(int id, string name, List<string> players, bool locked)
            {
                ID = id;
                Locked = locked;
                Players = players;
                Name = name;
            }
        }
    }
}
