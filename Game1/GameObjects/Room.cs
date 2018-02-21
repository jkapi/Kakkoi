using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace StrangerCade.Framework.GameObjects
{
    class Room
    {
        public ContentManager Content;
        public GraphicsDeviceManager Graphics;
        public GraphicsDevice GraphicsDevice;
        private SpriteBatch sb;
        public View View;
        public GMKeyboard Keyboard;
        public GMMouse Mouse;
        public Color DrawClearColor = Color.SkyBlue;
        public List<GameObject> Objects;
        public bool DeActivateOutsideWindow = false;
        public Texture2D Background;
        public GameTime GameTime;
        

        public void Initialize(ContentManager content, GraphicsDeviceManager graphics, SpriteBatch spriteBatch)
        {
            Content = content;
            Graphics = graphics;
            graphics.SynchronizeWithVerticalRetrace = false;
            GraphicsDevice = graphics.GraphicsDevice;
            sb = spriteBatch;
            Objects = new List<GameObject>();
            View = new View();
            View.Initialize(sb, GraphicsDevice);
            Keyboard = new GMKeyboard();
            Mouse = new GMMouse();
            Initialize();
            foreach (GameObject obj in Objects)
            {
                obj.PreInitialize(this);
                obj.Initialize();
            }
        }

        public virtual void Initialize()
        {

        }

        public void Update(GameTime gameTime)
        {
            GameTime = gameTime;

            PreUpdate();
            Keyboard.Update(gameTime);
            Mouse.Update();
            Mouse.Cursor = Mouse.DefaultCursor;

            int roomWidth = Graphics.PreferredBackBufferWidth;
            int roomHeight = Graphics.PreferredBackBufferHeight;


            Update();

            foreach (GameObject obj in Objects)
            {
                if (DeActivateOutsideWindow)
                {
                    if (obj.Position.X < -obj.Sprite.Width | obj.Position.Y < -obj.Sprite.Height | obj.Position.X >= roomWidth | obj.Position.Y >= roomHeight)
                    {
                        obj.Activated = false;
                    }
                }

                if (obj.Activated)
                {
                    obj.Update(gameTime);
                }
            }

            PostUpdate();
        }
        public virtual void PreUpdate()
        { }
        public virtual void Update()
        { }
        public virtual void PostUpdate()
        { }

        public void Draw(GameTime gameTime)
        {
            GameTime = gameTime;

            Mouse.Draw();
            Viewport oldViewport = GraphicsDevice.Viewport;
            GraphicsDevice.Viewport = View.Viewport;
            Draw();
            foreach (GameObject obj in Objects)
            {
                if (obj.Activated)
                {
                    obj.Draw(View, gameTime);
                }
            }
            PostDraw();
            GraphicsDevice.Viewport = oldViewport;
        }

        public virtual void Draw()
        { }

        public virtual void PostDraw()
        { }
    }
}
