using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{


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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playGame()
    {
        GameManager.playGame();
        SceneManager.UnloadSceneAsync(0);
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
