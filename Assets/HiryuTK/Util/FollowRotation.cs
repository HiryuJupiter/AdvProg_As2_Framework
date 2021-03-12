using UnityEngine;

public class FollowRotation : MonoBehaviour
{
    public Transform followObj;

    void Update()
    {
        transform.rotation = followObj.rotation;
    }
}