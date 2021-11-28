using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            transform.SetParent(collision.transform);
            Collider2D collider = GetComponent<Collider2D>();
            collider.enabled = false;
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            renderer.enabled = false;
            this.enabled = false;

            CloneBehavior clone = collision.GetComponent<CloneBehavior>();
            clone.Collect(gameObject);
        }
    }
}
