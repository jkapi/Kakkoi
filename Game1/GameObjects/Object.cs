using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrangerCade.Framework.GameObjects
{
    class GameObject
    {
        public Sprite Sprite = null;
        protected int SpriteIndex { get { return spriteIndex; } set { spriteIndex = Math.Min(Math.Max(0, value), Sprite.SubImageByName.Count); } }
        private int spriteIndex = 0;
        protected int SpriteSpeed = 30;
        protected float SpriteRotation = 0;
        protected Vector2 SpriteScale = Vector2.One;
        protected Vector2 SpriteOrigin = Vector2.Zero;

        public Vector2 Position = new Vector2(0);
        protected Room Room;
        protected View View;
        protected GameTime GameTime;
        protected GMKeyboard Keyboard;
        protected GMMouse Mouse;
        public bool Activated = true;

        public GameObject(Vector2 position)
        {
            Position = position;
        }

        public void PreInitialize(Room rm)
        {
            Room = rm;
            View = rm.View;
            Keyboard = rm.Keyboard;
            Mouse = rm.Mouse;
        }

        public virtual void Initialize()
        {

        }

        public void Update(GameTime gameTime)
        {
            GameTime = gameTime;
            Update();
        }

        public virtual void Update()
        {

        }

        public void Draw(View view, GameTime gameTime)
        {
            this.GameTime = gameTime;
            Draw();
            if (Sprite != null)
            {
                view.DrawSprite(Sprite, spriteIndex, Position, SpriteScale, SpriteRotation, SpriteOrigin);
            }
            spriteIndex = (int)(SpriteSpeed * gameTime.TotalGameTime.TotalSeconds % (Sprite.SubImages.Count - 1));
        }

        public virtual void Draw()
        {

        }
    }
}
