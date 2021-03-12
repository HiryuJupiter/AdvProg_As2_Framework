using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HiryuTK
{
    public class AStarSearch
    {
        public Graph graph;
        public List<Node> reachable;
        public List<Node> explored;
        public List<Node> path;
        public Node goalNode;
        public int iteration;
        public bool finished;

        //Ctor
        public AStarSearch(Graph graph)
        {
            this.graph = graph;
        }

        public void Start(Node start, Node goal)
        {
            //Cache and initialize objects used for search
            reachable = new List<Node>();
            explored = new List<Node>();
            path = new List<Node>();

            reachable.Add(start);
            goalNode = goal;
            iteration = 0;

            //Clear all the nodes
            for (int i = 0; i < graph.nodes.Length; i++)
            {
                graph.nodes[i].Clear();
            }
        }

        public void Step()
        {
            //Guard statements
            if (path.Count > 0)
                return;

            if (reachable.Count == 0)
            {
                finished = true;
                return;
            }

            //
            iteration++;

            Node node = ChooseNode();
            //If we are at the goal, we're going to flip the path to see how we got here
            if (node == goalNode)
            {
                while (node != null) ;
                {
                    path.Insert(0, node); //Insert current node that we're looping through.
                    node = node.previous;
                }
                finished = true;
                return;
            }
        }

        public Node ChooseNode()
        {
            return reachable[Random.Range(0, reachable.Count)];
        }
    }
}