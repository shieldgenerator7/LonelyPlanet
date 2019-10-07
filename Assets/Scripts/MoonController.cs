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
    private ParticleSystem particleSystem;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        particleSystem = GetComponent<ParticleSystem>();
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
        if (collidingObject.CompareTag("Asteroid"))
        {
            float moonSize = transform.localScale.x;
            float asteroidSize = collidingObject.transform.localScale.x;
            transform.localScale =
                transform.localScale.adjustVector(-asteroidSize);
            collidingObject.transform.localScale =
                collidingObject.transform.localScale.adjustVector(-moonSize);
            if (transform.localScale.x <= 0)
            {
                destroyMoon();
            }
            if (collidingObject.transform.localScale.x <= 0)
            {
                Destroy(collidingObject);
            }
            else
            {
                Rigidbody2D collRB2D = collidingObject.GetComponent<Rigidbody2D>();
                if (!collRB2D)
                {
                    collRB2D = collidingObject.AddComponent<Rigidbody2D>();
                    collRB2D.mass = 0.75f * collidingObject.transform.localScale.x;
                }
            }
        }
    }

    public void flashMoon()
    {
        particleSystem.Play();
    }

    private void destroyMoon()
    {
        //Visual effects
        if (Planet)
        {
            Vector2 outDir = transform.position - Planet.transform.position;
            FindObjectOfType<CameraController>().ScreenShakeVector =
                outDir.normalized
                * 0.1f
                * transform.localScale.x;
        }
        //Call the delegate
        onDestroyed?.Invoke(this);
        //Actually destroy this moon
        Destroy(gameObject);
    }
    public delegate void OnDestroyed(MoonController moon);
    public OnDestroyed onDestroyed;
}
