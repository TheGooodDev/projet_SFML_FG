using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace projet_SFML
{
    class hitboxtest : Projectile
    {


        private float scale = 0.5f;




        public hitboxtest(Vector2f pos, Vector2f[] hitbox, int direction, float movement, EntityType entityType, World world)
        {
            this.world = world;
            //SPRITE VALUE
            this.sprite = new Sprite();
            this.pos = pos;
            this.direction = direction;
            sprite.Position = pos;
            sprite.Scale = new Vector2f(scale * direction, scale);
            sprite.Texture = new Texture("sprites/Circle.png");
            sprite.TextureRect = new IntRect(0, 0, 50, 50);


            this.entityType = entityType;


            this.hitbox = new Hitbox(hitbox, sprite);

            this.speed = 1f;

            this.movement = movement;

        }

        public override void Update(RenderWindow renderWindow, float dt)
        {
            Move();

            this.hitbox.setHitbox();
            renderWindow.Draw(this.hitbox.hitbox);

            outOfBound(renderWindow, world.floor);


            sprite.Position = pos;
            sprite.Origin = new Vector2f(sprite.GetLocalBounds().Width / 2, sprite.GetLocalBounds().Height / 2);
        }
    }
}
