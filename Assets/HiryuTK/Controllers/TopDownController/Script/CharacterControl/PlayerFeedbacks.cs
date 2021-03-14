using System.Collections;
using UnityEngine;

namespace HiryuTK.TopDownController
{
    public class PlayerFeedbacks : MonoBehaviour
    {
        [SerializeField] private GameObject model;

        public void SetModelVisibility (bool isVisible)
        {
            model.SetActive(isVisible);
        }
    }
}