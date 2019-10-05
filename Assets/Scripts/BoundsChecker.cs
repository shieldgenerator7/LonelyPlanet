using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsChecker : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        //Teleport the object
        if (collision.gameObject.transform.parent == null
            || collision.gameObject.transform.parent.GetComponent<Rigidbody2D>() == null)
        {
            collision.gameObject.transform.position *= -1;
        }
    }
}
