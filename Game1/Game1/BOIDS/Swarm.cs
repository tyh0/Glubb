using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public class Swarm
    {
        public List<Boid> Boids = new List<Boid>();

        public Swarm(int boundary)
        {
            for (int i = 0; i < 15; i++)
            {
                Boids.Add(new Boid((i > 12), boundary));
            }
        }

        public void MoveBoids()
        {
            foreach (Boid boid in Boids)
            {
                boid.Move(Boids);
            }
        }
    }
}
