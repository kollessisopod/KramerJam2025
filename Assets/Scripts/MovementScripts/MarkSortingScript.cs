using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AlwaysOnTopSprite : MonoBehaviour
{
    public int sortingOrder = 20000; // Very high to appear in front of most things

    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = sortingOrder;
    }

    void LateUpdate()
    {
        if (spriteRenderer.sortingOrder != sortingOrder)
            spriteRenderer.sortingOrder = sortingOrder;
    }
}