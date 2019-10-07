using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float gameOverHangTimeDuration = 5;
    private float gameOverStartTime = -1;

    public List<GameObject> hiddenUntilStart;

    public enum GameState
    {
        NOT_STARTED,
        STARTING,
        IN_PROGRESS,
        GAME_OVER
    }
    public GameState gameState { get; private set; } = GameState.NOT_STARTED;

    public bool AnyInput
    {
        get
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            int touchCount = Input.touchCount;
            bool mouseDown = Input.GetMouseButton(0);
            return
                horizontal != 0
                || vertical != 0
                || touchCount > 0
                || mouseDown;
        }
    }

    public static GameManager instance { get; private set; }

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
        if (gameState == GameState.NOT_STARTED)
        {
            FindObjectOfType<PlanetController>().enabled = false;
        }
        if (gameState == GameState.STARTING)
        {
            foreach (GameObject go in hiddenUntilStart)
            {
                go.SetActive(true);
            }
            FindObjectOfType<PlanetController>().enabled = true;
            gameState = GameState.IN_PROGRESS;
        }
        else if (gameState == GameState.GAME_OVER)
        {
            if (gameOverStartTime > 0
                && Time.time > gameOverStartTime + gameOverHangTimeDuration)
            {
                if (AnyInput)
                {
                    //Restart the game
                    resetGame();
                }
            }
        }
    }

    public static void playGame()
    {
        if (instance.gameState == GameState.GAME_OVER)
        {
            resetGame();
        }
        instance.gameState = GameState.STARTING;
    }

    public static void gameOver()
    {
        instance.gameOverStartTime = Time.time;
        instance.gameState = GameState.GAME_OVER;
        SceneManager.LoadSceneAsync(0, LoadSceneMode.Additive);
    }

    public static void resetGame()
    {
        //Restart the game
        SceneManager.LoadScene("PlayScene");
    }
}
