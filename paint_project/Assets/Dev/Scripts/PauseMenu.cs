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

    // Update is called once per frame
    //void Start(){
    //    camera = Camera.main;
    //}

    void Update()
    {
        
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        layerUI.SetActive(true);
        if (!Camera.main.GetComponent<TutorialManager>().TutorialIsActive()) {
            Time.timeScale = 1f;
        }
        GameIsPaused = false;
    }

    public void Pause()
    {
        // if (GameIsPaused) {
        //     Resume();
        // }else {
        layerUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        // }
       
    }

    public void LoadMenu()
    {
        //this.mainCamera.transform.position = uiMenuPos;
        ResetTutorial();
        pauseMenuUI.SetActive(false);
        gameOverUI.SetActive(false);
        victoryUI.SetActive(false);
        levelsMenuUI.SetActive(true);
        GameIsPaused=false;
        Spawner.canSpawn = false;
        GameObject.Find("AudioManager").GetComponent<AudioManagerScript>().menuAudioSource.volume = 0.5f;
        GameObject.Find("AudioManager").GetComponent<AudioManagerScript>().menuAudioSource.mute = false;        
    }

    public void QuitGame()
    {
        ResetTutorial();
        pauseMenuUI.SetActive(false);
        gameOverUI.SetActive(false);
        victoryUI.SetActive(false);
        mainMenuUI.SetActive(true);
        GameIsPaused = false;
        Spawner.canSpawn = false;
        GameObject.Find("AudioManager").GetComponent<AudioManagerScript>().menuAudioSource.volume = 0.5f;
        GameObject.Find("AudioManager").GetComponent<AudioManagerScript>().menuAudioSource.mute = false;   
    }

    public void ShowGameOver(){
        this.lastCameraCoords = this.camera.transform.position;
        this.camera.transform.position = uiMenuPos;
        this.layerUI.SetActive(false);
        this.gameOverUI.SetActive(true);
        Spawner.canSpawn = false;
    }

    public void ShowVictory(){
        this.camera.transform.position = uiMenuPos;
        this.layerUI.SetActive(false);
        this.victoryUI.SetActive(true);
        Spawner.canSpawn = false;
    }

    public void ContinueGame(){
        this.camera.transform.position = this.lastCameraCoords;
        pauseMenuUI.SetActive(false);
        gameOverUI.SetActive(false);
        victoryUI.SetActive(false);
        layerUI.SetActive(true);
    }

    public void ResetTutorial() {
        Camera.main.GetComponent<TutorialManager>().ResetTutorial();
    }
}
