using UnityEngine;

//Automatically sort a Renderer (SpriteRenderer, MeshRenderer) based on his Y position
public class PositionRendererSorter : MonoBehaviour
{

    [SerializeField] int baseSortingOrder = 5000; // This number should be higher than what any of your sprites will be on the position.y
    [SerializeField] int offset;

    float timer;
    float timerMax = .1f;
    Renderer myRenderer;

    void Awake()
    {
        myRenderer = gameObject.GetComponent<Renderer>();
    }

    void LateUpdate()
    {
        timer -= Time.deltaTime;
        if (timer > 0f) return;
        timer = timerMax;
        myRenderer.sortingOrder = (int)(baseSortingOrder - transform.position.y - offset);
    }

    public void SetOffset(int offset)
    {
        this.offset = offset;
    }
}

