using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace StrangerCade.Framework
{
    class Sprite
    {
        public Dictionary<string, Rectangle> SubImageByName = new Dictionary<string, Rectangle>();
        public List<Rectangle> SubImages = new List<Rectangle>();
        public Texture2D Texture;

        public Vector2 Origin = Vector2.Zero;
        public Vector2 Center { get { return (SubImages[0].Center - SubImages[0].Location).ToVector2(); } }

        public Vector2 Size { get { return SubImages[0].Size.ToVector2(); } }

        public int Width { get { return SubImages[0].Width; } }
        public int Height { get { return SubImages[0].Height; } }

        /// <summary>
        /// Create sprite from texture
        /// </summary>
        /// <param name="texture"></param>
        ///   /// <summary>
        /// 
        /// </summary>
        ///   /// <summary>
        /// 
        /// </summary>
        public Sprite(Texture2D texture, bool searchSpritesheet = true)
        {
  
            Texture = texture;
            if (searchSpritesheet && File.Exists("Content/" + texture.Name + ".xml"))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load("Content/" + texture.Name + ".xml");
                XmlElement root = doc.DocumentElement;
                if (root.Name == "TextureAtlas")
                {
                    foreach (XmlNode childNode in root.ChildNodes)
                    {
                        if (childNode.Name == "SubTexture")
                        {
                            int x = Convert.ToInt32(childNode.Attributes["x"].Value);
                            int y = Convert.ToInt32(childNode.Attributes["y"].Value);
                            int width = Convert.ToInt32(childNode.Attributes["width"].Value);
                            int height = Convert.ToInt32(childNode.Attributes["height"].Value);
                            string name = childNode.Attributes["name"].Value;
                            name = name.Split('.')[0];
                            Rectangle bounds = new Rectangle(x, y, width, height);
                            SubImageByName.Add(name, bounds);
                            SubImages.Add(bounds);
                        }
                    }
                    if (SubImageByName.Count > 1)
                    {
                        return;
                    }
                }
            }
            SubImageByName.Add("0", texture.Bounds);
            SubImages.Add(texture.Bounds);
        }

        /// <summary>
        /// Create sprite from strip.
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="subImages">Amount of subimages in strip</param>
        public Sprite(Texture2D texture, int subImages)
        {
            Texture = texture;
            int subwidth = texture.Width / subImages;
            for (int i = 0; i < subImages; i++)
            {
                Rectangle bounds = new Rectangle(new Point(subwidth * i, 0), new Point(subwidth, texture.Height));
                SubImageByName.Add(i.ToString(), bounds);
                SubImages.Add(bounds);
            }
        }

        /// <summary>
        /// Creates sprite from spritemap.
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="size"></param>
        public Sprite(Texture2D texture, Point size)
        {
            Texture = texture;
            int horizontal = texture.Width / size.X;
            int vertical = texture.Height / size.Y;
            for (int y = 0; y < vertical; y++)
            {
                for (int x = 0; x < horizontal; x++)
                {
                    Rectangle bounds = new Rectangle(new Point(size.X * x, size.Y * y), new Point(size.X, size.Y));
                    SubImageByName.Add((y * x + x).ToString(), bounds);
                    SubImages.Add(bounds);
                }
            }
        }
        public Sprite(Texture2D texture, Point size, int count)
        {
            Texture = texture;
            int horizontal = texture.Width / size.X;
            int vertical = texture.Height / size.Y;
            for (int y = 0; y < vertical; y++)
            {
                for (int x = 0; x < horizontal; x++)
                {
                    Rectangle bounds = new Rectangle(new Point(size.X * x, size.Y * y), new Point(size.X, size.Y));
                    SubImages.Add(bounds);

                    if (y * horizontal + x == count)
                    {
                        return;
                    }
                }
            }
        }


        /// <summary>
        /// Creates sprite from part of spritemap.
        /// </summary>
        public Sprite(Texture2D texture, Rectangle part, Point size)
        {
            Texture = texture;
            AddFromMap(part, size);
        }

        public void AddFromMap(Rectangle part, Point size)
        {
            for (int y = 0; y < part.Height / size.Y; y++)
            {
                for (int x = 0; x < part.Width / size.X; x++)
                {
                    Rectangle bounds = new Rectangle(new Point(part.X + size.X * x, part.Y + size.Y * y), new Point(size.X, size.Y));
                    SubImageByName.Add(SubImageByName.Count.ToString(), bounds);
                    SubImages.Add(bounds);
                }
            }
        }
    }
}
