using Sorting.Visualization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sorting.Sorter
{
    /// <summary>
    /// Holds the logic for the sorting algorithm selection sort
    /// </summary>
    public static class SelectionSort
    {
        //Logic: run through the entire sequence, find the smallest number and bring it to the front.
        //Then move onto the next smallest. 

        /// <summary>
        /// Override the base sorting method with the current definition of sorting algorithm
        /// </summary>
        public static IEnumerator Run<T>(this List<Node> list, Action visualizationCallback) where T : IComparable
        {
            int nodeCount = list.Count;

            for (int i = 0; i < nodeCount - 1; i++)
            {
                for (int j = i + 1; j < nodeCount; j++)
                {
                    //If the value in this node is smaller, then update smallest value
                    if (list[j].CompareTo(list[i]) < 0)
                    //if (nodes[j].Value < nodes[i].Value)
                    {
                        SwapNodes(list, i, j);

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