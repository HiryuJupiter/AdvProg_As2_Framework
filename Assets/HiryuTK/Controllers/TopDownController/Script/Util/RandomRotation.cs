using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotation : MonoBehaviour
{
    Vector3 rot;
    private void Start()
    {
        rot = new Vector3(Random.Range(0f, 360f),
            Random.Range(0f, 360f),
            Random.Range(0f, 360f));
    }

    void Update()
    {
        transform.Rotate(rot * Time.deltaTime);
    }
}
