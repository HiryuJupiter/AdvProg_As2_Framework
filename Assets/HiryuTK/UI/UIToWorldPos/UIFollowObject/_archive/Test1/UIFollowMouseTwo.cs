using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIFollowWorldspaceObjFourth : MonoBehaviour
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
    }

    void Update()
    {
        //myRect.localPosition = (Vector2)Input.mousePosition - uiOffset;

        Vector2 viewPos = Camera.main.WorldToViewportPoint(followTarget.position);
        Vector2 screenPos = new Vector2(viewPos.x * canvasSize.x - uiOffset.x,
            viewPos.y * canvasSize.y - uiOffset.y);

        myRect.anchoredPosition = screenPos;
    }
}