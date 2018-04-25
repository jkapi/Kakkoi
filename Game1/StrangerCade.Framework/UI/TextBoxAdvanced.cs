using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StrangerCade.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Game1.StrangerCade.Framework;

namespace StrangerCade.Framework.UI
{
    class TextBoxAdvanced : GameObject
    {
        public Vector2 Size { get { return size; } set { size = value; Initialize(); } }
        private Vector2 size = new Vector2(10, 10);
        public SpriteFont Font { get { return font; } set { font = value; Initialize(); } } // Reinitialize when changing font
        private SpriteFont font;
        public string Text;
        public bool Multiline;

        public string Placeholder;
        public bool isPasswordBox;
        public int MaxLength = 32767;

        public bool Hovered { get; private set; } = false;
        public bool Enabled = true;
        public bool Focussed = false;

        public char PasswordChar = '•';

        public int CursorBlinkSpeed = 32;

        public int CursorPosition { get { return cursorPosition; } set { cursorPosition = Math.Min(Text.Length, Math.Max(value, 0)); } }
        private int cursorPosition = 0;

        private string DrawText;
        private Vector2 spaceSize;
        private RenderTarget2D textRenderTarget;

        private int tick;

        public TextBoxAdvanced(Vector2 position, Vector2 size, SpriteFont font, string placeholder = "", bool password = false, string text = "", bool multiline = false) : base(position)
        {
            this.font = font;
            this.size = size;
            Placeholder = placeholder;
            isPasswordBox = password;
            Text = text;
            Multiline = multiline;
        }

        public override void Initialize()
        {
            spaceSize = font.MeasureString(" ");
            textRenderTarget = new RenderTarget2D(Room.GraphicsDevice, (int)Size.X - 6, (int)Size.Y - 6);
        }

        public override void Update()
        {
            Text = Text.Insert(CursorPosition, Keyboard.String);
            CursorPosition += Keyboard.String.Length;
            Keyboard.String = "";

            // Handle Backspace & Delete
            if (Keyboard.CheckTriggered(Keys.Back))
            {
                Text = CursorPosition > 0 ? Text.Remove(CursorPosition - 1, 1) : Text;
                CursorPosition--;
                tick = 0;
            }
            if (Keyboard.CheckTriggered(Keys.Delete))
            {
                Text = CursorPosition < Text.Length ? Text.Remove(CursorPosition, 1) : Text;
                tick = 0;
            }

            // Handle Arrow Left/Right Keys
            if (Keyboard.CheckTriggered(Keys.Left)){ CursorPosition--; tick = 0; }
            if (Keyboard.CheckTriggered(Keys.Right)) { CursorPosition++; tick = 0; }

            // Handle Draw Text
            if (isPasswordBox)
            {
                DrawText = Multiline ? Font.WrapText(new string(PasswordChar, Text.Length), Size.X - 6) : new string(PasswordChar, Text.Length);
            }
            else
            {
                DrawText = Multiline ? Font.WrapText(Text, Size.X - 6) : Text;
            }
            tick++;
        }

        public override void Draw()
        {
            View.DrawRectangle(Position, Size, false, Color.White);
            View.DrawRectangle(Position, Size, true, Color.Black);
            View.DrawRenderTarget(textRenderTarget, Position + new Vector2(3, 3));
        }

        public override void PreDraw()
        {
            Vector2 size = font.MeasureString(DrawText);
            View.SwitchToRenderTarget(textRenderTarget, true, Color.White);
            // Find position on screen where the cursor line has to be drawn
            string[] textLines = DrawText.Split('\n');
            int line;
            int lineChar = 0;
            int cursorOffset = 0;
            for (line = 0; line < textLines.Length; line++)
            {
                if (cursorPosition - cursorOffset <= textLines[line].Length)
                {
                    lineChar = cursorPosition - cursorOffset;
                    break;
                }
                else
                {
                    cursorOffset += textLines[line].Length;
                }
            }
            // Get the coordinates on screen of that position in text
            Vector2 cursorLineTop = font.MeasureString(textLines[line].Substring(0, lineChar));
            cursorLineTop.Y *= line;
            cursorLineTop.X++;
            Vector2 cursorLineBottom = cursorLineTop;
            cursorLineBottom.Y += spaceSize.Y;
            //Draw Text
            View.DrawText(Font, DrawText, Vector2.Zero);
            if (Math.Floor((double)tick / (double)CursorBlinkSpeed) % 2 == 0)
            {
                //Draw Line
                View.DrawLine(cursorLineTop, cursorLineBottom);
            }
            View.SwitchToRenderTarget(null, true, Color.Transparent);
        }
    }
}
