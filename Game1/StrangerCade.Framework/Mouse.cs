using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrangerCade.Framework
{
    class GMMouse
    {
        private MouseState state;
        private MouseState oldState;
        public Vector2 Position { get { return state.Position.ToVector2(); } set { Mouse.SetPosition((int)value.X, (int)value.Y); } }
        public MouseCursor DefaultCursor = MouseCursor.Arrow;
        public int ScrollWheelValue { get; private set; }

        private List<Vector2> movementHistory = new List<Vector2>();
        private int movementHistoryLength = 10;
        public MouseCursor Cursor {
            set
            {
                if (value == null) { cursor = DefaultCursor; return; }
                cursor = value;
            }
            get => cursor;
        }
        private MouseCursor cursor;
        private MouseCursor lastCursor;

        public GMMouse()
        {
            state = Mouse.GetState();
            oldState = Mouse.GetState();
        }

        public void Update()
        {
            oldState = state;
            state = Mouse.GetState();

            movementHistory.Add(state.Position.ToVector2() - oldState.Position.ToVector2());

            if (movementHistory.Count > movementHistoryLength)
            {
                movementHistory.RemoveAt(0);
            }
            ScrollWheelValue = state.ScrollWheelValue;
        }

        /// <summary>
        /// Redraws the cursor.
        /// </summary>
        /// <remarks>Only does this when the cursor has changed to reduce lag.</remarks>
        public void Draw()
        {
            if (cursor != lastCursor)
            {
                Mouse.SetCursor(cursor);
                lastCursor = cursor;
            }
        }

        public bool Check(MouseButtons button)
        {
            return Check(button, state);
        }

        public bool CheckPressed(MouseButtons button)
        {
            return (Check(button, state) && !Check(button, oldState));
        }
        public bool CheckReleased(MouseButtons button)
        {
            return (!Check(button, state) && Check(button, oldState));
        }

        public bool CheckEdge(MouseButtons button)
        {
            return (Check(button, state) ^ Check(button, oldState));
        }

        private bool Check(MouseButtons button, MouseState state)
        {

            switch (button)
            {
                case MouseButtons.Left: return state.LeftButton == ButtonState.Pressed;
                case MouseButtons.Right: return state.RightButton == ButtonState.Pressed;
                case MouseButtons.Middle: return state.MiddleButton == ButtonState.Pressed;
                case MouseButtons.XButton1: return state.XButton1 == ButtonState.Pressed;
                case MouseButtons.XButton2: return state.XButton2 == ButtonState.Pressed;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Average movement vector in the last 10 frames
        /// </summary>
        /// <returns></returns>
        public Vector2 AverageMovement()
        {
            Vector2 ret = new Vector2(0);
            foreach (Vector2 position in movementHistory)
            {
                ret += position;
            }
            ret /= movementHistoryLength;
            return ret;
        }

        public void SetCursor(Texture2D texture, Point origin)
        {
            Cursor = MouseCursor.FromTexture2D(texture, origin.X, origin.Y);
        }
        public void SetCursor(Sprite sprite, int subimg, Point origin)
        {
            Color[] imageData = new Color[sprite.Texture.Width * sprite.Texture.Height];
            sprite.Texture.GetData<Color>(imageData);
            Texture2D texture = new Texture2D(sprite.Texture.GraphicsDevice, sprite.Width, sprite.Height);
            texture.SetData<Color>(GetImageData(imageData, sprite.Texture.Width, sprite.SubImages[subimg]));
            Cursor = MouseCursor.FromTexture2D(texture, origin.X, origin.Y);
        }

        private Color[] GetImageData(Color[] colorData, int width, Rectangle rectangle)
        {
            Color[] color = new Color[rectangle.Width * rectangle.Height];
            for (int x = 0; x < rectangle.Width; x++)
                for (int y = 0; y < rectangle.Height; y++)
                    color[x + y * rectangle.Width] = colorData[x + rectangle.X + (y + rectangle.Y) * width];
            return color;
        }
    }

    /// <summary>
    /// Mouse buttons
    /// </summary>
    /// <remarks>
    /// - XButton1: mouse page forward
    /// - XButton2: mouse page backward
    /// </remarks>
    enum MouseButtons
    {
        Left, Right, Middle, XButton1, XButton2
    }
}
