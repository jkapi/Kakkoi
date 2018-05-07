using StrangerCade.Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StrangerCade.Framework.Multiplayer;

namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        /// <summary>
        /// 
        /// </summary>
        public static bool stopping = false;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        /// <summary>
        /// 
        /// </summary>
        public Game1()
        {
            Logger.WriteLine("Started Kakoi");
            Logger.WriteLine("Starting GraphicsDevice");
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferMultiSampling = true;
            graphics.GraphicsProfile = GraphicsProfile.HiDef;
            IsMouseVisible = false;
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
            IsMouseVisible = true;
            Window.Title = "Kakoi Build " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            Logger.WriteLine("Creating SpriteBatch");
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Logger.WriteLine("Loading room: DebugRoom");
#if DEBUG
            Room.LoadRoom(typeof(Rooms.DebugRoom), Content, graphics, spriteBatch);
#else
            Room.LoadRoom(typeof(Rooms.LoginMenu), Content, graphics, spriteBatch);
#endif
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            Room.TryReinitializeIfNecessary();
            var kb = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || kb.IsKeyDown(Keys.Escape))
            {
                Logger.WriteLine("Exiting");
                SocketHandler.Stop();
                Exit();
            }
#if DEBUG
            if (kb.IsKeyDown(Keys.OemTilde))
            {
                Logger.WriteLine("Going to Debug Room");
                Room.GotoRoom(typeof(Rooms.DebugRoom));
            }
#endif
            float dTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            // TODO: Add your update logic here

            Room.CurrentRoom.Update(gameTime);

            base.Update(gameTime);
        }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Room.CurrentRoom.DrawClearColor);

          

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.Default);

            Room.CurrentRoom.Draw(gameTime);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
