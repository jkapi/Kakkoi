using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrangerCade.Framework.UI
{
    class CheckBox : GameObject
    {
        bool Checked;
        bool Hovered = false;
        public CheckBox(Vector2 position, bool @checked = false) : base(position)
        {
            Checked = @checked;
        }

        public override void Update()
        {
            if (new Rectangle(Position.ToPoint(), new Point(20)).Contains(Mouse.Position))
            {
                if (Mouse.CheckReleased(MouseButtons.Left))
                    Checked = !Checked;
                Mouse.Cursor = MouseCursor.Hand;
                Hovered = true;
            }
            else
            {
                if (new Rectangle(Position.ToPoint(), new Point(20)).Contains(Mouse.LastPosition))
                {
                    Mouse.Cursor = Mouse.DefaultCursor;
                }
                Hovered = false;
            }
        }

        public override void Draw()
        {
            View.DrawRectangle(Position, new Vector2(20), false, Color.White);
            View.DrawRectangle(Position, new Vector2(20), true, Color.Black);
            Color crossColor = Hovered ? Checked ? Color.DarkGray : Color.LightGray : Checked ? Color.Blue : Color.White;
            View.DrawLine(Position + new Vector2(3), Position + new Vector2(17), 2, crossColor);
            View.DrawLine(Position + new Vector2(3, 17), Position + new Vector2(17, 3), 2, crossColor);
        }
    }
}
