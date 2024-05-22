using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject levelsMenuUI;
    public GameObject mainMenuUI;

    private bool started = false;
    
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
        levelsMenuUI.SetActive(false);
    }

    void Update()
    {
        if(Input.anyKey && mainMenuUI.active){
            GoToLevels();
        }
    }

    // Update is called once per frame
    public void GoToLevels()
    {
        mainMenuUI.SetActive(false);
        levelsMenuUI.SetActive(true);
    }
}
