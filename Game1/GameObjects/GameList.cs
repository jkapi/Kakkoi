using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StrangerCade.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.GameObjects
{
    class GameList : GameObject
    {
        static public bool _loadingDone;
        static public Sprite FrontCovers;
        int selectedCover = 0;

        public GameList(Vector2 position) : base (position)
        {}

        public override void Initialize()
        {
            var loading = Task.Factory.StartNew(LoadContent);
        }

        public void LoadContent()
        {
            FrontCovers = new Sprite(Room.Content.Load<Texture2D>("RoomSelect/GameCovers"), 4);
            for (int i = 0; i < 10; i++)
            {
                FrontCovers.SubImages.Add(FrontCovers.SubImages[3]);
            }
            _loadingDone = true;
        }

        public override void Update()
        {

        }

        public override void PreDraw()
        {

        }

        public override void Draw()
        {
            if (_loadingDone)
            {
                for (int i = 0; i < FrontCovers.SubImages.Count; i++)
                {
                    View.DrawSprite(FrontCovers, i, new Vector2(100 * i, 0));
                }
            }
        }
    }
}
