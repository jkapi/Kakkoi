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
using StrangerCade.Framework.Multiplayer;

namespace Game1.Rooms
{
    class MainMenu : Room
    {
        private Texture2D KakoiLogo;
        private Texture2D MenuSolo;
        private Texture2D MenuMulti;
        private Texture2D MenuSettings;
        private Texture2D MenuQuit;
        private SpriteFont OpenSans;
        private Vector2 mainmenubuttonpos = new Vector2(1100, 375);
        private float mainmenubuttonspacing = 80;

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

        private bool menuEnabled = true;

        private bool multiEnabled = false;

        private bool showMulti = false;
        private bool showSettings = false;
        private float settingsMenuWidth = 440;
        private Vector2 targetOffsetMain { get {
                if (showSettings && !showMulti)
                    return new Vector2(settingsMenuWidth / 2 + 20, 0);
                if (showMulti)
                    return new Vector2(showSettings ? settingsMenuWidth / 2 + 20 : 0, -800);
                else
                    return Vector2.Zero;
            } }
        private Vector2 offsetMain = Vector2.Zero;
        private Vector2 targetOffsetMenuSettings { get { return showSettings ? Vector2.Zero : new Vector2(-settingsMenuWidth, 0); } }
        private Vector2 offsetMenuSettings = Vector2.Zero;

        private RoomList roomList;
        private Vector2 targetOffsetMenuMulti {  get { return showMulti ? Vector2.Zero : new Vector2(0, 1920); } }

        public override void Initialize()
        {
            MovingBackground.Init(Content);
            KakoiLogo = Content.Load<Texture2D>("roomselect/menukakoilogo");
            MenuSolo = Content.Load<Texture2D>("roomselect/menusolo");
            MenuSettings = Content.Load<Texture2D>("roomselect/menusettings");
            MenuQuit = Content.Load<Texture2D>("roomselect/menuquit");
            OpenSans = Content.Load<SpriteFont>("opensans13");
            multiEnabled = SocketHandler.Connected;
            if (multiEnabled)
                MenuMulti = Content.Load<Texture2D>("roomselect/menuonline");
            else
                MenuMulti = Content.Load<Texture2D>("roomselect/menuonlinedisabled");
            offsetMenuSettings = new Vector2(-settingsMenuWidth, 0);
            roomList = new RoomList();
            Objects.Add(roomList);
        }

        public override void Update()
        {
            multiHovered = IsVector2InPolygon4(boundsMulti, Mouse.Position - offsetMain) && multiEnabled && menuEnabled;
            soloHovered = IsVector2InPolygon4(boundsSolo, Mouse.Position - offsetMain) && menuEnabled;
            settingsHovered = IsVector2InPolygon4(boundsSettings, Mouse.Position - offsetMain) && menuEnabled;
            quitHovered = IsVector2InPolygon4(boundsQuit, Mouse.Position - offsetMain) && menuEnabled;

            targetOffsetMulti = multiHovered ? targetOffset : 0;
            targetOffsetSolo = soloHovered ? targetOffset : 0;
            targetOffsetSettings = settingsHovered ? targetOffset : 0;
            targetOffsetQuit = quitHovered ? targetOffset : 0;

            if (offsetMulti != targetOffsetMulti) { offsetMulti += animationSpeed * (targetOffsetMulti - offsetMulti); }
            if (offsetSolo != targetOffsetSolo) { offsetSolo += animationSpeed * (targetOffsetSolo - offsetSolo); }
            if (offsetSettings != targetOffsetSettings) { offsetSettings += animationSpeed * (targetOffsetSettings - offsetSettings); }
            if (offsetQuit != targetOffsetQuit) { offsetQuit += animationSpeed * (targetOffsetQuit - offsetQuit); }
            if (offsetMain != targetOffsetMain)
            {
                offsetMain += animationSpeed * (targetOffsetMain - offsetMain);
                offsetMenuSettings += animationSpeed * (targetOffsetMenuSettings - offsetMenuSettings);
            }
            if (Mouse.CheckReleased(MouseButtons.Left))
            {
                if (multiHovered) {
                    showMulti = true;
                    showSettings = false;
                }
                if (soloHovered) { throw new NotImplementedException(); }
                if (settingsHovered) { showSettings = !showSettings; }
                if (quitHovered) { Game1.stopping = true; }
            }

            if (Keyboard.CheckReleased(Keys.Escape))
            {
                if (showMulti)
                {
                    showMulti = false;
                }
                else
                {
                    Game1.stopping = true;
                }
            }

            if (multiHovered | soloHovered | settingsHovered | quitHovered)
                Mouse.Cursor = MouseCursor.Hand;
            else
                Mouse.Cursor = Mouse.DefaultCursor;

            MultiplayerAnimationHandler();
        }

        public override void Draw()
        {
            MovingBackground.Draw(this);
            View.DrawTexture(MenuMulti, new Vector2(mainmenubuttonpos.X + offsetMulti, mainmenubuttonpos.Y) + offsetMain);
            View.DrawTexture(MenuSolo, new Vector2(mainmenubuttonpos.X + offsetSolo, mainmenubuttonpos.Y + mainmenubuttonspacing) + offsetMain);
            View.DrawTexture(MenuSettings, new Vector2(mainmenubuttonpos.X + offsetSettings, mainmenubuttonpos.Y + 2*mainmenubuttonspacing) + offsetMain);
            View.DrawTexture(MenuQuit, new Vector2(mainmenubuttonpos.X + offsetQuit, mainmenubuttonpos.Y + 3*mainmenubuttonspacing) + offsetMain);
            View.DrawTexture(KakoiLogo, new Vector2(800, 540) + offsetMain, null, 0, KakoiLogo.Bounds.Center.ToVector2());
            View.DrawRectangle(offsetMenuSettings, new Vector2(440, 1080), false, new Color(Color.Black, 0.3f));
            View.DrawText(OpenSans, "There are no settings yet.", new Vector2(220 - OpenSans.MeasureString("There are no settings yet.").X / 2, 20) + offsetMenuSettings, Color.White);
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

        private void MultiplayerAnimationHandler()
        {
            if (roomList.Position != targetOffsetMenuMulti) { roomList.Position += animationSpeed * (targetOffsetMenuMulti - roomList.Position); }
        }
    }
}
