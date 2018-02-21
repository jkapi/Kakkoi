using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrangerCade.Framework.GameObjects
{
    class GMMouse
    {
        private MouseState state;
        private MouseState oldState;
        public Vector2 Position { get { return state.Position.ToVector2(); } set { Mouse.SetPosition((int)value.X, (int)value.Y); } }
        public MouseCursor DefaultCursor = MouseCursor.Arrow;

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
        }

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

        public Vector2 AverageMovement()
        {
            Vector2 ret = new Vector2(0);
            foreach (Vector2 position in movementHistory)
            {
                ret += position;
            }
            ret /= 10;
            return ret;
        }
    }

    enum MouseButtons
    {
        Left, Right, Middle, XButton1, XButton2
    }
}
