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
        public World world;

        public Vector2f pos;
        public Hitbox hitbox;

        public bool deleted = false;
        public float speed;
        public Sprite sprite;
        public EntityType entityType;
        public Vector2f vel = new Vector2f(0.0f, 0.0f);


        public bool isColliding(GameObject entity)
        {
            for(uint i = 0; i < entity.hitbox.hitbox.VertexCount;i++)
            {
                if(this.hitbox.isInside((int)entity.hitbox.hitbox.VertexCount-1, entity.hitbox.hitbox[i].Position))
                {
                    return true;
                }
                
            }
            return false;
        }





        public void Delete(){
            
        }



        abstract public void Update(RenderWindow renderWindow, float dt);

        abstract public void outOfBound(RenderWindow renderWindow, float floor);
    }
}
