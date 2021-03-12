using UnityEngine;
using System.Collections;

public class RotateTowardsMouse : MonoBehaviour
{
    Vector3 mousePos;

    void Update()
    {
        mousePos = UIUtil.GetMouseWorldPosition();
        transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);
    }
}
