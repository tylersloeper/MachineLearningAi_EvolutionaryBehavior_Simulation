using EvolutionaryBehavior_MachineLearningAi.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionaryBehavior_MachineLearningAi
{
    class Program
    {
        static void Main(string[] args)
        {
            int begincountdown = 10;
            int timeElapsed = 0;
            Organism organism = new Organism();
            organism.PositionXY = new int[2];
            organism.ObjectsSeen = new List<ObjectsEncountered>();

            bool victory = false;



            for (int i =0; i<10; i++)
            {
                Console.Clear();
                Console.WriteLine("This is a Machine Learning (AI that learns) simulation. I have modeled the ai after evolution, and learning from past experiences (see comments in code for more information). The Ai will begin knowing nothing and be forced to move in one direction every 1 second. After 60 seconds the map will be recycled and the ai will be punished for not finding 'food'. The goal of the Ai is to learn to navigate an environment to find food. Evolutionary Competition is in the next update. ");
                Console.WriteLine(); //newline
                Console.WriteLine("Begins in: " + begincountdown); //newline
                begincountdown = begincountdown - 1;
                System.Threading.Thread.Sleep(1000);
            }


            ///Legend:
            /// AI =       +
            ///Food =      x
            ///wall =      |
            ///Traversable space =  -
            ///

            ///
            ///<---------------------------------------------------seperator---------------------------------------------------->
            ///

            //generate blank map. Use this for processing
            String[,] Map_Initial = new String[14, 14];
            InstantiateMap(Map_Initial);

            //Drop Food Randomly
            Random randomFood = new Random();
            int[] FoodPosition = new int[2]; //[1] =x coordinate. [2] = y coordinate
            FoodPosition[0] = randomFood.Next(2, 12);
            FoodPosition[1] = randomFood.Next(2, 12);
            Map_Initial[FoodPosition[0], FoodPosition[1]] = "x";


            //Drop Organism Randomly
            Random randomOrg = new Random();
            int[] OrganismPosition = new int[2]; //[1] =x coordinate. [2] = y coordinate
            OrganismPosition[0] = randomOrg.Next(2, 12);
            OrganismPosition[1] = randomOrg.Next(2, 12);
            organism.PositionXY[0] = OrganismPosition[0];
            organism.PositionXY[1] = OrganismPosition[1];

            //no overlap
            bool nomatch = false;
            while(nomatch == false)
            {
                if(OrganismPosition[0]  != FoodPosition[0] && OrganismPosition[1] != FoodPosition[1])
                {
                    Map_Initial[OrganismPosition[0], OrganismPosition[1]] = "+";
                    nomatch = true;
                }
                else
                {
                    OrganismPosition[0] = randomOrg.Next(2, 12);
                    OrganismPosition[1] = randomOrg.Next(2, 12);
                }
            }

            

            ///
            ///<---------------------------------------------------seperator---------------------------------------------------->
            ///

            //Print Map. Use this for GUI
            DrawMap(Map_Initial);
            DrawMapOrganismView(Map_Initial, OrganismPosition);



            ///
            ///<---------------------------------------------------seperator---------------------------------------------------->
            ///
            //every 1 seconds
            bool active = true;
            while(active == true)
            {
                if(timeElapsed %60 != 0)
                {
                    Console.Clear();

                    string Direction = Think();
                    //OrganismPosition = MakeAMove(Map_Initial, OrganismPosition, Direction); //when function is completed
                    OrganismPosition = MakeAMove(Map_Initial, OrganismPosition, MoveRandomly());
                    Console.WriteLine("\n"); //additional \n

                    //debug
                    DrawMap(Map_Initial);
                    DrawMapOrganismView(Map_Initial, OrganismPosition);

                    System.Threading.Thread.Sleep(1200);
                    timeElapsed = timeElapsed + 1;

                    //put victory condition here
                    ///
                }
                else
                {
                    //regenerate the map after 60 second. Victory condition failed
                    {
                        //generate blank map. Use this for processing
                        Map_Initial = new String[14, 14];
                        InstantiateMap(Map_Initial);

                        //Drop Food Randomly
                        randomFood = new Random();
                        FoodPosition = new int[2]; //[1] =x coordinate. [2] = y coordinate
                        FoodPosition[0] = randomFood.Next(2, 12);
                        FoodPosition[1] = randomFood.Next(2, 12);
                        Map_Initial[FoodPosition[0], FoodPosition[1]] = "x";


                        //Drop Organism Randomly
                        randomOrg = new Random();
                        OrganismPosition = new int[2]; //[1] =x coordinate. [2] = y coordinate
                        OrganismPosition[0] = randomOrg.Next(2, 12);
                        OrganismPosition[1] = randomOrg.Next(2, 12);

                        //no overlap
                        nomatch = false;
                        while (nomatch == false)
                        {
                            if (OrganismPosition[0] != FoodPosition[0] && OrganismPosition[1] != FoodPosition[1])
                            {
                                Map_Initial[OrganismPosition[0], OrganismPosition[1]] = "+";
                                nomatch = true;
                            }
                            else
                            {
                                OrganismPosition[0] = randomOrg.Next(2, 12);
                                OrganismPosition[1] = randomOrg.Next(2, 12);
                            }
                        }
                        timeElapsed = timeElapsed + 1;
                    }

                    //weight all objects seen for failure
                    if(organism.ObjectsSeen != null)
                    {
                        foreach(var Object in organism.ObjectsSeen)
                            {
                            Object.ObjectValue = Object.ObjectValue - 1;
                            }
                    }

                }

            }







            ///
            ///<---------------------------------------------------seperator---------------------------------------------------->
            ///
            //END
            Console.ReadKey();


        }

        static void DrawMap(string[,] Map)
        {
            //Print Map. Use this for GUI
            for (int i = 0; i < 14; i++)
            {
                for (int j = 0; j < 14; j++)
                {
                    Console.Write("\t" + Map[i, j]);
                }
                Console.WriteLine();
            }
        }

        static string[,] InstantiateMap(string[,] Map)
        {
            //generate blank map. Use this for processing
            for (int i = 0; i < 14; i++)
            {
                for (int j = 0; j < 14; j++)
                {
                    Map[i, j] = "|";
                }
            }
            for (int i = 2; i < 12; i++)
            {
                for (int j = 2; j < 12; j++)
                {
                    Map[i, j] = "-";
                }
            }
            return Map;
        }

        static void DrawMapOrganismView(string[,] Map, int[] OrgPosition)
        {
            //description
            Console.WriteLine();
            Console.WriteLine("What the organism sees:");
            Console.WriteLine();

            //Print Map. Use this for GUI
            for (int i = OrgPosition[0] -2; i <= OrgPosition[0] + 2; i++)
            {
                for (int j = OrgPosition[1] - 2; j <= OrgPosition[1] + 2; j++)
                {
                    Console.Write("\t" + Map[i, j]);
                }
                Console.WriteLine();
            }
        }

        static string MoveRandomly()
        {
            Random randomDirection = new Random();
            ///1 = N
            ///2 = S
            ///3 = W
            ///4 = E
            ///
            int direction = randomDirection.Next(1, 5);

            if(direction == 1)
            {
                return "N";
            }
            if (direction == 2)
            {
                return "S";
            }
            if (direction == 3)
            {
                return "W";
            }
            if (direction == 4)
            {
                return "E";
            }

            return "failed";

        }

        static string Think()
        {
            //take the average value of all paths, and the path with the highest average is the path to take.

            ///1 = N
            ///2 = S
            ///3 = W
            ///4 = E
            ///
            int direction = 0;

            if (direction == 1)
            {
                return "N";
            }
            if (direction == 2)
            {
                return "S";
            }
            if (direction == 3)
            {
                return "W";
            }
            if (direction == 4)
            {
                return "E";
            }

            return "failed";

        }

        static int[] MakeAMove(string[,] Map, int[] OrgPosition, string Direction)
        {
            //do stuff and return updated position.
            int[] TempOrg = new int[2];
            int[] TempMap = new int[2];
            string ObjectEncountered;

            Console.Write("\n Moved: ");
            if (Direction == "N")
            {
                //debug
                Console.Write("\n<North>");

                if(Map[OrgPosition[0] + 1, OrgPosition[1]] != "|")
                {
                //perform swaps
                ObjectEncountered = Map[OrgPosition[0] + 1, OrgPosition[1]];
                Map[OrgPosition[0] + 1, OrgPosition[1]] = "+";
                Map[OrgPosition[0], OrgPosition[1]] = ObjectEncountered;
                OrgPosition[0] = OrgPosition[0] +1;

                //return updated org position.
                return OrgPosition;
                }



            }
            if (Direction == "S")
            {
                //debug
                Console.Write("\n<South>");

                if (Map[OrgPosition[0] - 1, OrgPosition[1]] != "|")
                {
                    //perform swaps
                    ObjectEncountered = Map[OrgPosition[0] - 1, OrgPosition[1]];
                    Map[OrgPosition[0] - 1, OrgPosition[1]] = "+";
                    Map[OrgPosition[0], OrgPosition[1]] = ObjectEncountered;
                    OrgPosition[0] = OrgPosition[0] -1;

                    //return updated org position.
                    return OrgPosition;
                }


            }
            if (Direction == "W")
            {

                //debug
                Console.Write("\n<West>");

                if (Map[OrgPosition[0], OrgPosition[1] - 1] != "|")
                {
                    //perform swaps
                    ObjectEncountered = Map[OrgPosition[0], OrgPosition[1] - 1];
                    Map[OrgPosition[0], OrgPosition[1] - 1] = "+";
                    Map[OrgPosition[0], OrgPosition[1]] = ObjectEncountered;
                    OrgPosition[1] = OrgPosition[1] - 1;

                    //return updated org position.
                    return OrgPosition;
                }


            }
            if (Direction == "E")
            {
                //debug
                Console.Write("\n<East>");

                if (Map[OrgPosition[0], OrgPosition[1] + 1] != "|")
                {
                    //perform swaps
                    ObjectEncountered = Map[OrgPosition[0], OrgPosition[1] + 1];
                    Map[OrgPosition[0], OrgPosition[1] + 1] = "+";
                    Map[OrgPosition[0], OrgPosition[1]] = ObjectEncountered;
                    OrgPosition[1] = OrgPosition[1] + 1;

                    //return updated org position.
                    return OrgPosition;
                }

            }

            Console.Write("<Path-Blocked>");
            return OrgPosition;

        }



    }
}
