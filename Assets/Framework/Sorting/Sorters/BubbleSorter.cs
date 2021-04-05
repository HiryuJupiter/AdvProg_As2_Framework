using System.Collections;

namespace Sorting.Sorter
{
    public class BubbleSorter : BaseSorter
    {
        /// <summary>
        /// Override the base sorting method with the current definition of sorting algorithm
        /// </summary>
        protected override IEnumerator SortAscending()
        {
            int nodeCount = nodes.Length;

            //-1 because we can't swap the final number out of the array.
            for (int i = 0; i < nodeCount - 1; i++)
            {
                for (int j = 0; j < nodeCount - 1; j++)
                {
                    //If the current number is bigger than the next, then swap them
                    //if (nodes[j].Value > nodes[j + 1].Value)
                    if (nodes[j].CompareTo(nodes[j + 1]) > 0)
                    {
                        SwapNodes(j, j + 1);
                        //Node _current = nodes[j];
                        //nodes[j] = nodes[j + 1];
                        //nodes[j + 1] = _current;

                        //Visualization
                        HighlightNodeBlue(j, true);
                        HighlightNodeBlue(j + 1, true);
                        UpdateNodes();
                        yield return null;
                        HighlightNodeBlue(j, false);
                        HighlightNodeBlue(j + 1, false);
                    }
                }
            }
        }
    }
}