using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionaryBehavior_MachineLearningAi.Classes
{
    class Organism
    {
        public int ID { get; set; }

        public List<ObjectsEncountered> ObjectsSeen = new List<ObjectsEncountered>();

        public int Lives { get; set; } //successfully completing a level gives +10 lives. lives are lost in the population level simulation.
        public int[] PositionXY { get; set; } //x,y position. use [0] as x, and [1] as y.

        public string Direction()
        {
            return "hi";
        }


    }
}
