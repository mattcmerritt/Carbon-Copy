using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!collision.GetComponent<CloneBehavior>().HasWeapon())
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
            else if (Input.GetKey(KeyCode.E) && collision.GetComponent<CloneBehavior>().CanGrabWeapon())
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
}
