using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StrangerCade.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace StrangerCade.Framework.UI
{
    /*
     *      It's dangerous to go alone. 
     *      Take this ASCII sword.  
     *      o()xxxx[{::::::::::::::::::::::::::::::::::>
     */
    class TextBox : GameObject
    {
        public static string Clipboard = "";
        string Text { get { return _text; } set { _text = value; UpdateTextSize(); } }
        string _text = "";
        string PlaceholderText = "";

        public SpriteFont Font;
        public Color ColorBackground = Color.White;
        public Color ColorBorder = Color.Black;
        public Color ColorText = Color.Black;
        public Color ColorPlaceholder = Color.Gray;

        public Color ColorBackgroundUnfocussed = Color.White;
        public Color ColorBorderUnfocussed = Color.DarkGray;
        public Color ColorTextUnfocussed = Color.Black;
        public Color ColorPlaceholderUnfocussed = Color.Gray;

        public Color ColorBackgroundHover = Color.White;
        public Color ColorBorderHover = Color.DimGray;
        public Color ColorTextHover = Color.Black;
        public Color ColorPlaceholderHover = Color.Gray;

        public Color ColorSelect = Color.DodgerBlue;
        public Color ColorTextSelect = Color.White;

        string drawtext;
        int cursorpos = 0;
        int show = 0;
        int showLength = 15;
        List<Vector2> textSize = new List<Vector2>();

        public char PasswordChar = '•';
        public bool Password = false;
        public bool Focussed = false;
        public bool Hover { get; internal set; } = false;
        Rectangle bounds;
        public int Width;
        public int Height { get { return bounds.Height; } }

        public int SelectStart = -1;
        public int SelectEnd = -1;

        int _selectstart = -1;
        int _selectend = -1;
        int _selectlength { get { return _selectend - _selectstart; } }

        Vector2 mouseStartPosition;
        Vector2 mousePosition;

        public TextBox(Vector2 position, int width, SpriteFont font, string placeholder = "", bool password = false, string text = "") : base(position)
        {
            Font = font;
            Width = width;
            PlaceholderText = placeholder;
            Password = password;
            Text = text;
            bounds = new Rectangle(Position.ToPoint(), new Point(Width, (int)Font.MeasureString("A").Y + 3));
            if (Password)
            {
                drawtext = new string((char)PasswordChar, Text.Length);
            }
            else
            {
                drawtext = Text;
            }

            textSize.Clear();
            textSize.Add(new Vector2(0, Font.MeasureString("A").Y));
            for (int i = 1; i <= Text.Length; i++)
            {
                textSize.Add(Font.MeasureString(drawtext.Substring(0, i)));
            }
        }

        public override void Update()
        {
            // only catch keys if focussed
            if (Focussed)
            {
                HandleKeyboard();

                if (Password)
                {
                    // Show password characters
                    drawtext = new string((char)PasswordChar, Text.Length);
                }
                else
                {
                    // Show normal text
                    drawtext = Text;
                }

                // Keep text fitting in textbox by deleting characters at end that don't fit
                while (Font.MeasureString(drawtext).X > Width - 6)
                {
                    drawtext = drawtext.Substring(0, drawtext.Length - 1);
                    Text = Text.Substring(0, Text.Length - 1);
                    cursorpos--;
                }
                
                UpdateTextSize();
            }

            HandleMouse();

            // You're not selecting when the begin and end of selection are at the same position ;)
            // Failsafe for drawing...
            if (SelectStart == SelectEnd && SelectStart != -1)
            {
                cursorpos = SelectStart;
                SelectStart = -1;
                SelectEnd = -1;
            }
        }

        override public void Draw()
        {
            // Never get enough of this *kuch*
            _selectstart = Math.Min(SelectStart, SelectEnd);
            _selectend = Math.Max(SelectStart, SelectEnd);

            // Simple much used vector
            Vector2 offset = new Vector2(3, 0);

            if (Focussed)
            {
                View.DrawRectangle(bounds, false, ColorBackground);
                View.DrawRectangle(bounds, true, ColorBorder);
                if (SelectStart != -1) // If selecting text
                {
                    // Get text before, after and the selected text.
                    string beforetext = drawtext.Substring(0, _selectstart);
                    string selecttext = drawtext.Substring(_selectstart, _selectlength);
                    string aftertext = drawtext.Substring(_selectend);

                    // Get sizes of text before selected text and selected text
                    Vector2 beforeSize = Font.MeasureString(beforetext);
                    Vector2 selectSize = Font.MeasureString(selecttext);

                    // Get rectangle for selected text background
                    Rectangle selectbox = new Rectangle((int)(Position.X + beforeSize.X + 3), (int)Position.Y + 3, (int)selectSize.X, (int)bounds.Height - 7);

                    // Draw in parts
                    View.DrawText(Font, beforetext, Position + offset, ColorText);
                    View.DrawRectangle(selectbox, false, ColorSelect);
                    View.DrawText(Font, selecttext, Position + offset + new Vector2(beforeSize.X, 0), ColorTextSelect);
                    View.DrawText(Font, aftertext, Position + offset + new Vector2(beforeSize.X, 0) + new Vector2(selectSize.X, 0), ColorText);
                }
                else
                {
                    if (Text.Length == 0)
                    {
                        View.DrawText(Font, PlaceholderText, Position + offset, ColorPlaceholder);
                    }
                    View.DrawText(Font, drawtext, Position + offset, ColorText);

                    // Draw cursor every half second, or when forced to draw
                    if ((GameTime.TotalGameTime.TotalSeconds % 1 > 0.5 | show > 0) && textSize.Count > 0)
                    {
                        View.DrawLine(new Vector2(textSize[cursorpos].X, 3) + Position + offset, new Vector2(textSize[cursorpos].X + 3, textSize[cursorpos].Y) + Position);
                    }
                }
            }
            else if (Hover)
            {
                View.DrawRectangle(bounds, false, ColorBackgroundHover);
                View.DrawRectangle(bounds, true, ColorBorderHover);
                View.DrawText(Font, drawtext, Position + offset, ColorTextHover);
                if (Text.Length == 0)
                {
                    View.DrawText(Font, PlaceholderText, Position + offset, ColorPlaceholderHover);
                }
            }
            else
            {
                View.DrawRectangle(bounds, false, ColorBackgroundUnfocussed);
                View.DrawRectangle(bounds, true, ColorBorderUnfocussed);
                View.DrawText(Font, drawtext, Position + offset, ColorTextUnfocussed);
                if (Text.Length == 0)
                {
                    View.DrawText(Font, PlaceholderText, Position + offset, ColorPlaceholderUnfocussed);
                }
            }
            // force show cursor <<<
            show--;
        }

        /// <summary>
        /// Handles all mouse interaction
        /// </summary>
        private void HandleMouse()
        {
            bool wasHovered = Hover;
            Hover = bounds.Contains(Mouse.Position);

            if (Hover)
            {
                Mouse.Cursor = MouseCursor.IBeam;
            }
            else
            {
                if (wasHovered)
                {
                    Mouse.Cursor = Mouse.DefaultCursor;
                }
            }

            if (Mouse.CheckPressed(MouseButtons.Left))
            {
                if (Hover)
                {
                    Focussed = true;
                    Keyboard.String = "";

                    MoveCursorToCharacterByPosition(Mouse.Position.ToPoint());
                    mouseStartPosition = Mouse.Position;
                }
                else
                {
                    // If clicked outside textbox, stop focussing
                    Focussed = false;
                }
            }

            if (Mouse.Check(MouseButtons.Left))
            {
                mousePosition = Mouse.Position;
                SelectStart = GetCharacterByPosition(mouseStartPosition.ToPoint());
                SelectEnd = GetCharacterByPosition(mousePosition.ToPoint());
            }
        }

        /// <summary>
        /// Handles all keyboard interaction
        /// </summary>
        private void HandleKeyboard()
        {
            // Shorthand for some large pieces of code
            bool ctrl = Keyboard.Check(Keys.LeftControl) || Keyboard.Check(Keys.RightControl);
            bool shift = Keyboard.Check(Keys.LeftShift) || Keyboard.Check(Keys.RightShift);

            // if control button is pressed
            if (ctrl)
            {
                ////////////////Handle selection////////////////
                //Select all
                if (Keyboard.CheckPressed(Keys.A))
                {
                    SelectStart = 0;
                    SelectEnd = Text.Length;
                }

                if (Keyboard.CheckPressed(Keys.Left))
                {
                    if (shift)
                    {
                        // Select all to left if shift is pressed
                        SelectStart = cursorpos;
                        SelectEnd = 0;
                        cursorpos = SelectEnd;
                    }
                    else
                    {
                        // Put cursor at begin of text
                        cursorpos = 0;
                        show = showLength;
                        // And stop selecting
                        SelectStart = -1;
                        SelectEnd = -1;
                    }
                }

                if (Keyboard.CheckPressed(Keys.Right))
                {
                    if (shift)
                    {
                        // Select all to right if shift is pressed
                        SelectStart = cursorpos;
                        SelectEnd = Text.Length;
                        cursorpos = SelectEnd;
                    }
                    else
                    {
                        // Put cursor at end of text
                        cursorpos = Text.Length;
                        show = showLength;
                        // And stop selecting
                        SelectStart = -1;
                        SelectEnd = -1;
                    }
                }

                // Already prepare visible start and end of selection for clipboard handling
                _selectstart = Math.Min(SelectStart, SelectEnd);
                _selectend = Math.Max(SelectStart, SelectEnd);

                ////////////////Handle Clipboard////////////////
                // Cut text
                if (Keyboard.CheckPressed(Keys.X) && SelectStart != -1) // Can only happen if selecting text...
                {
                    // Only cut text if this isn't a password box
                    if (!Password)
                    {
                        // Put selected text in clipboard
                        Clipboard = Text.Substring(_selectstart, _selectlength);
                        // Remove it from text
                        Text = Text.Remove(_selectstart, _selectlength);
                        // put cursor the the visible begin of the selection
                        cursorpos = _selectstart;
                        // and stop selecting text
                        SelectStart = -1;
                        SelectEnd = -1;
                    }
                }

                // Copy text
                if (Keyboard.CheckPressed(Keys.C) && SelectStart != -1) // Can only happen if selecting text...
                {
                    // Only copy text if this isn't a password box
                    if (!Password)
                    {
                        // Put selected text in clipboard
                        Clipboard = Text.Substring(_selectstart, _selectlength);
                    }
                }

                // Paste text
                if (Keyboard.CheckPressed(Keys.V))
                {
                    // If text is selected
                    if (SelectStart != -1)
                    {
                        // replace the text with the clipboard text
                        Text = Text.Remove(_selectstart, _selectlength);
                        Text = Text.Insert(_selectstart, Clipboard);
                        // and set the cursor to the end of the pasted text
                        cursorpos = _selectstart + Clipboard.Length;
                        // and stop selecting
                        SelectStart = -1;
                        SelectEnd = -1;
                    }
                    else
                    {
                        // Just paste the text on the cursor position
                        Text = Text.Insert(cursorpos, Clipboard);
                        // and put the cursor after the pasted text
                        cursorpos += Clipboard.Length;
                    }
                }
            }
            else // If not ctrl
            {
                if (Keyboard.String.Length > 0) // if there's text in the "buffer"
                {
                    if (SelectStart != -1) // if selecting, remove selected text
                    {
                        Text = Text.Remove(_selectstart, _selectlength);
                        cursorpos = _selectstart;
                        SelectStart = -1;
                        SelectEnd = -1;
                    } // and continue as usual
                    // Add text in Keyboard.String to Text
                    Text = Text.Insert(cursorpos, Keyboard.String);
                    // Move the cursor
                    cursorpos += Keyboard.String.Length;
                    // Keep cursor visible
                    if (Keyboard.String.Length > 0)
                    {
                        show = showLength;
                    }
                }

                if (Keyboard.CheckTriggered(Keys.Left))
                {
                    if (shift)
                    {
                        if (SelectStart > -1) // If already selecting
                        {
                            // Move the end to the left, until the begin of the text
                            SelectEnd = Math.Max(0, SelectEnd - 1);
                            cursorpos = SelectEnd;
                        }
                        else
                        {
                            // If behind the first character (you can't select from cursor position = 0 because it's before the text)
                            if (cursorpos > 0)
                            {
                                // select the previous character
                                SelectStart = cursorpos;
                                SelectEnd = SelectStart - 1;
                            }
                        }
                    }
                    else // if not pressing shift
                    {
                        // move the cursor to the left
                        cursorpos = Math.Max(cursorpos - 1, 0);
                        // keep cursor visible
                        show = showLength;
                        // and stop selecting
                        SelectStart = -1;
                        SelectEnd = -1;
                    }
                }

                if (Keyboard.CheckTriggered(Keys.Right))
                {
                    if (shift)
                    {
                        if (SelectStart > -1) // If already selecting
                        {
                            // Move the Selection End to the right
                            SelectEnd = Math.Min(Text.Length, SelectEnd + 1); // lock at end of text
                            cursorpos = SelectEnd;
                        }
                        else
                        {
                            if (cursorpos < Text.Length) // if cursor isn't at the end of the text
                            {
                                // select the character to the right
                                SelectStart = cursorpos;
                                SelectEnd = SelectStart + 1;
                            }
                        }
                    }
                    else // if not pressing shift
                    {
                        // go to the right
                        cursorpos = Math.Min(cursorpos + 1, Text.Length);
                        // force show cursor
                        show = showLength;
                        // and stop selecting
                        SelectStart = -1;
                        SelectEnd = -1;
                    }
                }

                // And also set the visible selection start and end because it's used for removing text
                _selectstart = Math.Min(SelectStart, SelectEnd);
                _selectend = Math.Max(SelectStart, SelectEnd);
            }

            // Clear keyboard string
            Keyboard.String = "";

            // If backspace is pressed and the cursor is at least behind one character (unless selecting)
            if (Keyboard.CheckTriggered(Keys.Back) && (cursorpos > 0 | SelectStart != -1))
            {
                if (SelectStart != -1) // If selecting
                {
                    cursorpos = _selectstart;
                    // remove selected text
                    Text = Text.Remove(_selectstart, _selectlength);
                    // show cursor
                    show = showLength;
                    // And stop selection
                    SelectStart = -1;
                    SelectEnd = -1;
                }
                else // if not selecting
                {
                    // go back a character
                    cursorpos--;
                    // and remove the text
                    Text = Text.Remove(cursorpos, 1);
                    // also force the cursor to be visible
                    show = showLength;
                }
            }
            // If delete is pressed and the cursor is less than the text length (unless selecting)
            if (Keyboard.CheckTriggered(Keys.Delete) && (cursorpos < Text.Length | SelectStart != -1))
            {
                // when selecting
                if (SelectStart != -1)
                {
                    // behave the same as when pressing backspace
                    cursorpos = _selectstart;
                    Text = Text.Remove(_selectstart, _selectlength);
                    show = showLength;
                    SelectStart = -1;
                    SelectEnd = -1;
                }
                else // when not selecting
                {
                    // remove the character after cursor
                    Text = Text.Remove(cursorpos, 1);
                    // force the cursor to be visible
                    show = showLength;
                }
            }
            if (Keyboard.CheckTriggered(Keys.Home))
            {
                // Go to begin of text
                cursorpos = 0;
                show = showLength;
                // also stop selecting
                SelectStart = -1;
                SelectEnd = -1;
            }
            if (Keyboard.CheckTriggered(Keys.End))
            {
                // Go to end of text
                cursorpos = Text.Length;
                show = showLength;
                // also stop selecting
                SelectStart = -1;
                SelectEnd = -1;
            }
        }

        private void MoveCursorToCharacterByPosition(Point point)
        {
            cursorpos = GetCharacterByPosition(point);
        }

        private int GetCharacterByPosition(Point point)
        {
            // if in front of text
            if (point.X < Position.X + 3)
            {
                return 0;
            }
            // Chech what the last character is that the point fitted into
            int ret = 0;
            for (int i = 1; i < textSize.Count; i++)
            {
                Rectangle rect = new Rectangle((Position + new Vector2(textSize[i].X, -100)).ToPoint(), (Position + textSize[i] + new Vector2(0, 100)).ToPoint());
                if (rect.Contains(point))
                {
                    ret = i;
                }
            }
            if (ret > 0)
            {
                return ret;
            }
            // if nothing is found, it's probably too far behind text, or too low, or too high, just send the position of the end of the text.
            return Text.Length;
        }

        /// <summary>
        /// Updates the known size of text per length
        /// </summary>
        private void UpdateTextSize()
        {
            // Important!
            if (Password)
            {
                // Show password characters
                drawtext = new string((char)PasswordChar, Text.Length);
            }
            else
            {
                // Show normal text
                drawtext = Text;
            }

            textSize.Clear();
            textSize.Add(new Vector2(0, Font.MeasureString("A").Y));
            for (int i = 1; i <= drawtext.Length; i++)
            {
                textSize.Add(Font.MeasureString(drawtext.Substring(0, i)));
            }
        }
    }
}