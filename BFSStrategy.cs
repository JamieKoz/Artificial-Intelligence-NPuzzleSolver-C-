using System;
using System.Collections.Generic;

namespace NPuzzle
{
    public class BFSStrategy : SearchMethod
    {
        public BFSStrategy()
        {
            code = "BFS";
            longName = "Breadth-First Search";
            Frontier = new Frontier();
            Searched = new List<PuzzleState>();
        }

        protected override PuzzleState PopFrontier()
        {
            //remove an item from the fringe to be searched
            PuzzleState thisState = Frontier.Pop();
            //Add it to the list of searched states, so that it isn't searched again
            Searched.Add(thisState);

            return thisState;
        }

        public override Direction[] Solve(NPuzzle puzzle)
        {
            //This method uses the fringe as a queue.
            //-----------------------------------------

            //put the start state in the Fringe to get explored.
            AddToFrontier(puzzle.StartState);

            List<PuzzleState> newStates = new List<PuzzleState>();

            while (Frontier.Count > 0)
            {
                //get the next item off the fringe
                PuzzleState thisState = this.PopFrontier();

                //is it the goal item?
                if (thisState.Equals(puzzle.GoalState))
                {
                    //We have found a solution! return it!
                    return thisState.GetPathToState();
                }
                else
                {
                    //This isn't the goal, just explore the node
                    newStates = thisState.Explore();
                    Console.Clear();
                    Console.WriteLine(thisState.ToString());

                    for (int i = 0; i < newStates.Count; i++)
                    {
                        //add this state to the fringe, addToFringe() will take care of duplicates
                        AddToFrontier(newStates[i]);
                    }
                }
            }
            //No solution found and we've run out of nodes to search
            //return null.
            return null;
        }

        public override bool AddToFrontier(PuzzleState state)
        {
            //if this state has been found before,
            if (Searched.Contains(state) || Frontier.Contains(state))
            {
                //discard it
                return false;
            }
            else
            {
                //else put this item on the end of the queue;
                Frontier.Enqueue(state);
                return true;
            }
        }
    }
}