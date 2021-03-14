using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HiryuTK.TopDownController
{
    public class BulletRotation_TopDown : MonoBehaviour
    {
        [SerializeField] public float RotationSpeed = 30f;
        void Update()
        {
            transform.Rotate(Vector3.up, RotationSpeed * Time.deltaTime);
        }
    }
}
