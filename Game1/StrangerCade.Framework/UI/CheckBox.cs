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
    class CheckBox : GameObject
    {
        public Color ColorBackground = Color.White;
        public Color ColorBorder = Color.Black;
        public Color ColorTick = Color.DeepSkyBlue;
        public Color ColorTickHover = Color.SkyBlue;
        public Color ColorText = Color.Black;

        public bool Checked;
        public bool Hovered = false;

        public string Text;

        private SpriteFont font;

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
            font = Room.Content.Load<SpriteFont>("opensans13");
        }

        public override void Draw()
        {
            View.DrawRectangle(Position, new Vector2(20), false, ColorBackground);
            View.DrawRectangle(Position, new Vector2(20), true, ColorBorder);
            if (Checked)
            {
                View.DrawLine(Position + new Vector2(3), Position + new Vector2(17), 2, ColorTick);
                View.DrawLine(Position + new Vector2(3, 17), Position + new Vector2(17, 3), 2, ColorTick);
            }
            else if (Hovered)
            {
                View.DrawLine(Position + new Vector2(3), Position + new Vector2(17), 2, ColorTickHover);
                View.DrawLine(Position + new Vector2(3, 17), Position + new Vector2(17, 3), 2, ColorTickHover);
            }
            if (Text != "")
            {
                View.DrawText(font, Text, new Vector2(Position.X + 22, Position.Y - 3), ColorText);
            }
        }
    }
}
