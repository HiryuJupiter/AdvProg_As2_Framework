using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFollowWorldspaceObjTwo : MonoBehaviour
{
    public Canvas canvas;
    public Transform followTarget;
    RectTransform canvasRect;
    RectTransform myRect;

    Vector2 canvasSize;
    Vector2 uiOffset;
    float scaleFactor;

    void Start()
    {
        myRect = GetComponent<RectTransform>();
        canvasRect = canvas.GetComponent<RectTransform>();

        canvasSize = canvasRect.sizeDelta;
        uiOffset = canvasSize * 0.5f;
        scaleFactor = canvas.scaleFactor;
    }

    void Update()
    {
        Vector2 screenPos = Camera.main.WorldToScreenPoint(followTarget.position) / scaleFactor;
        myRect.localPosition = screenPos - uiOffset;
    }
}
