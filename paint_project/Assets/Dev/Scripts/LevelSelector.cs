using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    public int imgIndex;
    public string img;
    public Vector3 LevelMenuCords;
    //public PaintManagerScript paintManager;
    private Camera mainCamera;
    public GameObject joystick;
    public GameObject progressBar;
    public GameObject text;
    public GameObject pauseBotton;
    private AudioManagerScript AudioManager;
    private TutorialManager tutorialManager;
    // Start is called before the first frame update
    public GameObject layerUI;
    public GameObject levelsMenuUI;
    void Start()
    {
        AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManagerScript>();
        layerUI.gameObject.SetActive(false);
        tutorialManager = Camera.main.GetComponent<TutorialManager>();
        // joystick.gameObject.SetActive(false);
        // progressBar.gameObject.SetActive(false);
        // text.gameObject.SetActive(false);
        // pauseBotton.gameObject.SetActive(false);

        mainCamera = Camera.main;

        Time.timeScale = 0f;
    }
    
    
    // void Start()
    // {
    //     layerUI.gameObject.SetActive(false);
    //     mainCamera = Camera.main;
    //     Time.timeScale = 0f;
    //     tutorialManager = Camera.main.GetComponent<TutorialManager>();
    // }
    
    void Update()
    {
        
    }

    public void lvlSelector(){
        //print("Soy un nivel ");
        //paintManager.curreentImage = imgIndex;
        levelsMenuUI.gameObject.SetActive(false);
        layerUI.gameObject.SetActive(true);
        this.mainCamera.transform.position = new Vector3(this.LevelMenuCords.x,this.LevelMenuCords.y,this.LevelMenuCords.z);
        if (imgIndex == 0){
            tutorialManager.StartRunning();
            tutorialManager.firstPopUp();
        }else {
            Time.timeScale = 1f;
        }
        AudioManager.StartFadingOutMenuMusic();
    }
}
