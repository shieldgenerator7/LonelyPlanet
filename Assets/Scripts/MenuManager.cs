using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
