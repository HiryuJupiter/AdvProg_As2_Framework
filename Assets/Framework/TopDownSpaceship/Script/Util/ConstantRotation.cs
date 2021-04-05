using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRotation : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(transform.forward, 20f * Time.deltaTime);
    }
}
