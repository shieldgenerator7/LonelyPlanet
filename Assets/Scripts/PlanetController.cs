using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour
{

    public float moveSpeed = 3;
    public float gravityStrength = 5;

    public CircleCollider2D gravityCollider;

    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Size = size;
    }

    public float Range
    {
        get => gravityCollider.radius;
        private set => gravityCollider.radius = value;
    }

    public float Strength
    {
        get => gravityStrength;
        private set => gravityStrength = value;
    }

    [SerializeField]
    private float size = 1000;
    public float Size
    {
        get => size;
        private set
        {
            size = value;
            rb2d.mass = size;
            Strength = size / 200;
            moveSpeed = size / 300;
            Range = size / 300;
            Camera.main.orthographicSize = size / 100;
        }
    }


    // Update is called once per frame
    void Update()
    {
        //Get inputs
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        //Give bonus when going in the opposite direction
        float bonus = 1;
        if (Mathf.Sign(horizontal) != Mathf.Sign(rb2d.velocity.x))
        {
            bonus++;
        }
        if (Mathf.Sign(vertical) != Mathf.Sign(rb2d.velocity.y))
        {
            bonus++;
        }
        //Add force
        rb2d.AddForce(
            new Vector2(horizontal, vertical) * rb2d.mass * moveSpeed * bonus
            );
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Size += collision.gameObject.GetComponent<Rigidbody2D>()
            .mass;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Size -= collision.gameObject.GetComponent<Rigidbody2D>()
            .mass;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Rigidbody2D rb2d = collision.gameObject.GetComponent<Rigidbody2D>();
        Vector2 pullDir = this.gameObject.transform.position - collision.gameObject.transform.position;
        rb2d.AddForce(pullDir * gravityStrength);
    }
}
