using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Transform followObj;

    float zOffset;
    Vector3 tgtPos;

    private void Awake()
    {
        zOffset = transform.position.z - followObj.position.z;
    }

    void Update()
    {
        tgtPos = followObj.position;
        tgtPos.z = zOffset;
        transform.position = tgtPos;
    }
}