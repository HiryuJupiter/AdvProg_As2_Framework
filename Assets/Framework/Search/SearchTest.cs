using System.Collections;
using UnityEngine;

namespace Search
{
    public class SearchTest : MonoBehaviour
    {
        [SerializeField] int[] array = new int[] { 0, 1, 2, 3, 4, 5, 6 };
        [SerializeField] int searchTarget = 2;

        void Start()
        {
            PerformSearch();
        }

        /// <summary>
        /// A quick test to see if linear search is working
        /// </summary>
        public void PerformSearch ()
        {
            string s = "";
            for (int i = 0; i < array.Length; i++)
            {
                s += array[i].ToString() + ", ";
            }
            Debug.Log("Array: " + s);

            int result = LinearSearch.IntArraySearch(array, searchTarget);
            Debug.Log($"Integer {searchTarget} found? " + (result == -1 ? "false" : "true"));
        }
    }
}
