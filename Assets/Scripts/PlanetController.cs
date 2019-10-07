using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlanetController : MonoBehaviour
{

    public float moveSpeed = 3;
    public float gravityStrength = 5;
    public float timeToGlue = 3;

    [Header("Size Factors")]
    public float rangeDivider = 30;

    [Header("Components")]
    public GameObject gravityColliderObject;
    private CircleCollider2D gravityCollider;
    public TextMeshProUGUI scoreText;

    private Rigidbody2D rb2d;
    private List<MoonController> moons = new List<MoonController>();

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        gravityCollider = gravityColliderObject.GetComponent<CircleCollider2D>();
        Size = size;
    }

    private int score = 0;
    public int Score
    {
        get => score;
        private set
        {
            score = Mathf.CeilToInt(value);
            scoreText.text = "" + score;
        }
    }

    public float Range
    {
        get => gravityColliderObject.transform.localScale.x / 2;
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
            Range = shrunkSize / rangeDivider;
            Camera.main.orthographicSize = Range * 2.5f;
        }
    }


    // Update is called once per frame
    void Update()
    {
        //Get inputs
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        //Check mouse + touch inputs
        if (horizontal == 0 && vertical == 0
            && (Input.GetMouseButton(0) || Input.touchCount > 0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 dir = mousePos - (Vector2)transform.position;
            if (dir.magnitude > 1)
            {
                dir.Normalize();
            }
            horizontal = dir.x;
            vertical = dir.y;
        }
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
            if (!moons.Contains(mc))
            {
                moons.Add(mc);
                mc.Planet = this;
                mc.transform.parent = transform;
                mc.onDestroyed += removeMoon;
                addScore(mc.gameObject, false);
                mc.flashMoon();
            }
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
                //Glue the object
                glue(collision.gameObject);
            }
            else
            {
                Vector3 pos = collRB2D.transform.position;
                pos.z += Time.fixedDeltaTime;
                collRB2D.transform.position = pos;
            }
        }
    }

    /// <summary>
    /// Checks to see if the new added piece is completely inside the range
    /// (assumes circlular range and circular new piece shape)
    /// </summary>
    /// <param name="newPiece"></param>
    /// <returns>True for inside, False for outside</returns>
    bool isInside(GameObject newPiece)
    {
        float radius =
            newPiece.GetComponent<CircleCollider2D>().radius
            * newPiece.transform.localScale.x;
        Vector2 outDir = newPiece.transform.position - transform.position;
        if (outDir.magnitude + radius <= Range)
        {
            return true;//it is inside
        }
        return false;//it is outside
    }

    private void glue(GameObject asteroid)
    {
        Rigidbody2D collRB2D = asteroid.GetComponent<Rigidbody2D>();
        SpriteRenderer collSR = asteroid.GetComponent<SpriteRenderer>();
        asteroid.transform.parent = transform;
        Size += collRB2D.mass;
        //Score
        addScore(asteroid);
        //Finalize asteroid
        Destroy(collRB2D);
        //Detect game over condition
        if (!isInside(asteroid))
        {
            GameManager.gameOver();
            collSR.color = Color.red;
            Destroy(rb2d);
            Destroy(this);
        }
        //Visual Effects
        asteroid.GetComponent<ParticleSystem>().Play();
        Vector2 outDir = asteroid.transform.position - transform.position;
        FindObjectOfType<CameraController>().ScreenShakeVector =
            outDir.normalized
            * 0.05f
            * asteroid.transform.localScale.x;
    }

    private void removeMoon(MoonController moon)
    {
        moons.Remove(moon);
    }

    private void addScore(GameObject rock, bool applyMoonBonus = true)
    {
        Rigidbody2D rockRB2D = rock.GetComponent<Rigidbody2D>();
        SpriteRenderer rockSR = rock.GetComponent<SpriteRenderer>();
        float scoreAddend = rockRB2D.mass * 100;
        if (applyMoonBonus)
        {
            float multiplier = 1;
            foreach (MoonController moon in moons)
            {
                multiplier += moon.transform.localScale.x;
                moon.flashMoon();
            }
            scoreAddend *= multiplier;
        }
        Score += Mathf.CeilToInt(scoreAddend);
        scoreText.color = rockSR.color;
    }
}
