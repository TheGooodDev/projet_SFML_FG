using System;
using System.Collections.Generic;
using System.Text;

namespace projet_SFML
{
    class World
    {
        public float gravity;
        public float floor;
        public List<GameObject> gameObjectThread;

        public World(float gravity, float floor)
        {
            this.gravity = gravity;
            this.floor = floor;
            gameObjectThread = new List<GameObject>();
        }

        public void addObject(GameObject item)
        {
            gameObjectThread.Add(item);
        }

        public void removeObject(GameObject item)
        {
            gameObjectThread.Remove(item);
        }

    }
}
