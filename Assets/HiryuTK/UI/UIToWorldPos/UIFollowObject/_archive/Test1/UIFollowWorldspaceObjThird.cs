using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIFollowWorldspaceObjThird : MonoBehaviour
{
    public Canvas canvas;
    public Transform followTarget;
    RectTransform myRect;

    Vector2 viewportOffset= new Vector2(0.5f, .5f);

    void Start()
    {
        myRect = GetComponent<RectTransform>();
    }

    void Update()
    {
        Vector2 viewPos = Camera.main.WorldToViewportPoint(followTarget.position) / canvas.scaleFactor;
        myRect.anchoredPosition = viewPos - viewportOffset;
    }
}