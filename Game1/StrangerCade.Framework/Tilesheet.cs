using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace StrangerCade.Framework
{
    class Tilesheet
    {
        ushort[,] tiledata;
        bool flood;
        View view;
        Sprite tilemap;
        int tileHeight;
        int tileWidth;
        Vector2 scale;
        int height;
        int width;
        public long RenderedTiles;
        public Tilesheet(string filename, Sprite tilemap, View view)
        {
            this.tilemap = tilemap;
            Debug.WriteLine(tilemap.SubImageByName.Count);
            this.view = view;
            tileHeight = tilemap.Height;
            tileWidth = tilemap.Width;
            using (BinaryReader s = new BinaryReader(File.Open("Content/" + filename + ".tmd", FileMode.Open, FileAccess.Read)))
            {
                if (s.BaseStream.Length > 18)
                {
                    // Check header
                    if (s.ReadInt32() == 4476244) // V1
                    {
                        flood = s.ReadBoolean();
                        scale = new Vector2(s.ReadSingle(), s.ReadSingle());
                        width = s.ReadUInt16();
                        height = s.ReadUInt16();
                        if (s.BaseStream.Length == 17 + height * width * 2)
                        {
                            tiledata = new ushort[height, width];
                            for (int y = 0; y < height; y++)
                            {
                                for (int x = 0; x < width; x++)
                                {
                                    tiledata[y, x] = s.ReadUInt16();
                                }
                            }
                        }
                        else
                        {
                            throw new Exception("TMD File size is incorrect. Got " + s.BaseStream.Length + ", expected " + (width * height * 2 + 9));
                        }
                    }
                }
                else
                {
                    throw new Exception("TMD File too small!");
                }
            }
        }

        public void Draw()
        {
            RenderedTiles = 0;
            int minX = (int)(((view.Location.X) - tileWidth) / scale.X / view.Scale.X / tileWidth) - 1;
            int minY = (int)(((view.Location.Y) - tileHeight) / scale.Y / view.Scale.Y / tileHeight) - 1;
            int maxX = ((int)((view.Location.X + view.Viewport.Width) / scale.X / view.Scale.X) / tileWidth + 1);
            int maxY = ((int)((view.Location.Y + view.Viewport.Height) / scale.Y / view.Scale.Y) / tileHeight + 1);
            if (flood)
            {
                for (int y = minY; y < maxY; y++)
                {
                    for (int x = minX; x < maxX; x++)
                    {
                        view.DrawSprite(tilemap, tiledata[Modulo(y, height), Modulo(x, width)], new Vector2(x * tileWidth, y * tileHeight) * scale, scale);
                        RenderedTiles++;
                    }
                }
            }
            else
            {
                minX = Math.Max(0, minX);
                minY = Math.Max(0, minY);
                maxX = Math.Min(width, maxX);
                maxY = Math.Min(height, maxY);
                for (int y = minY; y < maxY; y++)
                {
                    for (int x = minX; x < maxX; x++)
                    {
                        view.DrawSprite(tilemap, tiledata[y, x], new Vector2(x * tileWidth, y * tileHeight) * scale, scale);
                        RenderedTiles++;
                    }
                }
            }
        }

        /// <summary>
        /// Real modulo, because % gives remainder in c#, thus it can give the wrong answer.
        /// </summary>
        int Modulo(int x, int m)
        {
            return (x % m + m) % m;
        }
    }
}
