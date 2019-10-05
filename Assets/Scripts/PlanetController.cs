using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour
{

    public float moveSpeed = 3;
    public float gravityStrength = 5;
    public float gravityRange = 3;

    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
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
            new Vector2(horizontal, vertical) * moveSpeed * bonus
            );
    }
}
