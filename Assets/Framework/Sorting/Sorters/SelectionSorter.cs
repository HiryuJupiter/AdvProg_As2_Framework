using Sorting.Visualization;
using System.Collections;
using UnityEngine;

namespace Sorting.Sorter
{
    /// <summary>
    /// Holds the logic for the sorting algorithm selection sort
    /// </summary>
    public class SelectionSorter : BaseSorter
    {
        //Logic: run through the entire sequence, find the smallest number and bring it to the front.
        //Then move onto the next smallest. 

        /// <summary>
        /// Override the base sorting method with the current definition of sorting algorithm
        /// </summary>
        protected override IEnumerator SortAscending()
        {
            int nodeCount = nodes.Length;

            for (int i = 0; i < nodeCount - 1; i++)
            {
                for (int j = i + 1; j < nodeCount; j++)
                {
                    //If the value in this node is smaller, then update smallest value
                    if (nodes[j].CompareTo(nodes[i]) < 0)
                    //if (nodes[j].Value < nodes[i].Value)
                    {
                        SwapNodes(i, j);

                        //Simply visualization, not part of algorithm
                        HighlightNodeBlue(i, true);
                        HighlightNodeBlue(j, true);
                        UpdateNodes();
                        yield return null;
                        HighlightNodeBlue(i, false);
                        HighlightNodeBlue(j, false);
                    }
                }
            }
        }
    }
}