using UnityEngine;
using Sorting.Visualization;
using System.Collections;

namespace Sorting.Sorter
{
    public class BinaryInsertionSort : BaseSorter
    {
        /// <summary>
        /// Override the base sorting method with the current definition of sorting algorithm
        /// </summary>
        protected override IEnumerator SortAscending()
        {
            for (int i = 1; i < nodes.Length; i++)
            {
                Node current = nodes[i];
                int left = 0;
                int right = i;

                //Find insertion location
                while (left < right)
                {
                    int middle = (left + right) / 2;
                    //If the current value is bigger than middle, then ...
                    //... the insertion point is on the right.
                    if (current.Value > nodes[middle].Value)
                        left = middle + 1;
                    else
                        right = middle;
                }

                //Shift the array to the right of the insertion point, this is faster
                System.Array.Copy(nodes, left, nodes, left + 1, i - left);
                nodes[left] = current;

                HighlightNodeRed(i, true);
                HighlightNodeBlue(left, true);
                UpdateNodes();
                HighlightNodeRed(i, false);
                HighlightNodeBlue(left, false);
                yield return null;
            }
        }
    }
}