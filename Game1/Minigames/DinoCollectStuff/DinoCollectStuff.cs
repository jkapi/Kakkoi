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
using System.Timers;

namespace Game1.Minigames.DinoCollectStuff
{
    class DinoCollectStuff : Room
    {

        Texture2D background;
        Sprite DinoSprite;
  
        Vector2 position;
        Vector2 playersize;
        bool isHit;
        int hitticks;
        List<Rectangle> collision = new List<Rectangle>();
        List<Drugs> listDrugs = new List<Drugs>();
        List<RozeDino> listRozeDinos = new List<RozeDino>();
        //List<int> platformInGebruik = new List<int>();


        SpriteFont Arial;
        SpriteFont font;
        bool onground = true;
        int TotalScore;
        int jumpSpeed = 24;
        int gravity = 2;
        int maxgravity = 16;
        int yspeed = 0;
        int xspeed = 0;
        long tick = 0;

        
        Stopwatch stopwatch;
        
        Random random = new Random();


       
        public override void Initialize()
        {
            TotalScore = 0;
            isHit = false;
            hitticks = 0;
            Timer spawntimer = new Timer(2000);
            spawntimer.Start();
            spawntimer.Elapsed += tickEvent;
            font = Content.Load<SpriteFont>("Score");
            background = Content.Load<Texture2D>("minigame/dinozooi/background rehab");
            Arial = Content.Load<SpriteFont>("arial16");
            position = new Vector2(20, 20);
            playersize = new Vector2(32, 64);
            collision.Add(new Rectangle(0, 1050, 525, 30));    //onderplatform
            collision.Add(new Rectangle(1475 ,1050 ,600, 30));               // onderplatform
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
           // Drugssprite = new Sprite(Content.Load<Texture2D>("minigame/Dinozooi/Clown"));
   
            DinoSprite = new Sprite(Content.Load<Texture2D>("minigame/Dinozooi/Clown"));

            listRozeDinos.Add(new RozeDino(new Vector2(120, 800), 120, 410, -3));     // enemy
            listRozeDinos.Add(new RozeDino(new Vector2(0, 660), -20 , 270 , -4));
            listRozeDinos.Add(new RozeDino(new Vector2(1620, 560), 1600 , 1880, 4));
            listRozeDinos.Add(new RozeDino(new Vector2(1550, 810), 1530 , 1810 , -2 ));

            foreach (RozeDino RozeDino in listRozeDinos)
            {
                Objects.Add(RozeDino);
            }

            stopwatch = Stopwatch.StartNew();

            //for (int i = 0; i < collision.Count; i++)
            //{
            //    platformInGebruik.Add(0);
            //}

            //Objects.Add(new Drugs(new Vector2(850, 415-48), new Vector2(850+300, 415-48)));
        }
   
        public override void Update()
        {

            if (hitticks == 1 & isHit)
            {
                TotalScore -= 10;
            }

            if(!isHit)
            {
                hitticks = 0;
            }
            //foreach (RozeDino RozeDino in listRozeDinos)
            //{
            //    RozeDino.Bounds.Intersects(new Rectangle((int)position.X, (int)position.Y, (int)playersize.X, (int)playersize.Y));
            //}

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

            List<Drugs> verwijderUitLijst = new List<Drugs>();

            foreach (Drugs item in listDrugs)
            {
                if (item.timer < 1)
                {
                    verwijderUitLijst.Add(item);
                }
                if (item.Bounds.Intersects(new Rectangle((int)position.X, (int)position.Y, (int)playersize.X, (int)playersize.Y)))
                {
                    TotalScore += 100;
                    verwijderUitLijst.Add(item);
                }

                
            }

            for (int i = 0; i < verwijderUitLijst.Count; i++)
            {
                verwijderUitLijst[i].Activated = false;
                listDrugs.Remove(verwijderUitLijst[i]);
                Objects.Remove(verwijderUitLijst[i]);

            }

            
        }

        public override void Draw()
        {

            foreach (RozeDino RozeDino in listRozeDinos)
            {
                isHit = RozeDino.Bounds.Intersects(new Rectangle((int)position.X, (int)position.Y, (int)playersize.X, (int)playersize.Y));
                if (isHit)
                {
                    hitticks++;
                    break;
                }
            }


            GraphicsDevice.Clear(Color.CornflowerBlue);

            View.DrawTextureStretched(background, Vector2.Zero, new Vector2(1920, 1080));

            //score tovoegen
            View.DrawText(Arial,"TotalScore: " + TotalScore, new Vector2(25, 25), Color.Red);
            View.DrawText(Arial, "hit: " + isHit, new Vector2(25, 50), Color.Red);

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

        private void tickEvent(Object source, ElapsedEventArgs e)
        {
            if (listDrugs.Count < 4)
            {
                //bool platformHasNoCoin = true;
                //while (platformHasNoCoin)
                //{
                //int platformNum = random.Next(0, platformInGebruik.Count);
                int randomRec = random.Next(0, collision.Count);
                //    if (platformInGebruik[platformNum] == 0)
                //    {
                Drugs drug = new Drugs(new Vector2(collision[randomRec].X, collision[randomRec].Y - 48), new Vector2(collision[randomRec].X + collision[randomRec].Width, collision[randomRec].Y - 45));
                drug.PreInitialize(this);
                drug.Initialize();
                        listDrugs.Add(drug);
                        //new Vector2(collision[platformNum].X, collision[platformNum].Y)
                        //new Vector2(collision[platformNum].X+collision[platformNum].Width, collision[platformNum].Y)
                        //platformInGebruik[platformNum] = 1;
                        //platformHasNoCoin = false;
                        Objects.Add(drug);

                    //}

                //}
            }
        
        } 
      

    }
}
