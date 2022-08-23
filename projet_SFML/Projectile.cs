using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace projet_SFML
{
    abstract class Projectile : GameObject
    {
        public int direction;
        public float movement;

        public void Move()
        {
            vel.X = movement * direction;
            pos += vel * speed;


        }

        public override void outOfBound(RenderWindow renderWindow, float floor)
        {
            if (pos.Y < 0  || pos.X < 0  || pos.X > renderWindow.Size.X  || pos.Y > floor )
            {
                this.deleted = true;
            }

        }
    }
}
