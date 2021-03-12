using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIFollowWorldspaceOne : MonoBehaviour
{
    public RectTransform canvasRect;
    public Transform followTarget;
    RectTransform myRect;

    Vector2 canvasSize;
    Vector2 uiOffset;

    void Start()
    {
        myRect = GetComponent<RectTransform>();
        canvasSize = canvasRect.sizeDelta;
        uiOffset = canvasSize * 0.5f;
        Debug.Log("canvasSize :" + canvasSize + ", uiOffset :" + uiOffset);
    }

    void Update()
    {
        Vector2 viewPos = Camera.main.WorldToViewportPoint(followTarget.position);
        Vector2 screenPos = new Vector2(viewPos.x * canvasSize.x - uiOffset.x,
            viewPos.y * canvasSize.y - uiOffset.y);

        myRect.localPosition = screenPos;
    }
}


/*
         //Both of the following gives the same result. They both works
        myRect.anchoredPosition = screenPos;
        //myRect.anchoredPosition = screenPos;
 */