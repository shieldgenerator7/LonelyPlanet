using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour
{

    public float moveSpeed = 3;
    public float gravityStrength = 5;
    public float timeToGlue = 3;

    public GameObject gravityColliderObject;
    private CircleCollider2D gravityCollider;

    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        gravityCollider = gravityColliderObject.GetComponent<CircleCollider2D>();
        Size = size;
    }

    public float Range
    {
        get => gravityColliderObject.transform.localScale
            .magnitude / 2;
        private set =>
            gravityColliderObject.transform.localScale =
                Vector2.one * value * 2;
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
            float shrunkSize = size - 900;
            rb2d.mass = size;
            Strength = shrunkSize / 20;
            moveSpeed = shrunkSize / 30;
            Range = shrunkSize / 30;
            Camera.main.orthographicSize = Range * 1.5f;
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
        if (horizontal != 0 || vertical != 0)
        {
            rb2d.AddForce(
                new Vector2(horizontal, vertical) * rb2d.mass * moveSpeed * bonus
                );
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MoonController mc = collision.gameObject.GetComponent<MoonController>();
        if (mc)
        {
            mc.Planet = this;
            mc.transform.parent = transform;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Rigidbody2D collRB2D = collision.gameObject.GetComponent<Rigidbody2D>();
        if (collRB2D && !collision.gameObject.GetComponent<MoonController>())
        {
            Vector2 pullDir = this.gameObject.transform.position - collision.gameObject.transform.position;
            collRB2D.AddForce(pullDir * gravityStrength);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D collRB2D = collision.gameObject.GetComponent<Rigidbody2D>();
        if (collRB2D)
        {
            Vector3 pos = collRB2D.transform.position;
            pos.z = 0;
            collRB2D.transform.position = pos;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Rigidbody2D collRB2D = collision.gameObject.GetComponent<Rigidbody2D>();
        if (collRB2D)
        {
            if (Utility.Approximately(collRB2D.velocity, rb2d.velocity)
                || collRB2D.transform.position.z >= timeToGlue)
            {
                collision.gameObject.transform.parent = transform;
                Size += collRB2D.mass;
                Destroy(collRB2D);
            }
            else
            {
                Vector3 pos = collRB2D.transform.position;
                pos.z += Time.fixedDeltaTime;
                collRB2D.transform.position = pos;
            }
        }
    }
}
