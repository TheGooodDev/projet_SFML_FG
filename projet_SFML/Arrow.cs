using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace projet_SFML
{
    class Arrow : Projectile
    {
        private Animation animation;

        private float scale = 0.5f;




        public Arrow(Vector2f pos, Vector2f[] hitbox, int direction, float movement,EntityType entityType,FullAnimation animation)
        {
            //SPRITE VALUE
            this.sprite = new Sprite();
            this.pos = pos;
            this.direction = direction;
            sprite.Position = pos;
            sprite.Scale = new Vector2f(scale * direction, scale);


            this.entityType = entityType;


            createHitbox(hitbox);


            this.speed = 1f;

            this.movement = movement;

            this.animation = animation;

        }


        public override void Update(RenderWindow renderWindow, float dt, float gravity, float floor)
        {

            decreaseSpeed();

            Move();

            setHitbox(vel);
            renderWindow.Draw(hitbox);
           


            animation.Start(sprite, dt);

            if (movement < 10)
            {
                vel.Y += gravity;
            }
            outOfBound(renderWindow, floor);


            sprite.Position = pos;
          //  sprite.Origin = new Vector2f(sprite.GetLocalBounds().Width / 2, sprite.GetLocalBounds().Height / 2);
        }

        public void decreaseSpeed()
        {
            if (this.movement < 0)
            {
                this.movement = 0;
            }
            else
            {
                this.movement -= 0.1f;
            }
        }
    }
}
