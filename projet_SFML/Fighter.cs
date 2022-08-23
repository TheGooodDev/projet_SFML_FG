using System;                    
using System.Collections.Generic;
using System.Text;

using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace projet_SFML
{

    enum AnimationStateIndex
    {
        IDLE,RUN,CROUCH,JUMP,SLIDE,CLIMB,ATTACK,IDLE2
    }

    enum State
    {
        GROUND, JUMP,FALL
    }

    enum Stand
    {
        NORMAL, FIGHT
    }

    class Fighter : GameObject
    {

        public float health;
        private float gravity;
        public bool on_animation = false;

        private State fighterState = State.GROUND;
        private Stand fighterStand = Stand.NORMAL;

        private AnimationStateIndex animationstateindex = AnimationStateIndex.IDLE;
        private Animation[] animations = new Animation[Enum.GetNames(typeof(AnimationStateIndex)).Length];


        
        public float jumpHeight;

        public Fighter(Vector2f pos,Vector2f[] hitbox,int jumpHeight, EntityType entityType,Animation[] animations)
        {
            this.speed = 7f;
            this.entityType = entityType;

            this.pos = pos;
            sprite = new Sprite();
            sprite.Scale = new Vector2f(4f, 4f);
            sprite.Position = pos;
            this.jumpHeight = jumpHeight;

            createHitbox(hitbox);
            for (int i = 0; i < animations.Length; i++)
            {
                this.animations[i] = animations[i];
            }

        }

        private bool isFloor(float floor)
        {
            if(pos.Y > floor)
            {
                fighterState = State.GROUND;
                pos.Y = floor;
                vel.Y = 0;
                return true;
            }else
            {
                return false;
            }
        }

        public override void outOfBound(RenderWindow renderWindow, float floor)
        {
            
            if(pos.Y < 0 + sprite.TextureRect.Height / 2)
            {
                pos.Y = 0 + sprite.TextureRect.Height / 2;
            }
            if(pos.X < 0 + sprite.TextureRect.Width / 2)
            {
                pos.X = 0 + sprite.TextureRect.Width / 2;
            }
            if(pos.X > renderWindow.Size.X - sprite.TextureRect.Width / 2)
            {
                pos.X = renderWindow.Size.X - sprite.TextureRect.Width/2 ;
            }
            else if (pos.Y > floor)
            {
                fighterState = State.GROUND;
                pos.Y = floor;
                vel.Y = 0;
            }

        }

        private void checkFallCondition(RenderWindow renderWindow)
        {
            if(pos.Y < 0 + renderWindow.Size.X/4)
            {
                fighterState = State.FALL;
            }
        }

        public override void Update(RenderWindow renderWindow, float dt, float gravity,float floor)
        {
            this.gravity = gravity;
            if (!on_animation)
            {
                HandleInput();
            }
            Animate(dt);

            isFloor(floor);
            handleGravity();

            pos += vel * speed;
            setHitbox(pos);

            outOfBound(renderWindow,floor) ;
            checkFallCondition(renderWindow);



            renderWindow.Draw(hitbox);
            // SET ORIGIN TO CENTER
            sprite.Position = pos;

        }

        public void Animate(float dt)
        {
            on_animation = animations[(int)animationstateindex].Start(sprite, dt); 
        }

        public void handleGravity()
        {
            if ((fighterState == State.FALL || fighterState == State.JUMP) && !on_animation)
            {
                animationstateindex = AnimationStateIndex.JUMP;
                vel.Y += gravity;
            }
        }

        public void HandleInput()
        {
            bool action = false;

            MovementInput(action);
            if (vel.Y > 0)
            {
                fighterState = State.FALL;
            }

            if (vel == new Vector2f(0.0f, 0.0f) && !action)
            {
                setIdlePhase();
            }
            AttackInput(action);


        }

        private bool MovementInput(bool action)
        {
            
            if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
            {
                if (fighterState != State.FALL)
                {
                    Jump();
                }
                action = true;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.LControl))
            {
                Crouch();
                action = true;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Tab))
            {
                if (fighterStand == Stand.NORMAL)
                {
                    setStand(Stand.FIGHT);
                }
                else
                {
                    setStand(Stand.NORMAL);
                }
                action = true;
            }



            if (Keyboard.IsKeyPressed(Keyboard.Key.Q) || Keyboard.IsKeyPressed(Keyboard.Key.Left))
            {
                Move(-1);
                action = true;
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.D) || Keyboard.IsKeyPressed(Keyboard.Key.Right))
            {
                Move(+1);
                action = true;
            }
            else
            {
                vel.X = 0;
            }

            return action;
        }

        private bool AttackInput(bool action)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.X))
            {
                stopFighter();
                Attack();
                action = true;
            }
            return action;
        }

        private void Attack()
        {
            
            setStand(Stand.FIGHT);
            animationstateindex = AnimationStateIndex.ATTACK;
        }

        private void Jump()
        {
            fighterState = State.JUMP;
            animationstateindex = AnimationStateIndex.JUMP;

            vel.Y = -6.1f;
   
        }

        private void Crouch()
        {
            animationstateindex = AnimationStateIndex.CROUCH;
            
        }

        private void Move(int direction)
        {
            if(fighterState == State.GROUND)
            {
            animationstateindex = AnimationStateIndex.RUN;
            }
            if(direction < 0)
            {
                sprite.Scale = new Vector2f(-4f, 4f);
            }
            else
            {
                sprite.Scale = new Vector2f(4f, 4f);
            }
            vel.X += direction;
            if(vel.X > 3.0f)
            {
                vel.X = 3.0f;
            }else if(vel.X < -3.0f)
            {
                vel.X = -3.0f;
            } 
        }

        private void setStand(Stand stand)
        { 

            fighterStand = stand;
            
        }

        private void setIdlePhase()
        {
            if (!on_animation)
            {

            switch (fighterStand)
            {
                case Stand.NORMAL:
                    animationstateindex = AnimationStateIndex.IDLE;
                    break;
                case Stand.FIGHT:
                    animationstateindex = AnimationStateIndex.IDLE2;
                    break;

            }
            }
        }

        private void stopFighter()
        {
            vel = new Vector2f(0.0f, 0.0f);
        }


    }
}
