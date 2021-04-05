using UnityEngine;
using Sorting.Visualization;
using System.Collections;

//Logic: check if the current number (key) is smaller than the previous one (predacessor)
//if it is, then go further back to check the next predacessor, while moving the previous predacessor 1 space back

namespace Sorting.Sorter
{
    /// <summary>
    /// Holds the logic for the sorting algorithm insertion sort
    /// </summary>
    public class InsertionSort : BaseSorter
    {
        /// <summary>
        /// Override the base sorting method with the current definition of sorting algorithm
        /// </summary>
        protected override IEnumerator SortAscending()
        {
            for (int i = 1; i < nodes.Length; i++)
            {
                Node currentNode = nodes[i];

                int leftMostIndex = i - 1; //Index of the left-most ancestor with a value greater than keyValue

                while (leftMostIndex >= 0 && nodes[leftMostIndex].Value > currentNode.Value)
                {
                    //If ancestor is of higher value than current, then make ancester move up in index...
                    nodes[leftMostIndex + 1] = nodes[leftMostIndex];

                    HighlightNodeRed(leftMostIndex, true);
                    HighlightNodeBlue(leftMostIndex + 1, true);
                    UpdateNodes();
                    yield return null;
                    HighlightNodeRed(leftMostIndex, false);
                    HighlightNodeBlue(leftMostIndex + 1, false);

                    //...and decrement the leftMostIndex
                    leftMostIndex--;
                }

                nodes[leftMostIndex + 1] = currentNode;

                //Simply visualization
                HighlightNodeRed(i, true);
                HighlightNodeBlue(leftMostIndex + 1, true);
                UpdateNodes();
                yield return null;
                HighlightNodeRed(i, false);
                HighlightNodeBlue(leftMostIndex + 1, false);
            }
        }
    }
}