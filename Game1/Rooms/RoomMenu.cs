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
using Microsoft.Xna.Framework.Input;
using Game1.GameObjects;

namespace Game1.Rooms
{
    class RoomMenu : Room
    {
        Object ListLock = new object();
        List<Room> Rooms = new List<Room>();
        
        private Texture2D Logo;
        private Texture2D LockSmall;

        private SpriteFont Arial12;
        private SpriteFont Arial16;
        private SpriteFont Arial24;

        private TextBox SearchBox;

        double lastRefresh = 0;

        float ListYOffsetTarget = 0;
        float ListYOffset = 0;
        int ScrollWheelOffset = 0;

        RenderTarget2D roomListRenderTarget;

        public override void Initialize()
        {
            Logo = Content.Load<Texture2D>("LogoBeta1_0");
            LockSmall = Content.Load<Texture2D>("RoomSelect/LockSmall");

            Arial12 = Content.Load<SpriteFont>("arial12");
            Arial16 = Content.Load<SpriteFont>("arial16");
            Arial24 = Content.Load<SpriteFont>("arial24");
            
            roomListRenderTarget = new RenderTarget2D(GraphicsDevice, 1920, 1080);

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

            // Can't get gametime or mouse in Initialize :/
            if (lastRefresh == 0) {
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

            ListYOffsetTarget = (Mouse.ScrollWheelValue - ScrollWheelOffset)/120 * 112;
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
        }

        public override void Draw()
        {
            View.SwitchToRenderTarget(roomListRenderTarget, true, Color.Transparent);
            if (SocketHandler.Connected)
            {
                lock (ListLock)
                {
                    int i = 0;
                    foreach (Room room in Rooms)
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
            MovingBackground.Draw(this);

            View.DrawRectangle(new Rectangle(0, 140, 1920, 1080), false, new Color(Color.Black, 0.1f));

            View.DrawTexture(Logo, new Vector2(20, 20), new Vector2(0.33f, 0.33f));

            DrawPlaceholderRadioBox(new Vector2(600, 90), "Show Friends");
            DrawPlaceholderRadioBox(new Vector2(800, 90), "Show Private");
            DrawPlaceholderRadioBox(new Vector2(1000, 90), "Show Full");
            View.DrawRenderTarget(roomListRenderTarget, new Vector2(0, 140));

        }

        public void DrawPlaceholderRadioBox(Vector2 position, string text)
        {
            View.DrawRectangle(new Rectangle((int)position.X, (int)position.Y, 17, 17), true);

            View.DrawText(Arial12, text, new Vector2(position.X + 20, position.Y), Color.LightGray);
        }

        public void DrawRoom(Vector2 position, string name, int players, string owner, bool locked, int id)
        {
            Rectangle bounds = new Rectangle(position.ToPoint() + new Point(0, 140), new Point(1400, 100));

            Color oldColor = View.DrawColor;
            View.DrawColor = bounds.Contains(Mouse.Position) && Mouse.Position.Y > 140 ? Color.White : Color.LightGray;
            float opacity = bounds.Contains(Mouse.Position) && Mouse.Position.Y > 140 ? 0.25f : 0.2f;
            opacity = bounds.Contains(Mouse.Position) && Mouse.Check(MouseButtons.Left) && Mouse.Position.Y > 140 ? 0.3f: opacity;

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
