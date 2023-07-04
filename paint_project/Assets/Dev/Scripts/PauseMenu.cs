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
    public GameObject camera;
    public GameObject layerUI;
    public Vector3 uiMenuPos;
    private Vector3 lastCameraCoords;

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && Time.timeScale == 1f)
        {
            this.Pause();
        }        
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        layerUI.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        levelsMenuUI.SetActive(false);
        layerUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        pauseMenuUI.SetActive(false);
        gameOverUI.SetActive(false);
        victoryUI.SetActive(false);
        levelsMenuUI.SetActive(true);
        GameIsPaused=false;
        GameObject.Find("AudioManager").GetComponent<AudioManagerScript>().menuAudioSource.volume = 0.5f;
        GameObject.Find("AudioManager").GetComponent<AudioManagerScript>().menuAudioSource.mute = false;        
    }

    public void QuitGame()
    {
        pauseMenuUI.SetActive(false);
        gameOverUI.SetActive(false);
        victoryUI.SetActive(false);
        mainMenuUI.SetActive(true);
        GameIsPaused = false;
        GameObject.Find("AudioManager").GetComponent<AudioManagerScript>().menuAudioSource.volume = 0.5f;
        GameObject.Find("AudioManager").GetComponent<AudioManagerScript>().menuAudioSource.mute = false;   
    }

    public void ShowGameOver(){
        this.lastCameraCoords = this.camera.transform.position;
        this.camera.transform.position = uiMenuPos;
        this.layerUI.SetActive(false);
        this.gameOverUI.SetActive(true);
    }

    public void ShowVictory(){
        this.camera.transform.position = uiMenuPos;
        this.layerUI.SetActive(false);
        this.victoryUI.SetActive(true);
    }

    public void ContinueGame(){
        this.camera.transform.position = this.lastCameraCoords;
        pauseMenuUI.SetActive(false);
        gameOverUI.SetActive(false);
        victoryUI.SetActive(false);
        layerUI.SetActive(true);
    }

    
}
