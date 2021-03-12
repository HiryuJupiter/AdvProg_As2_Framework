using System.Collections;
using UnityEngine;

namespace ClickAndDrag
{
    public class ObjectFollowMouseTest : MonoBehaviour
    {
        float zOffset;

        void Start()
        {
            zOffset = transform.position.z - Camera.main.transform.position.z;
        }

        void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 mousePos = ray.origin + ray.direction * zOffset;
            transform.position = mousePos;
        }
    }
}
