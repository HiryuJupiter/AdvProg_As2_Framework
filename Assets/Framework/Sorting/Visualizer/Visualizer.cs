using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 Node factory is the class that does the actual spawning
Node generator gives the command to factory on when to start building and how many nodes to build.
 */

namespace Sorting.Visualization
{
    /// <summary>
    /// For visualizing nodes
    /// </summary>
    [RequireComponent(typeof(NodeFactory))]
    public class Visualizer : MonoBehaviour
    {
        //Public static reference
        public static Visualizer Instance;

        [Header("Visualizing sequence parameters")]
        [SerializeField, Range(2, 100)]
        int nodeCount = 10;

        //Reference
        NodeFactory factory;

        //Cache
        List<Node> perfectSequence;

        //Properties
        public List<Node> Nodes { get; private set; } //Current sequence of nodes

        #region MonoBehaviour
        void Awake()
        {
            //Reference
            factory = GetComponent<NodeFactory>();

            //Initialize
            Instance = this;
            //Nodes = new List<Node>(nodeCount);

            //Create a perfect sequence of nodes
            perfectSequence = new List<Node>(nodeCount);
            perfectSequence = factory.BuildSequence(nodeCount);

            Nodes = perfectSequence.GetRange(0, perfectSequence.Count);
        }
        #endregion

        /// <summary>
        /// Set nodes to the array that's passed in
        /// </summary>
        public void SetNodes(List<Node> sorted)
        {
            Nodes = sorted;
            for (int i = Nodes.Count - 1; i >= 0; i--)
                Nodes[i].transform.SetAsFirstSibling();
        }

        /// <summary>
        /// Highlight node to blue color
        /// </summary>
        public void HighlightNodeBlue(int index, bool isHighlighed)
        {
            Nodes[index].SetSelectedBlue(isHighlighed);
        }

        /// <summary>
        /// Highlight node to red color
        /// </summary>
        public void HighlightNodeRed(int index, bool isHighlighed)
        {
            Nodes[index].SetSelectedRed(isHighlighed);
        }

        /// <summary>
        /// Shuffle nodes function that begins the shuffling coroutine
        /// </summary>
        public void ShuffleNodes()
        {
            StartCoroutine(Shuffle());
        }

        /// <summary>
        /// Coroutine that randomly arrange the nodes
        /// </summary>
        /// <returns></returns>
        IEnumerator Shuffle()
        {
            //We shuffle by selecting a random node and then move it to the front.
            //Keep a list of indexes that have yet to be moved to the front.
            List<int> unshuffled = new List<int>();
            for (int i = 0; i < Nodes.Count; i++)
            {
                unshuffled.Add(i);
            }

            for (int i = 0; i < nodeCount; i++)
            {
                //Select a random node and move it to the front 
                int uIndex = Random.Range(0, unshuffled.Count);
                Nodes[i] = perfectSequence[unshuffled[uIndex]];
                Nodes[i].transform.SetSiblingIndex(i);

                unshuffled.RemoveAt(uIndex);
                yield return null;
            }
        }
    }
}