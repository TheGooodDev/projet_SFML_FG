using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using projet_SFML;
using SFML;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace SFML.Net_Test
{
    class Program
    {
        static void Main()
        {
            // INITIALIZING WINDOW
            RenderWindow window = new RenderWindow(new Window.VideoMode(1920, 1080), "FGC Test", Styles.Close, new ContextSettings(32, 0, 0));
            window.SetFramerateLimit(60);
            window.Closed += RenderWindow_Closed;
            Sprite background = new Sprite();
            background.Texture = new Texture("sprites/background.png");
            background.Scale = new Vector2f(  (float)window.Size.X / background.Texture.Size.X, (float)window.Size.Y / background.Texture.Size.Y);

            // FPS COUNT LABEL
            Text fpsStats = new Text("FPS: ?", new Font("fonts/RachelBrown.ttf"));
            fpsStats.Scale = new Vector2f(0.35f, 0.35f);
            fpsStats.Position = new Vector2f(30, 30);

            //WORLD
            World world = new World(0.5f, window.Size.Y - 150);

            //FIGHTER
            world.addObject(new Fighter(new Vector2f(window.Size.X / 2, window.Size.Y - 150),
                new Vector2f[]{
                    new Vector2f(0,0),
                    new Vector2f(50,0),
                    new Vector2f(50,37),
                    new Vector2f(0,37)
                }, 200, EntityType.PLAYER, new Animation[] {
                new FlatAnimation("IDLE","sprites/hero_sheet.png", 4, 0, 0, 50, 37, 300f),
                new FlatAnimation("RUN","sprites/hero_sheet.png", 6, 0, 38, 50, 37, 300f),
                new FlatAnimation("CROUCH","sprites/hero_sheet.png", 4, 0, 38*2 -1, 50, 37, 300f),
                new FlatAnimation("JUMP","sprites/hero_sheet.png", 10, 0, 38*3 -2, 50, 37, 150f),
                new FlatAnimation("SLIDE","sprites/hero_sheet.png", 5, 0, 38*4 -3, 50, 37, 300f),
                new FlatAnimation("CLIMB","sprites/hero_sheet.png", 9, 0, 38*5 -4, 50, 37, 300f),
                new FullAnimation("ATTACK","sprites/hero_sheet.png", 7, 0, 38*6 -5, 50, 37, 150f),
                new FlatAnimation("IDLE2","sprites/hero_sheet.png", 4, 0, 38*7 -6, 50, 37, 300f)
            })) ;


            // BLACK WINDOW
            Color windowColor = new Color(0, 0, 0);

            // CLOCKS
            Clock fpsClock = new Clock();
            Clock deltaTimeClock = new Clock();

            Random rand = new Random();

           

            while (window.IsOpen)
            {
   
                window.DispatchEvents();
                if (world.gameObjectThread.Count < 2)
                {
                    //new IntRect(56, 112, 160, 48)
                    world.gameObjectThread.Add(new Arrow(new Vector2f(window.Size.X - 200, 500),
                        new Vector2f[]{
                            new Vector2f(56,112),
                            new Vector2f(219,112),
                            new Vector2f(219,160),
                            new Vector2f(56,160)
                        }, -1, 15f, EntityType.PROJECTILE,
                        new FullAnimation("arrow", "sprites/arrow.png", 3, 0, 0, 300, 262, 300f))); ;
                }
     
                float dt = deltaTimeClock.Restart().AsMilliseconds();
                window.Clear(windowColor);

                window.Draw(background);

                // FPS COUNT
                float framerate = 1.0f / fpsClock.Restart().AsSeconds();
                fpsStats.DisplayedString = fpsStats.DisplayedString = "FPS: " + Math.Round(framerate).ToString();


                for (int i = world.gameObjectThread.Count -1; i >= 0;i--)
                {
                    world.gameObjectThread[i].Update(window, dt, world.gravity,world.floor);
                                                     window.Draw(world.gameObjectThread[i].sprite);
                    if(world.gameObjectThread[i].entityType == EntityType.PLAYER)
                    {
                        for(int l = world.gameObjectThread.Count -1; l >= 0; l--)
                        {
                            if (world.gameObjectThread[l].entityType == EntityType.PROJECTILE)
                            {

                            if (world.gameObjectThread[i].isColliding(world.gameObjectThread[l]))
                            {
                                Console.WriteLine("Collide");
                                    world.removeObject(world.gameObjectThread[l]);
                            }
                            }
                        }
                    }
                    if (world.gameObjectThread[i].deleted)
                    {
                        world.removeObject(world.gameObjectThread[i]);
                    }

                }
                window.Draw(fpsStats);
                window.Display();
            }
        }

        private static void RenderWindow_Closed(object sender, EventArgs e)
        {
            RenderWindow window = (RenderWindow)sender;
            window.Close();
        }

    }
}