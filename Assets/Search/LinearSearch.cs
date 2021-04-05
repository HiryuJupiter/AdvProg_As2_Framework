using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Search
{
    public static class LinearSearch
    { 
        /// <summary>
        /// Search target item within an integer array
        /// </summary>
        public static int IntArraySearch (int[] data, int item)
        {
            //A linear search checks each element in the list sequentially until the target is found
            for (int i = 0; i < data.Length; i++)
                if (data[i] == item)
                    return i;
            return -1;
        }
    }
}
