using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HiryuTK.UI.WorldPositionUIText
{
    public class UIFollowMouse : MonoBehaviour
    {
        public Canvas canvas;
        public RectTransform canvasRect;

        RectTransform myRect;

        Vector2 canvasSize;
        Vector2 uiOffset; //The canvas's middle point is 0,0 instead of bottom left.

        void Start()
        {
            myRect = GetComponent<RectTransform>();
            canvasSize = canvasRect.sizeDelta;
            uiOffset = canvasSize * .5f;
        }

        void Update()
        {
            myRect.localPosition = (Vector2)Input.mousePosition / canvas.scaleFactor - uiOffset;
        }
    }
}