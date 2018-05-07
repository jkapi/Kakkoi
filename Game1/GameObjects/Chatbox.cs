using Game1.StrangerCade.Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StrangerCade.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.GameObjects
{
    class Chatbox : GameObject
    {
        public Chatbox() : base(Vector2.Zero) { }
        private List<Message> messages = new List<Message>();
        private SpriteFont font;

        public override void Initialize()
        {
            messages.Add(new Message("joeykapi", "Dit is een berichtje die ik ooit heb gestuurd", 0));
            messages.Add(new Message("joeykapi", "nog een", 0));
            messages.Add(new Message("CrazyCreeper", "Dit gaat je zo erg irriteren omdat deze tekst veelste breed is. Omdat deze tekst veelste breed is gaat hij niet passen. Dit is om te testen of het past, ik hoop van wel. Maar je weet natuurlijk maar nooit." +
                " Het is zelfs zo lang dat het in code niet past in een regel. Bij lange na niet. Ach, ik weet niets meer. Lorem ipsum dolor sit amet constnogwat tekst. Weet ik veel. Bacon bacon brisket french fries. En het gaat maar door en door en door en door" +
                " en door en door en door en door en door en door en door en door en door en door en door en door en door en door en door en door en door en door en door en door en door en door en door en door en door en door en door en door en door en door en door" +
                " en door en door en door en door en door en door en door en door en door en door en door en door en door en door en door en door en door en door en door en door en door en door en door en door en door en door en door en door en door en door en door" + 
                " en pa's wijze lynx bezag vroom over het fikse aquaduct4", 2));
            messages.Add(new Message("PiemelMan", "Pls stop", 3));
            messages.Add(new Message("CrazyCreeper", "okidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokidokido", 2));
            messages.Add(new Message("xXx_PussySlayer69_xXx", "ik neuk jullie allemaal de moeder", 1));
            messages.Add(new Message("CrazyCreeper", "je moeder", 2));
            messages.Add(new Message("PiemelMan", "gvd, ga je nog iets doen?", 3));
            messages.Add(new Message("joeykapi", "nee", 0));
            messages.Add(new Message("joeykapi", "dit is maar een voorbeeld", 0));
            messages.Add(new Message("PiemelMan", "ok :(", 1));

            font = Room.Content.Load<SpriteFont>("OpenSans13");
        }

        public override void Update()
        {
            foreach(Message message in messages)
            {
                if (!message.Handled)
                {
                    message.Text = font.WrapText(message.Text, 1890 - font.MeasureString(message.Sender).X).Trim();
                    if (message.Text[0] =='\n') { message.Text = message.Text.Substring(1); }
                    message.Handled = true;
                }
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
