using UnityEngine;
using System.Collections;
using System;

namespace HiryuTK.GameDataManagement
{
    /*
 * Concatenates all the elements of a string array, using the specified separator between each element.
String.Join(String, String[])

Returns a string array that contains the substrings in this instance that are delimited by elements of a specified string or Unicode character array.
Split(Char[])

 */


    //PlayerPref only works on int, float, string
    //WebBuilds are limited to 1MB size
    //Good for player settings (.e.g volumn), simple level progressions, high scoreboards,

    public static class SaveStringArrayPlayerPrefSaver
    {
        #region s Key, sArr Value
        public static bool SaveStringArrayToOneKey(string key, params string[] arry)
        {
            if (arry.Length <= 0) return false;

            try
            {
                PlayerPrefs.SetString(key, string.Join(",", arry));
                //PlayerPrefs.SetString(key, string.Join("\n"[0].ToString(), sArr));
            }
            catch (Exception e)
            {
                Debug.Log("Exception: " + e + ". Possibly due to WebGL build has ran out of allotted memory");
                return false;
            }

            return true;
        }


        public static string[] GetStringArrayFromOneKey(string key)
        {
            return PlayerPrefs.GetString(key).Split("\n"[0]);
        }

        public static string[] GetStringArrays(string key, int defaultSize, string defaultValue)
        {
            if (PlayerPrefs.HasKey(key))
                return PlayerPrefs.GetString(key).Split(',');

            //If no key was found:
            string[] array = new string[defaultSize];
            for (int i = 0; i < defaultSize; i++)
            {
                array[i] = defaultValue;
            }
            return array;
        }

        #endregion

        #region sArr Key, sArr Value
        public static bool SetStringArrayToKeyArrays(string[] keys, params string[] values)
        {
            if (values.Length <= 0) return false;

            try
            {
                PlayerPrefs.SetString(string.Concat(keys), string.Join(",", values));
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }


        public static string[] GetStringArrayFromKeyArrays(string[] keyArray)
        {
            return PlayerPrefs.GetString(string.Concat(keyArray)).Split("\n"[0]);
        }

        public static string[] GetStringArrayFromKeyArrays(string[] keys, int defaultSize, string defaultValue)
        {
            if (PlayerPrefs.HasKey(string.Concat(keys)))
            {
                string[] vArr = PlayerPrefs.GetString(string.Concat(keys)).Split(',');

                if (vArr.Length == defaultSize)
                {
                    return vArr;
                }
                else
                {
                    goto GenerateDefault;
                }
            }
            else
            {
                goto GenerateDefault;
            }



        GenerateDefault:
            {
                Debug.Log("Key is not found or the array value is not the size. Generating default array");
                string[] empArr = new string[defaultSize];
                for (int i = 0; i < defaultSize; i++)
                {
                    empArr[i] = "";
                }
                return empArr;
            }
        }

        #endregion
    }

}

