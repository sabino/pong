﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pong
{
    public static class TextureGenerator
    {
        public static Texture2D CreateTexture(GraphicsDevice graphicsDevice, int width, int height)
        {
            return CreateTexture(graphicsDevice, width, height, Color.White);
        }

        public static Texture2D CreateTexture(GraphicsDevice graphicsDevice, int width, int height, Color color)
        {
            color = Color.White;
            Color[] foregroundColors = new Color[width * height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    foregroundColors[x + y * width] = color;
                }
            }

            Texture2D texture = new Texture2D(graphicsDevice, width, height, false, SurfaceFormat.Color);
            texture.SetData(foregroundColors);
            return texture;
        }
    }
}
