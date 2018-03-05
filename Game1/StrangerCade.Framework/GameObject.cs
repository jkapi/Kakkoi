using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrangerCade.Framework
{
    /// <summary>
    /// The GameObject class
    /// </summary>
    class GameObject
    {
        /// <summary>
        /// The <see cref="Sprite">Sprite</see> of the gameObject
        /// </summary>
        /// <remarks>
        /// Can be nulled
        /// </remarks>
        public Sprite Sprite = null;
        /// <summary>
        /// The index of the <see cref="Sprite">Sprite</see>.
        /// </summary>
        /// <remarks>Set to draw the Nth frame of the GameObject.Sprite</remarks>
        protected int SpriteIndex { get { return spriteIndex; } set { spriteIndex = Math.Min(Math.Max(0, value), Sprite.SubImageByName.Count); } }
        private int spriteIndex = 0;
        /// <summary>
        /// The amount of times the <see cref="SpriteIndex">SpriteIndex</see> has to be refreshed each second.
        /// </summary>
        protected int SpriteSpeed = 30;
        /// <summary>
        /// The rotation of the <see cref="Sprite">Sprite</see>
        /// </summary>
        protected float SpriteRotation = 0;
        /// <summary>
        /// The scale of the <see cref="Sprite">Sprite</see>
        /// </summary>
        protected Vector2 SpriteScale = Vector2.One;
        /// <summary>
        /// The draw origin of the <see cref="Sprite">Sprite</see>
        /// </summary>
        /// <remarks>
        /// The Vector2(x,y) position in the sprite that is used for the draw(x,y) function</remarks>
        protected Vector2 SpriteOrigin = Vector2.Zero;

        /// <summary>
        /// The position of this GameObject
        /// </summary>
        public Vector2 Position = new Vector2(0);
        /// <summary>
        /// Reference to the <see cref="Room">Room</see> that calls this GameObject
        /// </summary>
        protected Room Room;
        /// <summary>
        /// Reference to the <see cref="View">View</see> of the Room that calls this GameObject
        /// </summary>
        protected View View;
        /// <summary>
        /// Reference to the Game GameTime
        /// </summary>
        protected GameTime GameTime;
        /// <summary>
        /// Reference to the <see cref="GMKeyboard">Keyboard</see> handler of the room
        /// </summary>
        protected GMKeyboard Keyboard;
        /// <summary>
        /// Reference to the <see cref="GMMouse">Mouse</see> handler of the room
        /// </summary>
        protected GMMouse Mouse;
        /// <summary>
        /// If this GameObject is Activated. The calling is handled by Room.Update, so you can still call the Update and Draw functions.
        /// </summary>
        public bool Activated = true;

        /// <summary>
        /// Initializer with setting position of the GameObject.
        /// </summary>
        /// <remarks>
        /// This function has to be called from objects that inherit GameObject. This can be done with: <c>public ObjectName(Vector2 position) : base(position){}</c></remarks>
        /// <param name="position"></param>
        public GameObject(Vector2 position)
        {
            Position = position;
        }

        /// <summary>
        /// PreInitialization, sets the references to be used by Initialize, Update and Draw
        /// </summary>
        /// <param name="rm">Calling Room (this)</param>
        public void PreInitialize(Room rm)
        {
            Room = rm;
            View = rm.View;
            Keyboard = rm.Keyboard;
            Mouse = rm.Mouse;
        }

        /// <summary>
        /// Initialize handler, to be overwritten.
        /// </summary>
        public virtual void Initialize()
        {

        }

        /// <summary>
        /// Update handler to be called by the Room.
        /// </summary>
        /// <remarks>Currently does nothing more than updating GameTime and calling Update()</remarks>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            GameTime = gameTime;
            Update();
        }

        /// <summary>
        /// Update handler, to be overwritten. Called every V-Blank.
        /// </summary>
        /// <remarks>
        /// Make use of the GameTime. The FPS isn't always fixed.
        /// </remarks>
        public virtual void Update()
        {

        }


        /// <summary>
        /// Draw handler to be called from the Room.
        /// \nFirst calls Draw, after that it draws GameObject.Sprite
        /// </summary>
        /// <param name="view">The Raw View</param>
        /// <param name="gameTime">Updated GameTime Object</param>
        public void Draw(View view, GameTime gameTime)
        {
            this.GameTime = gameTime;
            Draw();
            if (Sprite != null)
            {
                view.DrawSprite(Sprite, spriteIndex, Position, SpriteScale, SpriteRotation, SpriteOrigin);
                spriteIndex = (int)(SpriteSpeed * gameTime.TotalGameTime.TotalSeconds % (Sprite.SubImages.Count - 1));
            }
        }


        /// <summary>
        /// Draw handler, to be overwritten. Called every V-Blank.
        /// </summary>
        /// <remarks>
        /// Make use of the GameTime. The FPS isn't always fixed.
        /// </remarks>
        public virtual void Draw()
        {

        }
    }
}
