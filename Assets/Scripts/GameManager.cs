using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float gameOverHangTimeDuration = 5;
    private float gameOverStartTime = -1;

    private static GameManager instance;

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOverStartTime > 0
            && Time.time > gameOverStartTime + gameOverHangTimeDuration)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            int touchCount = Input.touchCount;
            bool mouseDown = Input.GetMouseButton(0);
            if (horizontal != 0 || vertical != 0
                || touchCount > 0 || mouseDown)
            {
                //Restart the game
                SceneManager.LoadScene(0);
            }
        }
    }

    public static void gameOver()
    {
        instance.gameOverStartTime = Time.time;
    }
}
