using Sorting.Visualization;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Sorting.Sorter
{
    public class RadixSorter : BaseSorter
    {
        // [272, 45, 75, 81, 501, 2, 24, 66]
        // It sorts by the smallest digits first, going up...
        // Sorts by 2, 5, 5, 1, 1, 2, 4, 6
        // The single digits. 
        // 81, 501, 272, 2, 24, 45, 75, 66

        // Next it looks at the second digits
        // 2 becomes 02
        // 501, 02, 24, 45, 66, 272, 75, 81, 

        /// <summary>
        /// Override the base sorting method with the current definition of sorting algorithm
        /// </summary>
        protected override IEnumerator SortAscending()
        {
            //Do counting sort for every digit.
            int max = GetMaxNumber();
            for (int exp = 1; max / exp > 0; exp *= 10)
            {
                yield return StartCoroutine(CountSort(exp));
            }
            yield return null;
        }

        /// <summary>
        /// Sort by digits
        /// </summary>
        /// <param name="exponents"></param>
        /// <returns></returns>
        IEnumerator CountSort (int exponents)
        {
            Node[] output = new Node[nodes.Length]; //The output array for the entire sorted array for this digit

            //The 10 unit long count array starts off being 10 buckets for tracking occurances of 0~9 integers.
            int[] count = new int[10];
            for (int i = 0; i < 10; i++)
            {
                count[i] = 0;
            }

            //Record occurances
            int GetBucketIndex(int nodeArrayIndex) => (nodes[nodeArrayIndex].Value / exponents) % 10;
            
            for (int i = 0; i < nodes.Length; i++)
            {
                count[GetBucketIndex(i)]++;
            }

            //Now, at this point, count ceases to record occurances of each digits. Instead, it is now recording
            //the index position of the numbers that they will take on in the output. For example, if this was the
            //bucket: [1] [3] [2] [1], so 1 occurance of 0, 3 occurances of 1, 2 occurances of 2, 1 occurances of 1.
            //They will now record the number's index position in the final index:  [1] [4] [6] [1]. 
            for (int i = 1; i < 10; i++)
            {
                count[i] += count[i - 1];
            }

            //Build the output array
            for (int i = nodes.Length - 1; i >= 0; i--)
            {
                //We minus 1 because, say in the bucket [1] [4] [2], the first number with a 0 at the end, should appear 
                //in the first place, but the first place is index 0, not 1, so we have to minus 1.
                int bucketIndex = GetBucketIndex(i);
                output[count[bucketIndex] - 1] = nodes[i];
                count[bucketIndex]--;
            }

            for (int i = 0; i < nodes.Length; i++)
            {
                nodes[i] = output[i];

                HighlightNodeBlue(i, true);
                UpdateNodes();
                yield return null;
                HighlightNodeBlue(i, false);
            }
        }

        int GetMaxNumber()
        {
            int max = nodes[0].Value;
            for (int i = 1; i < nodes.Length; i++)
            {
                if (nodes[i].Value > max)
                    max = nodes[i].Value;
            }
            return max;
        }
    }
}

//Complex version, totally working
/*
  public class RadixSorter : BaseSorter
    {
        // [272, 45, 75, 81, 501, 2, 24, 66]
        // It sorts by the smallest digits first, going up...
        // Sorts by 2, 5, 5, 1, 1, 2, 4, 6
        // The single digits. 
        // 81, 501, 272, 2, 24, 45, 75, 66

        // Next it looks at the second digits
        // 2 becomes 02
        // 501, 02, 24, 45, 66, 272, 75, 81, 

        protected override IEnumerator SortAscending()
        {
            //Do counting sort for every digit.
            int max = GetMaxNumber();
            for (int exp = 1; max / exp > 0; exp *= 10)
            {
                CountSort(exp);
            }
            yield return null;
        }

        void CountSort (int exponents)
        {
            Node[] output = new Node[nodes.Length]; //The output array for the entire sorted array for this digit

            //The 10 unit long count array starts off being 10 buckets for tracking occurances of 0~9 integers.
            int[] count = new int[10];
            for (int i = 0; i < 10; i++)
            {
                count[i] = 0;
            }

            //Record occurances
            int GetBucketIndex(int nodeArrayIndex) => (nodes[nodeArrayIndex].Value / exponents) % 10;
            
            for (int i = 0; i < nodes.Length; i++)
            {
                count[GetBucketIndex(i)]++;
            }

            //Now, at this point, count ceases to record occurances of each digits. Instead, it is now recording
            //the index position of the numbers that they will take on in the output. For example, if this was the
            //bucket: [1] [3] [2] [1], so 1 occurance of 0, 3 occurances of 1, 2 occurances of 2, 1 occurances of 1.
            //They will now record the number's index position in the final index:  [1] [4] [6] [1]. 
            for (int i = 1; i < 10; i++)
            {
                count[i] += count[i - 1];
            }

            //Build the output array
            for (int i = nodes.Length - 1; i >= 0; i--)
            {
                //We minus 1 because, say in the bucket [1] [4] [2], the first number with a 0 at the end, should appear 
                //in the first place, but the first place is index 0, not 1, so we have to minus 1.
                int bucketIndex = GetBucketIndex(i);
                output[count[bucketIndex] - 1] = nodes[i];
                count[bucketIndex]--;
            }

            for (int i = 0; i < nodes.Length; i++)
            {
                nodes[i] = output[i];
            }

            UpdateNodes();
        }

        int GetMaxNumber()
        {
            int max = nodes[0].Value;
            for (int i = 1; i < nodes.Length; i++)
            {
                if (nodes[i].Value > max)
                    max = nodes[i].Value;
            }
            return max;
        }

        int GetNumDigits()
        {
            int max = nodes[0].Value;
            for (int i = 1; i < nodes.Length; i++)
            {
                if (nodes[i].Value > max)
                    max = nodes[i].Value;
            }
            return max.ToString().Length;
        }
    }
 */

