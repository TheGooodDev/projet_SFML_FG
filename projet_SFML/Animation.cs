using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace projet_SFML
{
        abstract class Animation 
        {
            public Texture texture { get; set; }
        public string name { get; set; }
        protected int nFrames { get; set; }
        protected IntRect[] frames { get; set; }
            protected int iFrame { get; set; } = 0;
        protected float holdTime { get; set; }
        protected float time { get; set; }  = 0.0f;
        protected bool started = false;
        protected bool ended = false;


        public abstract bool Start(Sprite sprite, float dt);
            
            public void ApplyToSprite(Sprite sprite)
            {
                sprite.Texture = texture;
                sprite.TextureRect = frames[iFrame];
            }

            public abstract void Update(float dt);

            protected void Advance()
            {
                if (++iFrame >= nFrames)
                {
                    iFrame = 0;
                ended = true;
                started = false;
                }
            }
        }
    
}
