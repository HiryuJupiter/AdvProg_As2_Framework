using UnityEngine;
using System.Collections;

public class FlyweightGlobalRotation : MonoBehaviour
{
    public static Quaternion CoinRotation;

    public float CoinRotationModifier = 0.5f;

    void Update()
    {
        CoinRotation = Quaternion.Euler(0f, 0f, Time.time * CoinRotationModifier) ;
    }
}