//Simplest version possible
/*
 protected override IEnumerator SortAscending()
        {
            
            //Do counting sort for every digit.
            int max = GetMaxNumber();
            Debug.Log(" max: " + max);
            for (int exp = 1; max / exp > 0; exp *= 10)
            {
                Debug.Log(" exp: " + exp);

                Radix(exp);
            }

            yield return null;
        }

        //Exp represents the digits
        void Radix (int exponents)
        {
            //Create a new counting array of 10 empty buckets to count the occurance of 
            //each unique number. 
            List<List<Node>> bucket = new List<List<Node>>();
            for (int i = 0; i < 10; i++)
            {
                bucket.Add(new List<Node>());
            }

            //Add the array elements to a bucket based on the value of the number at the 
            //digit.
            for (int i = 0; i < nodes.Length; i++)
            {
                int bucketNumber = (nodes[i].Value /  exponents) % 10;
                Debug.Log("nodes[i].Value: " + nodes[i].Value + ", digit" + exponents +  ", bucketNumber: " + bucketNumber);
                bucket[bucketNumber].Add(nodes[i]);
            }

            //Flatten the array
            List<Node> flattened = new List<Node>();
            for (int i = 0; i < bucket.Count; i++)
            {
                for (int j = 0; j < bucket[i].Count; j++)
                {
                    flattened.Add(bucket[i][j]);
                }
            }
            nodes = flattened.ToArray();
            UpdateNodes();
            //for (int i = 0; i < nodes.Length; i++)
            //    bucket[(nodes[i].Value / (digit* 10) ) % 10]++;

            //int[] output = new int[nodes.Length];

            ////Change count[i] so that it now contains actual position of this character 
            ////in the output array
            //for (int i = 1; i < 10; i++)
            //{
            //    bucket[i] += bucket[i + 1];
            //}
        }

        int GetMaxNumber()
        {
            int max = nodes[0].Value;
            for (int i = 1; i < nodes.Length; i++)
            {
                if (nodes[i].Value > max)
                    max = nodes[i].Value;
            }
            return max;
        }

        int GetNumDigits()
        {
            int max = nodes[0].Value;
            for (int i = 1; i < nodes.Length; i++)
            {
                if (nodes[i].Value > max)
                    max = nodes[i].Value;
            }
            return max.ToString().Length;
        }
    }
 */

//In class version
/*
 using System.Collections;
using UnityEngine;

using Array = System.Array;


namespace Sorting
{
    public class RadixSorter : BaseSorter
    {
        // [272, 45, 75, 81, 501, 2, 24, 66]
        // It sorts by the least significant digits
        // Sorts by 2, 5, 5, 1, 1, 2, 4, 6
        // The single digits. 
        // 81, 501, 272, 2, 24, 45, 75, 66
        // Next it looks at the second digits
        // 2 becomes 02
        // 501, 02, 24, 45, 66, 272, 75, 81, 
        //catch, ctreating a new array every time


        protected override IEnumerator SortAscending()
        {
            int nodeCount = nodes.Length;
            int i, j;
            Node[] temp = new Node[nodes.Length];

            //Integer cannot have more than 31 digits
            for (int shift = 31; shift > -1; --shift)
            {
                //Reset j to zero
                j = 0;

                //loop through the whole array
                //Why is removing -1 changes it
                //for (i = 0; i < nodeCount - 1; i++)
                //Why does this works??? for (i = 0; i < nodeCount; ++i)
                for (i = 0; i < nodeCount; ++i)
                {
                    //Determine if the bit shifted is above 0
                    //If it is above 0, it means there is a number
                    //e.g. shifting 2 << 1 = 0. Shifting 12 << 1 becaues we're in the next digit.
                    bool move = (nodes[i].Value << shift) >= 0; 

                    if (shift == 0 ? !move : move)
                    {
                        nodes[i - j] = nodes[i];
                    }
                    else
                    {
                        temp[j++] = nodes[i];
                    }

                }

                //Copy the data to the temp array
                Array.Copy(temp, 0, nodes, nodes.Length - j, j);

                //Simply visualization, not part of algorithm
                HighlightNodeBlue(0, true);
                HighlightNodeBlue(1, true);
                UpdateNodes();
                yield return null;
                HighlightNodeBlue(0, false);
                HighlightNodeBlue(1, false);
            }
        }
    }
}
 */