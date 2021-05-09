using UnityEngine;
using Sorting.Visualization;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Sorting.Sorter
{
    /// <summary>
    /// Holds the logic for the sorting algorithm quick sort
    /// </summary>
    public static class QuickSorter
    {
        static MonoBehaviour mono;
        static List<Node> list;
        static Action visualizationCallback;

        /// <summary>
        /// Starts the sort
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_mono"></param>
        /// <param name="_list"></param>
        /// <param name="_visualizationCallback"></param>
        /// <returns></returns>
        public static IEnumerator Run<T>(MonoBehaviour _mono, List<Node> _list, Action _visualizationCallback) where T : IComparable
        {
            mono = _mono;
            list = _list;
            visualizationCallback = _visualizationCallback;

            yield return _mono.StartCoroutine(DoQuickSort(0, list.Count - 1));
        }

        /// <summary>
        /// Recursively partition the sequence into two groups, seperated by a pivot
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        static IEnumerator DoQuickSort(int left, int right)
        {
            if (left >= right) //This occurs upon reaching the end of recurrsion.
                yield break;

            int pivotIndex = 0;
            yield return mono.StartCoroutine(Partition(left, right, (finalPivotIndex) => pivotIndex = finalPivotIndex));
            yield return mono.StartCoroutine(DoQuickSort(left, pivotIndex - 1)); //Sort left side
            yield return mono.StartCoroutine(DoQuickSort(pivotIndex + 1, right)); //Sort right side
        }

        /// <summary>
        /// Sort the partitioned group
        /// </summary>
        /// <returns></returns>
        static IEnumerator Partition(int low, int high, Action<int> finalPivotIndex)
        {
            //Takes the last element as pivot
            int pivotIndex = high;
            int pivotValue = list[pivotIndex].Value;
            visualizationCallback?.Invoke();

            //Loop through the array from left bound to right bound.
            // J is the current element we're looking at, it's incremented automatically by the for loop.
            // I will point to the right-most number in the lower section.
            int i = low - 1;
            for (int j = low; j < high; j++)
            {
                if (list[j].Value < pivotValue)
                {
                    i++;
                    SwapNodes(list, i, j);

                    //If the current value is small than pivotValue, then move it to the left side, and increment the lower-section's marker
                    visualizationCallback?.Invoke();
                    yield return null;
                }
            }

            //Since in the first step, the pivot selected was the last number, in order for it to now
            //seperate the low and high group, it need to swap with the number on the right side of i,
            //namely, swap with the first number of the high group.
            SwapNodes(list, i + 1, high);

            yield return null;

            //Returns the pivot index.
            finalPivotIndex(i + 1);
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


//Perfect quick sort without coroutine for visualization
/*
 public class QuickSort : BaseSorter
{
        protected override IEnumerator SortAscending()
        {
            yield return StartCoroutine(DoQuickSort(0, nodes.Length - 1));
        }

        IEnumerator DoQuickSort (int left, int right)
        {
            if (left >= right) //This occurs upon reaching the end of recurrsion.
                yield break;

            int pivotIndex = Partition(left, right);

            StartCoroutine(DoQuickSort(left, pivotIndex - 1)); //Sort left side
            StartCoroutine(DoQuickSort(pivotIndex + 1, right)); //Sort right side
        }

        //Partition a sequence into two groups, seperated by a pivot
        int Partition (int low, int high)
        {
            //Takes the last element as pivot
            int pivotIndex = high;
            int pivotValue = nodes[pivotIndex].Value;

            //Loop through the array from left bound to right bound.
            // J is the current element we're looking at, it's incremented automatically by the for loop.
            // I will point to the right-most number in the lower section.
            int i = low - 1; 
            for (int j = low; j < high; j++)
            {
                if (nodes[j].Value < pivotValue)
                {
                    i++;

                    //If the current value is small than pivotValue, then move it to the left side, and increment the lower-section's marker
                    SwapNodes(i, j);

                    //HighlightNode(i, true);
                    //HighlightNode(j, true);
                    UpdateNodes();
                    //yield return null;
                    //HighlightNode(i, false);
                    //HighlightNode(j, false);
                }
            }

            //Since in the first step, the pivot selected was the last number, in order for it to now
            //seperate the low and high group, it need to swap with the number on the right side of i,
            //namely, swap with the first number of the high group.
            SwapNodes(i + 1, high);
            UpdateNodes();

            //Returns the pivot index.
            return i + 1;
        }
    }
 */

/*
 using UnityEngine;
using System.Collections;

namespace Sorting
{
    public class QuickSort : BaseSorter
{
        protected override IEnumerator SortAscending()
        {
            Debug.Log("Start quick sort of nodes: ");
            PrintNodes(0, nodes.Length - 1);

            Debug.Log(" +++++++++++++++++++++++ ");
            yield return StartCoroutine(DoQuickSort(0, nodes.Length - 1));
            PrintNodes(0, nodes.Length - 1);

            yield break;
        }

        IEnumerator DoQuickSort (int left, int right)
        {
            if (left >= right) //This occurs upon reaching the end of recurrsion.
                yield break;

            int pivotIndex = Partition(left, right);

            StartCoroutine(DoQuickSort(left, pivotIndex - 1)); //Sort left side
            StartCoroutine(DoQuickSort(pivotIndex + 1, right)); //Sort right side


        }

        //Partition a sequence into two groups, seperated by a pivot
        int Partition (int low, int high)
        {
            Debug.Log(" ------- START ------ ");
            PrintNodes(low, high);

            //Takes the last element as pivot
            int pivotIndex = high;
            int pivotValue = nodes[pivotIndex].Value;

            //Loop through the array from left bound to right bound.
            // J is the current element we're looking at, it's incremented automatically by the for loop.
            // I will point to the right-most number in the lower section.
            int i = low - 1; 
            for (int j = low; j < high; j++)
            {
                if (nodes[j].Value < pivotValue)
                {
                    i++;

                    //If the current value is small than pivotValue, then move it to the left side, and increment the lower-section's marker

                    Debug.Log("(A) i = " + i + ", j  = " + j);
                    SwapNodes(i, j);

                    //HighlightNode(i, true);
                    //HighlightNode(j, true);
                    UpdateNodes();
                    //yield return null;
                    //HighlightNode(i, false);
                    //HighlightNode(j, false);
                }
            }

            //Since in the first step, the pivot selected was the last number, in order for it to now
            //seperate the low and high group, it need to swap with the number on the right side of i,
            //namely, swap with the first number of the high group.
                    Debug.Log("(B) i + 1 = " + (i + 1) + ", high  = " + high);
            SwapNodes(i + 1, high);
            UpdateNodes();

            PrintNodes(low, high);

            //Returns the pivot index.
            return i + 1;
        }

        void PrintNodes (int low, int high)
        {
            string log = "";
            for (int i = low; i <= high; i++)
            {
                log = log + "[" +  i + "]  = " + nodes[i].Value + ", ";
            }
            Debug.Log(log);
        }
    }
}
 */