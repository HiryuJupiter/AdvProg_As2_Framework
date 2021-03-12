using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HiryuTK
{
    //The node represents a space in a map and its spatial relationship to its neighbors.
    public class Node
    {
        public List<Node> adjecent = new List<Node>();
        public Node previous = null;
        public string label = "";

        public void Clear()
        {
            previous = null;
        }
    }
}