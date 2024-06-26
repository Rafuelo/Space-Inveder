using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfBaricadeScript : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite half;
    public Sprite low;
    public BoxCollider2D collider2d;
    public float offsetX;

    private int durability = 15;

    void Update()
    {
        if (durability == 10)
        {
            collider2d.offset = new Vector2(collider2d.offset.x, -0.25f);
            collider2d.size = new Vector2(collider2d.size.x, 0.5f);
            spriteRenderer.sprite = half;
        }

        if (durability == 5)
        {
            collider2d.offset = new Vector2(offsetX, -0.37f);
            collider2d.size = new Vector2(0.5f, 0.26f);
            spriteRenderer.sprite = low;
        }

        if (durability == 0)
            Destroy(gameObject);
    }

    public int GetDurability()
    {
        return durability;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        durability--;
    }
}
