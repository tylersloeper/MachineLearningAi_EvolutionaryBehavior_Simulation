using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionaryBehavior_MachineLearningAi.Classes
{
    class ObjectsEncountered
    {
        //to dynamically add objects to the differentiator. Objects will be identified by string, and weighted by value.
        public string Object { get; set; } //ID
        public int ObjectValue { get; set; } //value, -<><> to <><> 
    }
}
