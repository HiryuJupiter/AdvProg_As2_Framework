using Sorting.Visualization;
using System.Collections;

namespace Sorting.Sorter
{
    /// <summary>
    /// Holds the logic for the sorting algorithm merge sort
    /// </summary>
    public class MergeSort : BaseSorter
    {
        /// <summary>
        /// Override the base sorting method with the current definition of sorting algorithm
        /// </summary>
        protected override IEnumerator SortAscending()
        {
            int nodeCount = nodes.Length;
            StartCoroutine(Sort(0, nodeCount - 1));
            
            yield return null;
        }

        /// <summary>
        /// Recursively sort an array into 2 subarrays
        /// </summary>
        IEnumerator Sort(int left, int right)
        {
            if (left < right)
            {
                int middle = left + (right - left) / 2;

                yield return StartCoroutine(Sort(left, middle));
                yield return StartCoroutine(Sort(middle + 1, right));

                yield return StartCoroutine (Merge(left, middle, right));
            }
        }

        /// <summary>
        /// Merge subarrays back together
        /// </summary>
        IEnumerator Merge(int left, int middle, int right)
        {
            //Crete the 2 sub arrays that are to be merged
            int sub1Length = middle - left + 1;
            int sub2Length = right - middle;
            Node[] sub1 = new Node[sub1Length];
            Node[] sub2 = new Node[sub2Length];

            int i, j; //sub1 index and sub2 index.
            for (i = 0; i < sub1Length; i++)
            {
                sub1[i] = nodes[left + i];
            }
            for (j = 0; j < sub2Length; j++)
                sub2[j] = nodes[middle + 1 + j];

            i = 0; //sub1 index
            j = 0; //sub2 index

            int mergeIndex = left;
            while (i < sub1Length && j < sub2Length)
            {
                if (sub1[i].Value < sub2[j].Value)
                {
                    nodes[mergeIndex] = sub1[i];
                    i++;
                }
                else
                {
                    nodes[mergeIndex] = sub2[j];
                    j++;
                }

                HighlightNodeBlue(mergeIndex, true);
                UpdateNodes();
                yield return null;
                HighlightNodeBlue(mergeIndex, false);

                mergeIndex++;
            }

            //The while loop that does the comparison will teriminate when one 
            //of the subarray index reached the subarray length. But there will
            // be one remaining element in the other array, so now we copy that over 
            //into the output array.
            while (i < sub1Length)
            {
                nodes[mergeIndex] = sub1[i];
                i++;
                mergeIndex++;

                HighlightNodeBlue(mergeIndex - 1, true);
                UpdateNodes();
                yield return null;
                HighlightNodeBlue(mergeIndex - 1, false);
            }
            while (j < sub2Length)
            {
                nodes[mergeIndex] = sub2[j];
                j++;
                mergeIndex++;

                HighlightNodeBlue(mergeIndex - 1, true);
                UpdateNodes();
                yield return null;
                HighlightNodeBlue(mergeIndex - 1, false);
            }
        }
    }
}