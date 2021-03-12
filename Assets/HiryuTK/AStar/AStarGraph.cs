using UnityEngine;
using System.Collections;

namespace HiryuTK
{

    public class Graph
    {
        public int rows = 0;
        public int cols = 0;
        public Node[] nodes;

        //The graph class link the nodes together
        public Graph(int[,] grid)
        {
            //Create a 2D graph of Nodes based on the size of grid we want
            rows = grid.GetLength(0);
            cols = grid.GetLength(1);

            nodes = new Node[grid.Length]; //Gets the total length of the 2d array.
            for (int i = 0; i < nodes.Length; i++)
            {
                Node node = new Node();
                node.label = i.ToString();
                nodes[i] = node;
            }

            //Build out node association
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    //Get the node in our flat array.
                    Node node = nodes[cols * r + c];

                    if (grid[r, c] == 1) //If grid condition == wall or solid tile
                    {
                        continue;
                    }

                    //Add neighbors                
                    if (r > 0) //Up
                        node.adjecent.Add(nodes[cols * (r - 1) + c]);
                    if (r < rows - 1) //Down
                        node.adjecent.Add(nodes[cols * (r + 1) + c]);
                    if (c > 0) //Left
                        node.adjecent.Add(nodes[cols * r + c - 1]);
                    if (c < cols - 1) //Right
                        node.adjecent.Add(nodes[cols * r + c + 1]);


                }
            }
        }
    }

}
