# MachineLearningAi_EvolutionaryBehavior_Simulation
Machine Learning AI. 

Planned Release 1:
AI will learn to differentiate objects placed in its "MAP" as good or bad as they are related to completing the task of finding "FOOD" (the exit condition). Simulation will be seeded by running 1 million times so that the AI can learn its environment before being observed.


Developer Notes:
"
In order to evolve value need millions of simulations running millions of times. Selection has to be by "reproduction" (that which reproduces more becomes more highly represented). 

Successfully finding food means reproduction successful and type is duplicated.

Every 10 minutes or 10 cycles a percentage of randomly selected simulators are "killed"/removed. Goes by population size. 

0.1% of population regularly.
After 1 million +1% for every 100k, but never more than 95% and never less than 1 simulation. 

After 100 rounds each simulation is matched up with another and faces off. 0-0 is tied. Anything positive is subtract the difference. 50 survive. Top 5 duplicate. 45 fresh new ones each cycle.

Now a step further back. How to inherently determine value. I should set up conditions and it figures out what is right.

Differentiator. 60 second rounds one move per second. For every failed round -1 to all items on map. For every successful round +the number of moves for every tile encountered x #of times encountered to total. Goal/food is an automatic +60.

Perception is 2 blocks horizontal and vertical. 

Moves every turn once. If cant resolve equal directions randomly select one from equal ones

Add blocks in perceptions value and compare with all directions to decide which direction to move. 

Include all permutations of blocks for 2 deep. For memory. : 

How to resolve differentiation with memory. 

Give permutations a memory too. -1 per round +number of times encountered for all successful rounds. When picking a direction +value if squares + permutation value.

Better performance will come with better wider memory of squares. If its possible to have it automatically add permutations then can expand perceptions 

Ie 3x2 up down 
