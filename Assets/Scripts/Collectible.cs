using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    private bool isCollected = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!isCollected)
        {
            if (collision.tag == "Player")
            {
                Weapon weapon = GetComponent<Weapon>();

                if (weapon != null)
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

                        isCollected = true;
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

                        isCollected = true;
                    }
                }
            }
        }
    }

    public void CanCollectAgain()
    {
        isCollected = false;
    }
}
