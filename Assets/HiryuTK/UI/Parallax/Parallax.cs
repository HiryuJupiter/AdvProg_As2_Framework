using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float ySpeed;
    public float xSpeed;

    float centerX;
    float centerY;
    float startX;
    float startY;

    RectTransform rect;

    void Awake()
    {
        //Cache
        rect = GetComponent<RectTransform>();
        centerX = Screen.width / 2f;
        centerY = Screen.height / 2f;
        startX = rect.localPosition.x;
        startY = rect.localPosition.y;
    }

    void Update()
    {
        float xFromCenter = Input.mousePosition.x - centerX;
        float yFromCenter = Input.mousePosition.y - centerY;

        Vector3 p = rect.localPosition;
        p.x = startX + xFromCenter * xSpeed;
        p.y = startY + yFromCenter * ySpeed;
        rect.localPosition = p;
    }
}
