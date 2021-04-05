using System.Collections;
using UnityEngine;

public class ResourceLoadTest : MonoBehaviour
{
    [SerializeField]  Texture2D texture;
    private void Start()
    {
        texture = Resources.Load<Texture2D>("MainChar_");
    }

    private void Update()
    {

    }
}