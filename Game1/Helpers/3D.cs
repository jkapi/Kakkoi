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
        private bool UseDepthStencil;

        private DepthStencilState oldDepthStencil;

        public D3D(GraphicsDeviceManager graphics, RenderTarget2D target, Color? clearColor = null)
        {
            RenderTarget = target;
            g = graphics;
            gd = graphics.GraphicsDevice;
            ClearColor = clearColor ?? Color.Transparent;
            if (target == null)
            {
                CameraPosition = new Vector3(graphics.PreferredBackBufferWidth / 2f, graphics.PreferredBackBufferHeight / 2f, -graphics.PreferredBackBufferHeight / 2f);
                CameraLookAtVector = new Vector3(graphics.PreferredBackBufferWidth / 2f, graphics.PreferredBackBufferHeight / 2f, 0);
                CameraAspectRatio = (float)graphics.PreferredBackBufferWidth / (float)graphics.PreferredBackBufferHeight;
            }
            else
            {
                CameraPosition = new Vector3(target.Width / 2, target.Height / 2, -target.Height / 2);
                CameraLookAtVector = new Vector3(target.Width / 2, target.Height / 2, 0);
                CameraAspectRatio = (float)target.Width / (float)target.Height;
            }
            CameraUpVector = Vector3.Down;

            CameraFOV = MathHelper.PiOver2;
            CameraNearClipPlane = 0.0001f;
            CameraFarClipPlane = 10000f;

            EnableLighting = false;

            UseDepthStencil = true;
        }

        public void Begin()
        {
            gd.SetRenderTarget(RenderTarget);
            gd.Clear(ClearOptions.DepthBuffer | ClearOptions.Target, Color.Transparent, 1.0f, 0);
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
            oldDepthStencil = gd.DepthStencilState;
            if (UseDepthStencil)
            {
                var depthState = new DepthStencilState();
                depthState.DepthBufferEnable = true; /* Enable the depth buffer */
                depthState.DepthBufferWriteEnable = true; /* When drawing to the screen, write to the depth buffer */

                gd.DepthStencilState = depthState;
            }
        }

        public void End()
        {
            gd.DepthStencilState = oldDepthStencil;
            gd.SetRenderTarget(null);
        }

        public void DrawModel(Model model, Matrix translation)
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = translation;
                    effect.View = effect.View;
                    effect.Projection = effect.Projection;
                    effect.LightingEnabled = true;
                    effect.EnableDefaultLighting();
                }

                mesh.Draw();
            }
        }

        public void DrawTexture(Texture2D texture, Matrix translation)
        {
            DrawTexturePart(texture, Vector2.Zero, Vector2.One, texture.Bounds.Size.ToVector2(), translation);
        }
        public void DrawTexture(Texture2D texture, Vector2 size, Matrix translation)
        {
            DrawTexturePart(texture, Vector2.Zero, Vector2.One, size, translation);
        }

        private void DrawTexturePart(Texture2D texture, Vector2 topleft, Vector2 bottomright, Vector2 size, Matrix translation)
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

            effect.World = translation;
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

        public void DrawSprite(Sprite sprite, int subimage, Matrix translation)
        {
            Vector2 subLocation = sprite.SubImages[subimage].Location.ToVector2();
            Vector2 subSize = sprite.SubImages[subimage].Size.ToVector2();
            Vector2 totalSize = sprite.Texture.Bounds.Size.ToVector2();

            Vector2 topLeft = subLocation / totalSize;
            Vector2 bottomRight = (subLocation + subSize) / totalSize;
            DrawTexturePart(sprite.Texture, topLeft, bottomRight, sprite.Size, translation);
        }

        public void DrawSprite(Sprite sprite, int subimage, Vector2 size, Matrix translation)
        {
            Vector2 subLocation = sprite.SubImages[subimage].Location.ToVector2();
            Vector2 subSize = sprite.SubImages[subimage].Size.ToVector2();
            Vector2 totalSize = sprite.Texture.Bounds.Size.ToVector2();

            Vector2 topLeft = subLocation / totalSize;
            Vector2 bottomRight = (subLocation + subSize) / totalSize;
            DrawTexturePart(sprite.Texture, topLeft, bottomRight, size, translation);
        }
    }
}