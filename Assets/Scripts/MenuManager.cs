using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject creditsPanel;

    private static MenuManager instance;
    // Start is called before the first frame update
    void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(instance.gameObject);
        }
        instance = this;

        //Check to make sure game scene is loaded
        Scene playScene = SceneManager.GetSceneByName("PlayScene");
        if (!playScene.isLoaded)
        {
            SceneManager.LoadScene("PlayScene", LoadSceneMode.Additive);
        }
        pauseGame(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void pauseGame(bool pause)
    {
        Time.timeScale = (pause) ? 0 : 1;
        foreach (MoonController moon in FindObjectsOfType<MoonController>())
        {
            moon.enabled = !pause;
        }
    }

    public void playGame()
    {
        pauseGame(false);
        GameManager.playGame();
        SceneManager.UnloadSceneAsync(0);
    }

    public void toggleCredits()
    {
        creditsPanel.SetActive(!creditsPanel.activeSelf);
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
