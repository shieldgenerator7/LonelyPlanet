using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsChecker : MonoBehaviour
{
    public bool circularBounds = true;

    private Collider2D coll2D;

    private void Start()
    {
        coll2D = GetComponent<Collider2D>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Teleport the object
        if (collision.gameObject.transform.parent == null
            || collision.gameObject.transform.parent.GetComponent<Rigidbody2D>() == null)
        {
            if (circularBounds)
            {
                collision.gameObject.transform.position *= -1;
            }
            else
            {
                Vector3 pos = collision.gameObject.transform.position;
                if (pos.x < coll2D.bounds.min.x || pos.x > coll2D.bounds.max.x)
                {
                    pos.x *= -1;
                }
                if (pos.y < coll2D.bounds.min.y || pos.y > coll2D.bounds.max.y)
                {
                    pos.y *= -1;
                }
                collision.gameObject.transform.position = pos;
            }
        }
    }
}
