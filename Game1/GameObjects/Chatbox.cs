using Game1.StrangerCade.Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StrangerCade.Framework;
using StrangerCade.Framework.Multiplayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;
using System.IO;

namespace Game1.GameObjects
{
    class Chatbox : GameObject
    {
        public Chatbox() : base(Vector2.Zero) { }
        private List<Message> messages = new List<Message>();
        private SpriteFont font;

        public override void Initialize()
        {
            SocketHandler.SetHandler(PacketTypes.CHAT, addMessage);

            font = Room.Content.Load<SpriteFont>("OpenSans13");
        }

        private void addMessage(NetIncomingMessage message)
        {
            messages.Add(new Message(message.ReadString(), message.ReadString(), message.ReadInt32()));
        }

        public override void Update()
        {
            foreach(Message message in messages)
            {
                if (!message.Handled)
                {
                    message.Text = font.WrapText(message.Text, 1890 - font.MeasureString(message.Sender).X).Trim();
                    if (message.Text.Length > 0)
                        if (message.Text[0] =='\n') { message.Text = message.Text.Substring(1); }
                    message.Handled = true;
                }
            }
            if (Keyboard.CheckPressed(Microsoft.Xna.Framework.Input.Keys.A))
            {
                var s = new MemoryStream();
                var m = new StreamWriter(s);
                m.Flush();
                SocketHandler.SendMessage(PacketTypes.CHAT, "Hallo allemaal");
                
            }
        }

        public override void Draw()
        {
            View.DrawRectangle(new Rectangle(0, 728, 1920, 352), false, new Color(0.0f, 0.0f, 0.0f, 0.3f));
            float y = 740;
            for (int i = 0; i < messages.Count; i++)
            {
                float nameWidth = font.MeasureString(messages[i].Sender).X;
                DrawTextOutlined(font, messages[i].Sender + ":", new Vector2(10, y), Color.White, messages[i].Color);
                View.DrawText(font, messages[i].Text, new Vector2(18 + nameWidth, y), Color.White);
                y += font.MeasureString(messages[i].Text).Y;
            }
        }
        private void DrawTextOutlined(SpriteFont font, object text, Vector2 position, Color textColor, Color outlineColor, int borderWidth = 1)
        {
            // Measure full height of text
            Vector2 stringSize = font.MeasureString("Aj");

            for (int x = -borderWidth; x <= borderWidth; x++)
            {
                for (int y = -borderWidth; y <= borderWidth; y++)
                {
                    View.DrawText(font, text, position + new Vector2(x, y), outlineColor);
                }
            }
            View.DrawText(font, text, position, textColor);
        }

        private class Message
        {
            public bool Handled;
            public string Text;
            public string Sender;
            public Color Color;
            public DateTime Time;

            public Message(string sender, string text, int color)
            {
                Text = text;
                Sender = sender;
                Time = DateTime.Now;
                switch(color)
                {
                    case 0: Color = Color.DarkRed; break;
                    case 1: Color = Color.DarkBlue; break;
                    case 2: Color = Color.DarkGreen; break;
                    case 3: Color = Color.Black; break;
                    default: Color = Color.Yellow; break;
                }
            }
        }
    }
}
