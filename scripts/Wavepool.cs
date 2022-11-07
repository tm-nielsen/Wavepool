﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Wavepool
{
    public class Wavepool
    {
        List<Ripple> ripples;

        WaveGrid waveGrid;

        SpriteBatch spriteBatch;

        float halfX;


        public Wavepool(Vector2 position, Vector2 size, int rows, int columns, float drawSize)
        {
            ripples = new List<Ripple>();

            halfX = size.X / 2;

            waveGrid = new WaveGrid(position, size, rows, columns, drawSize, GetOffset);
        }

        public void Load(Texture2D texture, GraphicsDevice graphicsDevice)
        {
            spriteBatch = new SpriteBatch(graphicsDevice);
            waveGrid.OnLoad(texture, spriteBatch);
        }

        public void Update(float deltaTime)
        {
            ripples = ripples.Where(ripple => ripple.alive).ToList();

            foreach(Ripple ripple in ripples)
            {
                ripple.Update(deltaTime);
            }
        }

        public void Draw()
        {
            spriteBatch.Begin();

            waveGrid.DrawGrid();

            spriteBatch.End();
        }

        public void AddRipple(Ripple ripple) => ripples.Add(ripple);

        Vector2 GetOffset(Vector2 position)
        {
            Vector2 offset = Vector2.Zero;

            foreach (Ripple ripple in ripples)
            {
                offset += ripple.GetOffset(position);
            }

            return offset;
        }
    }
}