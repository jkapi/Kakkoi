using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StrangerCade.Framework;
using StrangerCade.Framework.Multiplayer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Minigames.DinoCollectStuff
{
    class DinoCollectStuff : Room
    {

        Texture2D background;
        Sprite DinoSprite;
       
        Vector2 position;
        Vector2 playersize;
        List<Rectangle> collision = new List<Rectangle>();
        List<Drugs> listDrugs = new List<Drugs>();
        List<int> platformInGebruik = new List<int>();

        bool onground = true;

        int jumpSpeed = 24;
        int gravity = 2;
        int maxgravity = 16;
        int yspeed = 0;
        int xspeed = 0;
        long tick = 0;
        SpriteFont Arial;


        Stopwatch stopwatch;

        Random random = new Random();
       
        public override void Initialize()
        {
            background = Content.Load<Texture2D>("minigame/dinozooi/background rehab");
            Arial = Content.Load<SpriteFont>("arial16");
            position = new Vector2(20, 20);
            playersize = new Vector2(32, 64);
            collision.Add(new Rectangle(0, 1050, 1920, 30));    //onderplatform
            collision.Add(new Rectangle(525, 970, 950, 450));   // groot blok onderin
            collision.Add(new Rectangle(140, 870, 300, 20));    //linker platform onderin   //dino
            collision.Add(new Rectangle(0, 730, 300, 20));      // meest linkse platform   // dino
            collision.Add(new Rectangle(1620, 630, 300, 20));   //meest rechtse platform  //dino
            collision.Add(new Rectangle(1550, 880, 300, 20));   //rechterblok onderin
            collision.Add(new Rectangle(1300, 755, 350, 20));   //  rechterblok midden
            collision.Add(new Rectangle(600, 820, 400, 20));   // onderste blok in het midden
            collision.Add(new Rectangle(850, 685, 370, 20));   // midden midden
            collision.Add(new Rectangle(700, 550, 200, 20));   // midden boven
            collision.Add(new Rectangle(850, 415, 300, 20));  // midden helemaal boven
   
            DinoSprite = new Sprite(Content.Load<Texture2D>("minigame/Dinozooi/Clown"));
       
            Objects.Add(new RozeDino(new Vector2(120, 800), 120, 410, -3));     // enemy
            Objects.Add(new RozeDino(new Vector2(0, 660), -20 , 270 , -4));
            Objects.Add(new RozeDino(new Vector2(1620, 560), 1600 , 1880, 4));
            Objects.Add(new RozeDino(new Vector2(1550, 810), 1530 , 1810 , -2 ));
            stopwatch = Stopwatch.StartNew();

            for (int i = 0; i < collision.Count; i++)
            {
                platformInGebruik.Add(0);
            }

            //Objects.Add(new Drugs(new Vector2(850, 415-48), new Vector2(850+300, 415-48)));
        }
   
        public override void Update()
        {
         /*  if (tick % 2 == 0)
            {
                SocketHandler.SendMessage(PacketTypes.MOUSE, position.X, position.Y, xspeed, yspeed);
            } */
            xspeed = 0;
            xspeed += Keyboard.Check(Keys.D) ? 4 : 0;
            xspeed -= Keyboard.Check(Keys.A) ? 4 : 0;

            if (Keyboard.Check(Keys.Space) && onground && stopwatch.ElapsedMilliseconds > 300)
            {
                yspeed = -jumpSpeed;
                stopwatch.Restart();
            }

            if (yspeed < maxgravity)
            {
                yspeed += gravity;
            }

            position.Y += yspeed;
            onground = false;
            bool canmove = true;
            foreach (Rectangle rect in collision)
            {

                if (rect.Contains(position + playersize) || 
                    rect.Contains(position + new Vector2(0,playersize.Y)))
                {
                    position.Y = rect.Y - playersize.Y;
                    onground = true;
                }
                if (xspeed > 0)
                { // Check collision right
                    if (rect.Contains(position.X + playersize.X + xspeed, position.Y + playersize.Y -2))
                    {
                        canmove = false;
                    }
                }

                if (xspeed < 0)
                {
                    if (rect.Contains(position.X + xspeed, position.Y + playersize.Y -2))
                    {
                        canmove = false;
                    }
                }
            }
            if (canmove)
            {
                position.X += xspeed;
            }
            if (position.X <0 )
            {
                position.X = 0;
            }
            if (position.X > 1920 - playersize.X)
            {
                position.X = 1920 - playersize.X;
            }
            tick++;

            if (listDrugs.Count < 4)
            {
                bool platformHasNoCoin = true;
                while (platformHasNoCoin)
                { 
                    int platformNum = random.Next(0, platformInGebruik.Count);
                    if (platformInGebruik[platformNum] == 0)
                    {
                        Drugs drug = new Drugs(new Vector2(collision[platformNum].X, collision[platformNum].Y - 45), new Vector2(collision[platformNum].X + collision[platformNum].Width, collision[platformNum].Y - 45));
                        listDrugs.Add(drug);
                        //new Vector2(collision[platformNum].X, collision[platformNum].Y)
                        //new Vector2(collision[platformNum].X+collision[platformNum].Width, collision[platformNum].Y)
                        platformInGebruik[platformNum] = 1;
                        platformHasNoCoin = false;
                        Objects.Add(drug);

                    }

                }
            }

        }

        public override void Draw()
        {

          
            GraphicsDevice.Clear(Color.CornflowerBlue);
            View.DrawTextureStretched(background, Vector2.Zero, new Vector2(1920, 1080));
          
            /* List<Player> players = SocketHandler.GetPlayers();
             foreach (Player player in players)
             {
                 View.DrawText(Arial, player.Name, new Vector2(player.MouseX, player.MouseY));
             }*/

            View.DrawSetColor(Color.Gray);
            foreach(Rectangle rect in collision)
            {
                View.DrawRectangle(rect);
            }
          
            View.DrawSprite(DinoSprite, 0, position, new Vector2(playersize.X/DinoSprite.Width,
                                                                 playersize.Y/DinoSprite.Height));
        }

      
    }
}
