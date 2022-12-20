using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace projet_SFML
{
    class Hitbox
    {
        public VertexArray hitbox;
        public Vector2f[] points;
        public Sprite sprite;
        public Vector2f[] polygon;

        public Hitbox(Vector2f[] allPoint, Sprite sprite)
        {
            hitbox = new VertexArray(PrimitiveType.LineStrip, (uint)allPoint.Length+1);
            points = allPoint;
            this.sprite = sprite;
            polygon = new Vector2f[allPoint.Length];
        }

        public void setHitbox()
        {
            Vector2f[] cpoints = (Vector2f[])points.Clone();
            for (uint i = 0; i < points.Length; i++)
            {
                cpoints[i].X = sprite.Position.X + (cpoints[i].X - sprite.GetLocalBounds().Width / 2) * sprite.Scale.X;
                cpoints[i].Y = sprite.Position.Y + (cpoints[i].Y - sprite.GetLocalBounds().Height / 2) * sprite.Scale.Y;
                hitbox[i] = new Vertex(cpoints[i], Color.Red);
                polygon[i] = cpoints[i];
            }

            hitbox[(uint)cpoints.Length] = new Vertex(cpoints[0], Color.Red);
        }

        static bool onSegment(Vector2f p, Vector2f q, Vector2f r)
        {
            if (q.X <= Math.Max(p.X, r.X) &&
                q.X >= Math.Min(p.X, r.X) &&
                q.Y <= Math.Max(p.Y, r.Y) &&
                q.Y >= Math.Min(p.Y, r.Y))
            {
                Console.WriteLine("test");
                return true;
            }
            return false;
        }

        // To find orientation of ordered triplet (p, q, r).
        // The function returns following values
        // 0 --> p, q and r are collinear
        // 1 --> Clockwise
        // 2 --> Counterclockwise
        static int orientation(Vector2f p, Vector2f q, Vector2f r)
        {
            float val = (q.Y - p.Y) * (r.X - q.X) -
                    (q.X - p.X) * (r.Y - q.Y);

            if (val == 0)
            {
                return 0; // collinear
            }
            return (val > 0) ? 1 : 2; // clock or counterclock wise
        }

        // The function that returns true if
        // line segment 'p1q1' and 'p2q2' intersect.
        static bool doIntersect(Vector2f p1, Vector2f q1,
                                Vector2f p2, Vector2f q2)
        {
            // Find the four orientations needed for
            // general and special cases
            int o1 = orientation(p1, q1, p2);
            int o2 = orientation(p1, q1, q2);
            int o3 = orientation(p2, q2, p1);
            int o4 = orientation(p2, q2, q1);

            // General case
            if (o1 != o2 && o3 != o4)
            {
                return true;
            }

            // Special Cases
            // p1, q1 and p2 are collinear and
            // p2 lies on segment p1q1
            if (o1 == 0 && onSegment(p1, p2, q1))
            {
                return true;
            }

            // p1, q1 and p2 are collinear and
            // q2 lies on segment p1q1
            if (o2 == 0 && onSegment(p1, q2, q1))
            {
                return true;
            }

            // p2, q2 and p1 are collinear and
            // p1 lies on segment p2q2
            if (o3 == 0 && onSegment(p2, p1, q2))
            {
                return true;
            }

            // p2, q2 and q1 are collinear and
            // q1 lies on segment p2q2
            if (o4 == 0 && onSegment(p2, q1, q2))
            {
                return true;
            }

            // Doesn't fall in any of the above cases
            return false;
        }

        // Returns true if the Vector2f p lies
        // inside the polygon[] with n vertices
        public bool isInside(int n, Vector2f p)
        {
            
            // There must be at least 3 vertices in polygon[]
            if (n < 3)
            {
                return false;
            }

            // Create a Vector2f for line segment from p to infinite
            Vector2f extreme = new Vector2f(1000, p.Y);

            // Count intersections of the above line
            // with sides of polygon
            int count = 0, i = 0;
            do
            {
                int next = (i + 1) % n;

                // Check if the line segment from 'p' to
                // 'extreme' intersects with the line
                // segment from 'polygon[i]' to 'polygon[next]'
                if (doIntersect(polygon[i],
                                polygon[next], p, extreme))
                {
                    // If the Vector2f 'p' is collinear with line
                    // segment 'i-next', then check if it lies
                    // on segment. If it lies, return true, otherwise false
                    if (orientation(polygon[i], p, polygon[next]) == 0)
                    {
                        return onSegment(polygon[i], p,
                                        polygon[next]);
                    }
                    count++;
                }
                i = next;
            } while (i != 0);

            // Return true if count is odd, false otherwise
            return (count % 2 == 1); // Same as (count%2 == 1)
        }
    }
}
