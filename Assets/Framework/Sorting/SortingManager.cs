using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sorting.Sorter;
using Sorting.Visualization;

namespace Sorting
{
    public class SortingManager : MonoBehaviour
    {
        protected Visualizer visualizer;
        protected List<Node> nodes;

        void Start()
        {
            visualizer = Visualizer.Instance;
            nodes = visualizer.Nodes;
        }

        /// <summary>
        /// Starts the bubble sort
        /// </summary>
        public void RunBubbleSort ()
        {
            StartCoroutine(BubbleSorter.Run<IComparable>(nodes,  () => UpdateNodes()));
        }

        /// <summary>
        /// Starts the quick sort
        /// </summary>
        public void RunQuickSort()
        {
            StartCoroutine(QuickSorter.Run<IComparable>(this, nodes, () => UpdateNodes()));
        }

        /// <summary>
        /// Starts the selection sort
        /// </summary>
        public void RunSelectionSort()
        {
            StartCoroutine(SelectionSort.Run<IComparable>(nodes, () => UpdateNodes()));
        }

        /// <summary>
        /// Tell visualizer to updata visuals
        /// </summary>
        protected void UpdateNodes()
        {
            visualizer.SetNodes(nodes);
        }
    }
}

/*
 
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
 */