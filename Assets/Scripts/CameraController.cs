using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject focusTarget;

    private Rigidbody2D focusRB2D;
    private Vector2 savedVecolity = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        focusRB2D = focusTarget.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 pos = transform.position;
        if (savedVecolity != focusRB2D.velocity)
        {
            savedVecolity = focusRB2D.velocity;
            pos = Vector2.MoveTowards(pos, focusTarget.transform.position, Time.deltaTime * focusRB2D.velocity.magnitude * 0.9f);
            
        }
        else
        {
            pos = focusTarget.transform.position;
        }
        pos.z = transform.position.z;
        transform.position = pos;
    }
}
