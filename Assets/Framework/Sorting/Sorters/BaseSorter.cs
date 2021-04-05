using Sorting.Visualization;
using System.Collections;
using UnityEngine;

namespace Sorting.Sorter
{
    public abstract class BaseSorter : MonoBehaviour
    {
        protected Visualizer visualizer;
        protected Node[] nodes;

        #region MonoBehaviour 
        private void Start()
        {
            visualizer = Visualizer.Instance;
            nodes = visualizer.Nodes;
        }
        #endregion

        #region Public
        /// <summary>
        /// Run sorting algorithm
        /// </summary>
        public void RunSorter()
        {
            StartCoroutine(SortAscending());
        }
        #endregion

        /// <summary>
        /// Sorting coroutine
        /// </summary>
        /// <returns></returns>
        protected abstract IEnumerator SortAscending();

        /// <summary>
        /// Swap nodes at 2 indexes
        /// </summary>
        /// <param name="indexA"></param>
        /// <param name="indexB"></param>
        protected void SwapNodes(int indexA, int indexB)
        {
            Node temp = nodes[indexA];
            nodes[indexA] = nodes[indexB];
            nodes[indexB] = temp;
        }

        /// <summary>
        /// Highlight node to blue color
        /// </summary>
        /// <param name="_node"></param>
        /// <param name="isHighlighted"></param>
        protected void HighlightNodeBlue(int _node, bool isHighlighted)
        {
            visualizer.HighlightNodeBlue(_node, isHighlighted);
            //visualizer.SetNodes(nodes);
        }

        /// <summary>
        /// Highlight node to red color
        /// </summary>
        /// <param name="_node"></param>
        /// <param name="isHighlighted"></param>
        protected void HighlightNodeRed(int _node, bool isHighlighted)
        {
            visualizer.HighlightNodeRed(_node, isHighlighted);
            //visualizer.SetNodes(nodes);
        }

        /// <summary>
        /// Tell visualizer to updata visuals
        /// </summary>
        protected void UpdateNodes ()
        {
            visualizer.SetNodes(nodes);
        }

        /// <summary>
        /// Debug function for printing all node values
        /// </summary>
        /// <param name="low"></param>
        /// <param name="high"></param>
        protected void PrintNodes(int low, int high)
        {
            string log = "";
            for (int i = low; i <= high; i++)
            {
                log = log + "[" + i + "]  = " + nodes[i].Value + ", ";
            }
            Debug.Log(log);
        }
    }
}