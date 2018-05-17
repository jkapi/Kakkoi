using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrangerCade.Framework.UI
{
    class Button : GameObject
    {
        public SpriteFont Font;
        public string Text;

        public Color ColorBackground = Color.LightSteelBlue;
        public Color ColorBorder = Color.Black;
        public Color ColorText = Color.Black;
        public Color ColorPlaceholder = Color.Gray;

        public Color ColorBackgroundUnfocussed = Color.WhiteSmoke;
        public Color ColorBorderUnfocussed = Color.DarkGray;
        public Color ColorTextUnfocussed = Color.Black;
        public Color ColorPlaceholderUnfocussed = Color.Gray;

        public Color ColorBackgroundHover = Color.WhiteSmoke;
        public Color ColorBorderHover = Color.DimGray;
        public Color ColorTextHover = Color.Black;
        public Color ColorPlaceholderHover = Color.Gray;

        public Vector2 Size;

        public event EventHandler OnClick;
        public bool Clicked { get; internal set; } = false;
        private bool clicking = false;

        public bool Hover = false;

        public Rectangle Bounds { get { return new Rectangle(Position.ToPoint(), Size.ToPoint()); } set { this.Position = value.Location.ToVector2(); this.Size = value.Size.ToVector2(); } }
        public Rectangle mBounds { get { return new Rectangle((int)(Bounds.X * View.Scale.X), (int)(Bounds.Y * View.Scale.Y), (int)(Bounds.Width * View.Scale.X), (int)(Bounds.Height * View.Scale.Y)); } }

        public Button(Vector2 position, Vector2 size, SpriteFont font, string text) : base(position)
        {
            Font = font;
            Text = text;
            Size = size;
        }

        public Button(Rectangle bounds, SpriteFont font, string text) : base(bounds.Location.ToVector2())
        {
            Font = font;
            Text = text;
            Size = bounds.Size.ToVector2();
        }

        public override void Update()
        {
            if (Clicked)
            {
                if (OnClick != null)
                {
                    OnClick.Invoke(this, new EventArgs());
                }
                Clicked = false;
            }

            if (Mouse.Check(MouseButtons.Left) && Bounds.Contains(Mouse.Position))
            {
                clicking = true;
            }
            else
            {
                clicking = false;
            }
            if (Bounds.Contains(Mouse.Position))
            {
                Hover = true;
                if (Mouse.CheckReleased(MouseButtons.Left))
                {
                    Clicked = true;
                }
                Mouse.Cursor = MouseCursor.Hand;
            }
            else
            {
                if (Hover == true)
                {
                    Mouse.Cursor = Mouse.DefaultCursor;
                }
                Hover = false;
            }
        }

        public override void Draw()
        {
            Vector2 textMiddle = Font.MeasureString(Text) / 2;
            if (clicking)
            {
                View.DrawRectangle(Bounds, false, ColorBackground);
                View.DrawRectangle(Bounds, true, ColorBorder);
                View.DrawText(Font, Text, Bounds.Center.ToVector2(), ColorText, 0, textMiddle);
            }
            else if (Hover)
            {
                View.DrawRectangle(Bounds, false, ColorBackgroundHover);
                View.DrawRectangle(Bounds, true, ColorBorderHover);
                View.DrawText(Font, Text, Bounds.Center.ToVector2(), ColorTextHover, 0, textMiddle);
            }
            else
            {
                View.DrawRectangle(Bounds, false, ColorBackgroundUnfocussed);
                View.DrawRectangle(Bounds, true, ColorBorderUnfocussed);
                View.DrawText(Font, Text, Bounds.Center.ToVector2(), ColorTextUnfocussed, 0, textMiddle);
            }
        }
    }
}
