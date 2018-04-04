using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrangerCade.Framework
{
    class View
    {
        private SpriteBatch _spriteBatch;
        public Vector2 Location;
        public Vector2 Scale;
        public GameObject FollowObject;
        public Viewport Viewport;
        private GraphicsDevice graphics;

        public Rectangle TranslatedViewport { get { return new Rectangle((Location).ToPoint(), ((Viewport.Bounds.Size.ToVector2() * Scale)).ToPoint()); } }

        public Color DrawColor = Color.Black;
        public RotationType RotationMode = RotationType.Degrees;

        private Texture2D whitePixel;


        public View()
        {
            Location = Vector2.Zero;
            Scale = Vector2.One;
            FollowObject = null;
        }

        public void Initialize(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            _spriteBatch = spriteBatch;
            whitePixel = new Texture2D(graphicsDevice, 1, 1);
            whitePixel.SetData(new[] { Color.White });
            Viewport = graphicsDevice.Viewport;
            graphics = graphicsDevice;
        }

        public void Update()
        {

        }

        public void DrawSprite(Sprite sprite, int subimg, Vector2 position, Vector2? scale = null, float rotation = 0f, Vector2? origin = null, float depth = 0f, Color? color = null, SpriteEffects spriteEffect = SpriteEffects.None)
        { 
            Color _color = color ?? Color.White;
            Vector2 _scale = scale ?? Vector2.One;
            _scale *= Scale;
            if (RotationMode == RotationType.Degrees)
            {
                rotation = Deg2Rad(rotation);
            }
            Vector2 _origin = origin ?? Vector2.Zero;
            subimg = Math.Min(Math.Max(subimg, 0), sprite.SubImages.Count - 1);
            _spriteBatch.Draw(sprite.Texture, (position * Scale) - Location, sprite.SubImages[subimg], _color, rotation, _origin, _scale, spriteEffect, depth);
        }

        public void DrawSpriteStretched(Sprite sprite, int subimg, Vector2 position, Vector2 size, float rotation = 0f, Vector2? origin = null, float depth = 0f, Color? color = null, SpriteEffects spriteEffect = SpriteEffects.None)
        {
            Color _color = color ?? Color.White;
            Vector2 _scale = size / sprite.Size;
            _scale *= Scale;
            if (RotationMode == RotationType.Degrees)
            {
                rotation = Deg2Rad(rotation);
            }
            Vector2 _origin = origin ?? Vector2.Zero;
            subimg = Math.Min(Math.Max(subimg, 0), sprite.SubImages.Count - 1);
            _spriteBatch.Draw(sprite.Texture, (position * Scale) - Location, sprite.SubImages[subimg], _color, rotation, _origin, _scale, spriteEffect, depth);
        }

        public void DrawTexture(Texture2D texture, Vector2 position, Vector2? scale = null, float rotation = 0f, Vector2? origin = null, float depth = 0f, Color? color = null, SpriteEffects spriteEffect = SpriteEffects.None)
        {
            Color _color = color ?? Color.White;
            Vector2 _scale = scale ?? Vector2.One;
            _scale *= Scale;
            if (RotationMode == RotationType.Degrees)
            {
                rotation = Deg2Rad(rotation);
            }
            Vector2 _origin = origin ?? Vector2.Zero;
            _spriteBatch.Draw(texture, (position * Scale) - Location, texture.Bounds, _color, rotation, _origin, _scale, spriteEffect, depth);
        }

        public void DrawRenderTarget(RenderTarget2D renderTarget, Vector2 position, Vector2? scale = null, float rotation = 0f, Vector2? origin = null, float depth = 0f, Color? color = null, SpriteEffects spriteEffect = SpriteEffects.None)
        {
            Color _color = color ?? Color.White;
            Vector2 _scale = scale ?? Vector2.One;
            _scale *= Scale;
            if (RotationMode == RotationType.Degrees)
            {
                rotation = Deg2Rad(rotation);
            }
            Vector2 _origin = origin ?? Vector2.Zero;
            _spriteBatch.Draw(renderTarget, (position * Scale) - Location, renderTarget.Bounds, _color, rotation, _origin, _scale, spriteEffect, depth);
        }

        public void DrawTextureStretched(Texture2D texture, Vector2 position, Vector2 size, float rotation = 0f, Vector2? origin = null, float depth = 0f, Color? color = null, SpriteEffects spriteEffect = SpriteEffects.None)
        {
            Color _color = color ?? Color.White;
            Vector2 _scale = size / texture.Bounds.Size.ToVector2();
            _scale *= Scale;
            if (RotationMode == RotationType.Degrees)
            {
                rotation = Deg2Rad(rotation);
            }
            Vector2 _origin = origin ?? Vector2.Zero;
            _spriteBatch.Draw(texture, (position * Scale) - Location, texture.Bounds, _color, rotation, _origin, _scale, spriteEffect, depth);
        }

        public void DrawText(SpriteFont font, object text, Vector2 position, Color? color = null, float rotation = 0f, Vector2? origin = null, float depth = 0f, Vector2? scale = null, SpriteEffects spriteEffect = SpriteEffects.None)
        {
            Color _color = color ?? DrawColor;
            Vector2 _scale = scale ?? Vector2.One;
            _scale *= Scale;
            rotation += rotation;
            Vector2 _origin = origin ?? Vector2.Zero;
            if (RotationMode == RotationType.Degrees)
            {
                rotation = Deg2Rad(rotation);
            }
            string _text = Convert.ToString(text).Replace("\t", "    ");
            _spriteBatch.DrawString(font, _text, (position * Scale) - Location, _color, rotation, _origin, _scale, spriteEffect, depth);
        }

        public void DrawRectangle(Rectangle rectangle, bool outline = false, Color? color = null, float rotation = 0f, Vector2? origin = null, float depth = 0f)
        {
            Color _color = color ?? DrawColor;
            Vector2 _origin = origin ?? Vector2.Zero;
            if (outline)
            {
                DrawLine(new Vector2(rectangle.Left, rectangle.Top), new Vector2(rectangle.Right, rectangle.Top), 1, _color);
                DrawLine(new Vector2(rectangle.Left, rectangle.Top), new Vector2(rectangle.Left, rectangle.Bottom), 1, _color);
                DrawLine(new Vector2(rectangle.Left, rectangle.Bottom), new Vector2(rectangle.Right, rectangle.Bottom), 1, _color);
                DrawLine(new Vector2(rectangle.Right, rectangle.Top), new Vector2(rectangle.Right, rectangle.Bottom), 1, _color);
            }
            else
            {
                // Rescale
                int x = (int)((float)rectangle.Location.X * Scale.X - Location.X);
                int y = (int)((float)rectangle.Location.Y * Scale.Y - Location.Y);
                int width = (int)((float)rectangle.Width * Scale.X);
                int height = (int)((float)rectangle.Height * Scale.Y);

                _spriteBatch.Draw(whitePixel, new Rectangle(x, y, width, height), null, _color, rotation, _origin, SpriteEffects.None, depth);
            }
        }
        public void DrawRectangle(Vector2 position, Vector2 size, bool outline = false, Color? color = null, float rotation = 0f, Vector2? origin = null, float depth = 0f)
        {
            DrawRectangle(new Rectangle(position.ToPoint(), size.ToPoint()), outline, color, rotation, origin, depth);
        }

        public void DrawLine(Vector2 start, Vector2 end, int width = 1, Color? color = null, float depth = 0)
        {
            Color _color = color ?? DrawColor;

            start = (start * Scale) - Location;
            end = (end * Scale) - Location;

            Vector2 edge = end - start;
            float angle = (float)Math.Atan2(edge.Y, edge.X);
            
            _spriteBatch.Draw(whitePixel, new Rectangle((int)(start.X - Location.X), (int)(start.Y - Location.Y), (int)edge.Length(), width), null, _color, angle, new Vector2(0, 0.5f), SpriteEffects.None, depth);

        }

        public void DrawPoint(Vector2 point, Color? color, float depth = 0)
        {
            Color _color = color ?? DrawColor;
            _spriteBatch.Draw(whitePixel, point * Scale - Location, null, _color, 0, Vector2.Zero, Scale, SpriteEffects.None, depth);
        }

        public void DrawSetColor(Color color)
        {
            DrawColor = color;
        }

        public void DrawSetColor(byte r, byte g, byte b, byte a = 255)
        {
            DrawColor = new Color(r, g, b, a);
        }

        public enum RotationType
        {
            Radians, Degrees
        }

        private float Deg2Rad(float degrees)
        {
            return degrees * (float)Math.PI / 180.0f;
        }

        public void SwitchToRenderTarget(RenderTarget2D target, bool clear = false, Color? clearColor = null)
        {
            _spriteBatch.End();
            graphics.SetRenderTarget(target);
            if (clear == true)
            {
                if (clearColor != null)
                {
                    graphics.Clear((Color)clearColor);
                }
            }

            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);
        }
    }
}
