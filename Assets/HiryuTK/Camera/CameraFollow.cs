using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HiryuTK.CameraControl
{
    public class CameraFollow : MonoBehaviour
    {
        public float xMin;
        public float xMax;
        public float yMin;
        public float yMax;

        [SerializeField] private Transform target;

        void Update()
        {
            if (target != null)
            {
                float x = Mathf.Clamp(target.position.x, xMin, xMax);
                float y = Mathf.Clamp(target.position.y, yMin, yMax);
                transform.position = new Vector3(x, y, transform.position.z);
            }
        }
    }
}
