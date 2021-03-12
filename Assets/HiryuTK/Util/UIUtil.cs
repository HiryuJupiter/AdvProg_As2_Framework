using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public static class UIUtil
{
    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return worldPosition;
    }

    // Is Mouse over a UI Element? Used for ignoring World clicks through UI
    public static bool IsPointerOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    public static List<RaycastResult> UIHitsResults ()
    {
        PointerEventData pe = new PointerEventData(EventSystem.current);
        pe.position = Input.mousePosition;
        List<RaycastResult> hits = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pe, hits);
        return hits;
    }
}