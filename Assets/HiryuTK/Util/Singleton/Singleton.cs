using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HiryuTK.GameDataManagement
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; protected set; }

        protected void DeleteDuplicateSingleton()
        {
            if (Instance != null && Instance != this)
            {
                Debug.Log("Duplicate " + typeof(T).ToString() + " has being destroyed.");
                DestroyImmediate(gameObject);
                return;
            }
        }
    }
}

/*Usage
 * protected void Awake()
{
    DeleteDuplicateSingleton();
    if (instance == null)
    {
        instance = this;
        DontDestroyOnLoad(this);
    }
}*/
