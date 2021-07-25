using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerCollision : MonoBehaviour
{
    public BuildSoul buildSoul;

    SpriteRenderer spriteRenderer;
    Color color;
    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        color = spriteRenderer.color;
        spriteRenderer.color = new Color(1.0f, 0.0f, 0.0f, color.a);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BuildPlace"))
        {
            spriteRenderer.color = color;
            buildSoul.summonLight.SetActive(true);
            buildSoul.summonLight.transform.position = collision.gameObject.transform.position;
            buildSoul.spot = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("BuildPlace"))
        {
            spriteRenderer.color = new Color(1.0f, 0.0f, 0.0f, color.a);
            buildSoul.summonLight.SetActive(false);
            buildSoul.buildPossible = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("BuildPlace"))
        {
             buildSoul.buildPossible = true;
        }
    }
}
