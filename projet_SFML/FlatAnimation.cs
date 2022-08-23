﻿using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace projet_SFML
{
    class FlatAnimation : Animation
    {

        public FlatAnimation(string name, string textureFileName, int nFrames, int x, int y, int width, int height, float holdTime)
        {
            this.name = name;
            this.holdTime = holdTime;
            this.nFrames = nFrames;
            frames = new IntRect[this.nFrames];
            texture = new Texture(textureFileName);
            texture.Smooth = false;
            for (int i = 0; i < this.nFrames; i++)
            {
                frames[i] = new IntRect(x + (i * width), y, width, height);
            }
        }
        public override void Update(float dt)
        {
            time += dt;
            while (time >= holdTime)
            {
                time -= holdTime;
                Advance();

            }
        }

        public override bool Start(Sprite sprite, float dt)
        {
            Update(dt);
            ApplyToSprite(sprite);
            return false;
        }
    }
}
