using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StrangerCade.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Helpers
{
    class D3D
    {
        public RenderTarget2D RenderTarget;
        public Color ClearColor;

        public Vector3 CameraPosition;
        public Vector3 CameraLookAtVector;
        public Vector3 CameraUpVector;
        public float CameraAspectRatio;
        public float CameraFOV;
        public float CameraNearClipPlane;
        public float CameraFarClipPlane;

        public bool EnableLighting;

        private GraphicsDeviceManager g;
        private GraphicsDevice gd;
        private BasicEffect effect;

        public D3D(GraphicsDeviceManager graphics, RenderTarget2D target, Color? clearColor = null)
        {
            RenderTarget = target;
            g = graphics;
            gd = graphics.GraphicsDevice;
            ClearColor = clearColor ?? Color.Transparent;

            CameraPosition = new Vector3(target.Width / 2, target.Height / 2, -target.Height / 2);
            CameraLookAtVector = new Vector3(target.Width / 2, target.Height / 2, 0);
            CameraUpVector = Vector3.Down;

            CameraAspectRatio = (float)target.Width / (float)target.Height;
            CameraFOV = MathHelper.PiOver2;
            CameraNearClipPlane = 1;
            CameraFarClipPlane = target.Height + target.Width;

            EnableLighting = false;

        }

        public void Begin()
        {
            gd.SetRenderTarget(RenderTarget);
            gd.Clear(Color.Transparent);
            //gd.SamplerStates[0] = SamplerState.PointClamp;

            effect = new BasicEffect(gd)
            {
                View = Matrix.CreateLookAt(
                CameraPosition, CameraLookAtVector, CameraUpVector),
                Projection = Matrix.CreatePerspectiveFieldOfView(
                CameraFOV, CameraAspectRatio, CameraNearClipPlane, CameraFarClipPlane),
                TextureEnabled = true,
                LightingEnabled = EnableLighting
            };
        }

        public void End()
        {
            gd.SetRenderTarget(null);
        }

        public void DrawModel(Model model, Matrix world, Matrix view, Matrix projection)
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = world;
                    effect.View = view;
                    effect.Projection = projection;
                }

                mesh.Draw();
            }
        }

        public void DrawTexture(Texture2D texture, Matrix transformation)
        {
            DrawTexturePart(texture, Vector2.Zero, Vector2.One, texture.Bounds.Size.ToVector2(), transformation);
        }
        public void DrawTexture(Texture2D texture, Vector2 size, Matrix transformation)
        {
            DrawTexturePart(texture, Vector2.Zero, Vector2.One, size, transformation);
        }

        private void DrawTexturePart(Texture2D texture, Vector2 topleft, Vector2 bottomright, Vector2 size, Matrix transformation)
        {
            VertexPositionTexture[] verts = new VertexPositionTexture[6];
            verts[0].Position = new Vector3(0, 0, 0);
            verts[1].Position = new Vector3(0, size.Y, 0);
            verts[2].Position = new Vector3(size.X, 0, 0);

            verts[3].Position = verts[1].Position;
            verts[4].Position = new Vector3(size.X, size.Y, 0);
            verts[5].Position = verts[2].Position;


            verts[0].TextureCoordinate = topleft;
            verts[1].TextureCoordinate = new Vector2(topleft.X, bottomright.Y);
            verts[2].TextureCoordinate = new Vector2(bottomright.X, topleft.Y);

            verts[3].TextureCoordinate = verts[1].TextureCoordinate;
            verts[4].TextureCoordinate = bottomright;
            verts[5].TextureCoordinate = verts[2].TextureCoordinate;

            effect.World = transformation;
            effect.Texture = texture;

            foreach (var pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                gd.DrawUserPrimitives(
                            PrimitiveType.TriangleList,
                    verts,
                    0,
                    2);
            }
        }

        public void DrawSprite(Sprite sprite, int subimage, Matrix transformation)
        {
            Vector2 subLocation = sprite.SubImages[subimage].Location.ToVector2();
            Vector2 subSize = sprite.SubImages[subimage].Size.ToVector2();
            Vector2 totalSize = sprite.Texture.Bounds.Size.ToVector2();

            Vector2 topLeft = subLocation / totalSize;
            Vector2 bottomRight = (subLocation + subSize) / totalSize;
            DrawTexturePart(sprite.Texture, topLeft, bottomRight, sprite.Size, transformation);
        }

        public void DrawSprite(Sprite sprite, int subimage, Vector2 size, Matrix transformation)
        {
            Vector2 subLocation = sprite.SubImages[subimage].Location.ToVector2();
            Vector2 subSize = sprite.SubImages[subimage].Size.ToVector2();
            Vector2 totalSize = sprite.Texture.Bounds.Size.ToVector2();

            Vector2 topLeft = subLocation / totalSize;
            Vector2 bottomRight = (subLocation + subSize) / totalSize;
            DrawTexturePart(sprite.Texture, topLeft, bottomRight, size, transformation);
        }
    }
}