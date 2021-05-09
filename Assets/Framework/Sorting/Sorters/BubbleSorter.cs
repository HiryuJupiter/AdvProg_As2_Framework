using Sorting;
using Sorting.Visualization;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Sorting.Sorter
{
    /// <summary>
    /// Holds the logic for the sorting algorithm bubblle sort
    /// </summary>
    public static class BubbleSorter
    {
        /// <summary>
        /// Override the base sorting method with the current definition of sorting algorithm
        /// </summary>
        public static IEnumerator Run<T>(List<Node> list, Action visualizationCallback) where T : IComparable
        {
            int nodeCount = list.Count;

            //-1 because we can't swap the final number out of the array.
            for (int i = 0; i < nodeCount - 1; i++)
            {
                for (int j = 0; j < nodeCount - 1; j++)
                {
                    //If the current number is bigger than the next, then swap them
                    //if (nodes[j].Value > nodes[j + 1].Value)
                    if (list[j].CompareTo(list[j + 1]) > 0)
                    {
                        SwapNodes(list, j, j + 1);
                        visualizationCallback?.Invoke();
                        yield return null;
                    }
                }
            }
        }



























        /// <summary>
        /// Swap nodes at 2 indexes
        /// </summary>
        /// <param name="indexA"></param>
        /// <param name="indexB"></param>
        static void SwapNodes<T>(List<T> list, int indexA, int indexB) where T : IComparable
        {
            T temp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = temp;
        }
    }
}