using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFollowWorldspaceObjTwoB : MonoBehaviour
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
        Vector2 screenPos = Camera.main.WorldToScreenPoint(followTarget.position);

        //Multiple by inverse scale factor
        screenPos.x *= canvasSize.x / (float)Screen.width;
        screenPos.y *= canvasSize.y / (float)Screen.height;

        // set it
        myRect.localPosition = screenPos - uiOffset;
    }
}
