using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonController : MonoBehaviour
{
    public float moveSpeed = 1;

    private PlanetController planet;
    public PlanetController Planet
    {
        get => planet;
        set
        {
            planet = value;
            if (planet)
            {
                rb2d.isKinematic = true;
            }
            else
            {
                rb2d.isKinematic = false;
            }
        }
    }

    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Planet != null)
        {
            transform.RotateAround(
                planet.transform.position,
                Vector3.forward,
                moveSpeed
                );
            Vector2 outDir = transform.position - planet.transform.position;
            if (outDir.magnitude != planet.Range)
            {
                transform.position =
                    (Vector2)planet.transform.position
                    + (outDir.normalized * planet.Range);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collidingObject = collision.collider.gameObject;
        if (collidingObject.GetComponent<MoonController>())
        {
            Planet = null;
        }
        else
        {
            if (collidingObject.CompareTag("Asteroid"))
            {
                Destroy(gameObject);
                Destroy(collidingObject);
            }
        }
    }
}
