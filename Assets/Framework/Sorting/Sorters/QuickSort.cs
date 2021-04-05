using UnityEngine;
using Sorting.Visualization;
using System;
using System.Collections;

namespace Sorting.Sorter
{
    /// <summary>
    /// Holds the logic for the sorting algorithm quick sort
    /// </summary>
    public class QuickSort : BaseSorter
    {
        /// <summary>
        /// Override the base sorting method with the current definition of sorting algorithm
        /// </summary>
        protected override IEnumerator SortAscending()
        {
            StartCoroutine(DoQuickSort(0, nodes.Length - 1));
            yield return null;
        }

        /// <summary>
        /// Recursively partition the sequence into two groups, seperated by a pivot
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        IEnumerator DoQuickSort(int left, int right)
        {
            if (left >= right) //This occurs upon reaching the end of recurrsion.
                yield break;

            int pivotIndex = 0;
            yield return StartCoroutine(Partition(left, right, (finalPivotIndex) => pivotIndex = finalPivotIndex));
            yield return StartCoroutine(DoQuickSort(left, pivotIndex - 1)); //Sort left side
            yield return StartCoroutine(DoQuickSort(pivotIndex + 1, right)); //Sort right side
        }

        /// <summary>
        /// Sort the partitioned group
        /// </summary>
        /// <returns></returns>
        IEnumerator Partition(int low, int high, Action<int> finalPivotIndex)
        {
            //Takes the last element as pivot
            int pivotIndex = high;
            int pivotValue = nodes[pivotIndex].Value;
            HighlightNodeRed(pivotIndex, true);
            UpdateNodes();

            //Loop through the array from left bound to right bound.
            // J is the current element we're looking at, it's incremented automatically by the for loop.
            // I will point to the right-most number in the lower section.
            int i = low - 1;
            for (int j = low; j < high; j++)
            {
                if (nodes[j].Value < pivotValue)
                {
                    i++;
                    SwapNodes(i, j);

                    //If the current value is small than pivotValue, then move it to the left side, and increment the lower-section's marker
                    HighlightNodeBlue(i, true);
                    HighlightNodeBlue(j, true);

                    
                    UpdateNodes();
                    yield return null;

                    HighlightNodeBlue(i, false);
                    HighlightNodeBlue(j, false);
                }
            }

            //Since in the first step, the pivot selected was the last number, in order for it to now
            //seperate the low and high group, it need to swap with the number on the right side of i,
            //namely, swap with the first number of the high group.
            SwapNodes(i + 1, high);

            HighlightNodeBlue(i + 1, true);
            HighlightNodeBlue(high, true);
            
            UpdateNodes();
            yield return null;

            HighlightNodeBlue(i + 1, false);
            HighlightNodeBlue(high, false);

            HighlightNodeRed(i + 1, false);
            UpdateNodes();

            //Returns the pivot index.
            finalPivotIndex(i + 1);
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