using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace projet_SFML
{
    enum EntityType
    {
        PLAYER, PROJECTILE
    }

    abstract class GameObject
    {
        public Vector2f pos;
        public VertexArray hitbox;
        public bool deleted = false;
        public float speed;
        public Sprite sprite;
        public EntityType entityType;
        public Vector2f vel = new Vector2f(0.0f, 0.0f);


        public bool isColliding(GameObject entity)
        {
            return false;
        }

        public void createHitbox(Vector2f[] allPoint)
        {
            hitbox = new VertexArray(PrimitiveType.LineStrip, (uint)allPoint.Length);
            for (uint i = 0; i < allPoint.Length; i++)
            {
                allPoint[i].X = sprite.Position.X + allPoint[i].X * sprite.Scale.X;
                allPoint[i].Y = sprite.Position.Y + allPoint[i].Y * sprite.Scale.Y;
                hitbox[i] = new Vertex(allPoint[i],Color.Red);
            }
            hitbox.Append(new Vertex(allPoint[0], Color.Red));

        }

        public void setHitbox(Vector2f pos)
        {
            for (uint i = 0; i < hitbox.VertexCount; i++)
            {
                Vertex v = this.hitbox[i];
                v.Position.X += vel.X * speed;
                v.Position.Y += vel.Y * speed;
                this.hitbox[i] = v;
            }

        }

        public void Delete(){
            
        }

        abstract public void Update(RenderWindow renderWindow, float dt, float gravity, float floor);

        abstract public void outOfBound(RenderWindow renderWindow, float floor);
    }
}
