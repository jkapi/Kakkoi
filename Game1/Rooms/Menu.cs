﻿using StrangerCade.Framework;
using Game1.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Game1.Rooms
{
    class Menu : Room
    {
        private Texture2D KakoiLogo;
        private Texture2D MenuSolo;
        private Texture2D MenuMulti;
        private Texture2D MenuSettings;
        private Texture2D MenuQuit;
        private SpriteFont OpenSans;
        private Vector2 itempos = new Vector2(1100, 375);
        private float itemspacing = 80;

        // 1190    375    1200    435  |  1200     455    1190    515  |  1190    535    1170    595  |  1165    615    1135    675
        // 1535    375    1500    435  |  1485     455    1450    515  |  1440    535    1400    595  |  1390    615    1355    675
        private Vector2[] boundsMulti = new Vector2[] { new Vector2(1190, 375), new Vector2(1200, 435), new Vector2(1485, 435), new Vector2(1535, 375) };
        private Vector2[] boundsSolo = new Vector2[] { new Vector2(1200, 455), new Vector2(1190, 515), new Vector2(1450, 515), new Vector2(1485, 455) };
        private Vector2[] boundsSettings = new Vector2[] { new Vector2(1190, 535), new Vector2(1170, 595), new Vector2(1400, 595), new Vector2(1440, 535) };
        private Vector2[] boundsQuit = new Vector2[] { new Vector2(1165, 615), new Vector2(1135, 675), new Vector2(1355, 675), new Vector2(1390, 615) };

        private float targetOffsetMulti = 0;
        private float offsetMulti = 0;
        private float targetOffsetSolo = 0;
        private float offsetSolo = 0;
        private float targetOffsetSettings = 0;
        private float offsetSettings = 0;
        private float targetOffsetQuit = 0;
        private float offsetQuit = 0;

        private bool multiHovered = false;
        private bool soloHovered = false;
        private bool settingsHovered = false;
        private bool quitHovered = false;

        private float animationSpeed = 0.22222222F;
        private float targetOffset = 30;

        public override void Initialize()
        {
            MovingBackground.Init(Content);
            KakoiLogo = Content.Load<Texture2D>("roomselect/menukakoilogo");
            MenuSolo = Content.Load<Texture2D>("roomselect/menusolo");
            MenuMulti = Content.Load<Texture2D>("roomselect/menuonline");
            MenuSettings = Content.Load<Texture2D>("roomselect/menusettings");
            MenuQuit = Content.Load<Texture2D>("roomselect/menuquit");
            OpenSans = Content.Load<SpriteFont>("opensans13");
        }

        public override void Update()
        {
            multiHovered = IsVector2InPolygon4(boundsMulti, Mouse.Position);
            soloHovered = IsVector2InPolygon4(boundsSolo, Mouse.Position);
            settingsHovered = IsVector2InPolygon4(boundsSettings, Mouse.Position);
            quitHovered = IsVector2InPolygon4(boundsQuit, Mouse.Position);

            targetOffsetMulti = multiHovered ? targetOffset : 0;
            targetOffsetSolo = soloHovered ? targetOffset : 0;
            targetOffsetSettings = settingsHovered ? targetOffset : 0;
            targetOffsetQuit = quitHovered ? targetOffset : 0;

            if (offsetMulti != targetOffsetMulti) { offsetMulti += animationSpeed * (targetOffsetMulti - offsetMulti); }
            if (offsetSolo != targetOffsetSolo) { offsetSolo += animationSpeed * (targetOffsetSolo - offsetSolo); }
            if (offsetSettings != targetOffsetSettings) { offsetSettings += animationSpeed * (targetOffsetSettings - offsetSettings); }
            if (offsetQuit != targetOffsetQuit) { offsetQuit += animationSpeed * (targetOffsetQuit - offsetQuit); }

            if (Mouse.CheckReleased(MouseButtons.Left))
            {
                if (multiHovered) { GotoRoom(typeof(Rooms.RoomMenu)); }
                if (soloHovered) { throw new NotImplementedException(); }
                if (settingsHovered) { throw new NotImplementedException(); }
                if (quitHovered) { Game1.stopping = true; }
            }

            if (multiHovered | soloHovered | settingsHovered | quitHovered)
            {
                Mouse.Cursor = MouseCursor.Hand;
            }
            else
            {
                Mouse.Cursor = Mouse.DefaultCursor;
            }
        }

        public override void Draw()
        {
            MovingBackground.Draw(this);
            View.DrawTexture(MenuMulti, new Vector2(itempos.X + offsetMulti, itempos.Y));
            View.DrawTexture(MenuSolo, new Vector2(itempos.X + offsetSolo, itempos.Y + itemspacing));
            View.DrawTexture(MenuSettings, new Vector2(itempos.X + offsetSettings, itempos.Y + 2*itemspacing));
            View.DrawTexture(MenuQuit, new Vector2(itempos.X + offsetQuit, itempos.Y + 3*itemspacing));
            View.DrawTexture(KakoiLogo, new Vector2(800, 540), null, 0, KakoiLogo.Bounds.Center.ToVector2());
            View.DrawText(OpenSans, "Position: " + itempos +
                                    "\nSpacing: " + itemspacing +
                                    "\nMouse position: " + Mouse.Position +
                                    "\nAnimation distance: " + targetOffset + "@" + animationSpeed +
                                    "\nMulti: " + multiHovered + "|" + Math.Round(offsetMulti, 4) + "/" + targetOffsetMulti +
                                    "\nSolo: " + soloHovered + "|" + Math.Round(offsetSolo, 4) + "/" + targetOffsetSolo +
                                    "\nSettings: " + settingsHovered + "|" + Math.Round(offsetSettings, 4) + "/" + targetOffsetSettings +
                                    "\nQuit: " + quitHovered + "|" + Math.Round(offsetQuit, 4) + "/" + targetOffsetQuit, Vector2.One, Color.Lime);
        }

        // Copied from https://stackoverflow.com/questions/4243042/c-sharp-point-in-polygonpublic 
        private static bool IsVector2InPolygon4(Vector2[] polygon, Vector2 testPoint)
        {
            bool result = false;
            int j = polygon.Count() - 1;
            for (int i = 0; i < polygon.Count(); i++)
            {
                if (polygon[i].Y < testPoint.Y && polygon[j].Y >= testPoint.Y || polygon[j].Y < testPoint.Y && polygon[i].Y >= testPoint.Y)
                {
                    if (polygon[i].X + (testPoint.Y - polygon[i].Y) / (polygon[j].Y - polygon[i].Y) * (polygon[j].X - polygon[i].X) < testPoint.X)
                    {
                        result = !result;
                    }
                }
                j = i;
            }
            return result;
        }
    }
}
