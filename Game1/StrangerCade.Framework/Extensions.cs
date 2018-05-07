using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.StrangerCade.Framework
{
    static class Extensions
    {
        public static string WrapText(this SpriteFont font, string text, float width)
        {
            if (font.MeasureString(text).X < width)
                return text;

            string[] words = text.Split(' ');
            string output = "";
            float lineWidth = 0f;
            float spaceWidth = font.MeasureString(" ").X;
            for (int i = 0; i < words.Length; ++i)
            {
                Vector2 size = font.MeasureString(words[i]);
                if (lineWidth + size.X < width)
                {
                    lineWidth += size.X + spaceWidth;
                }
                else
                {
                    if (size.X >= width)
                    {
                        for (int j = 0; j < words[i].Length; j++)
                        {
                            if (font.MeasureString(words[i].Substring(0,j)).X > width)
                                words[i] = words[i].Insert(j - 1, "\n");
                        }
                        if (i > 0)
                            output += "\n";
                        lineWidth = font.MeasureString(words[i].Split('\n').Last()).X + spaceWidth;
                    }
                    else
                    {
                        output += "\n";
                        lineWidth = size.X + spaceWidth;
                    }
                }
                output += words[i];
                output += " ";
            }

            return output;
        }
    }
}
