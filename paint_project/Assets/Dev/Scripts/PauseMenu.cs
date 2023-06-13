using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject mainMenuUI;
    public GameObject levelsMenuUI;
    public GameObject gameOverUI;
    public GameObject victoryUI;
    private Camera mainCamera;
    public GameObject layerUI;
    public Vector3 uiMenuPos;

    // Update is called once per frame
    void Start(){
        mainCamera = Camera.main;
    }

    void Update()
    {
        
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        print("trigger");
        //this.mainCamera.transform.position = uiMenuPos;
        pauseMenuUI.SetActive(false);
        gameOverUI.SetActive(false);
        victoryUI.SetActive(false);
        levelsMenuUI.SetActive(true);
        
    }

    public void QuitGame()
    {
        pauseMenuUI.SetActive(false);
        mainMenuUI.SetActive(true);
    }

    public void ShowGameOver(){
        this.mainCamera.transform.position = uiMenuPos;
        this.layerUI.SetActive(false);
        this.gameOverUI.SetActive(true);
    }

    public void ShowVictory(){
        this.mainCamera.transform.position = uiMenuPos;
        this.layerUI.SetActive(false);
        this.victoryUI.SetActive(true);
    }

    
}
