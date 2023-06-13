using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    public int imgIndex;
    public Vector3 LevelCords;
    private PaintManagerScript paintManager;
    private Camera mainCamera;
    // public GameObject joystick;
    // public GameObject progressBar;
    // public GameObject text;
    // public GameObject pauseBotton;
    private AudioManagerScript AudioManager;
    private TutorialManager tutorialManager;
    // Start is called before the first frame update
    public GameObject layerUI;
    public GameObject levelsMenuUI;
    void Start()
    {
        AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManagerScript>();
        this.paintManager = GameObject.Find("Lienzo").GetComponent<PaintManagerScript>();
        if (layerUI) {
            layerUI.gameObject.SetActive(false);
        }
       
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
        if(paintManager) {
        paintManager.currentImage = imgIndex;
        paintManager.ResetCanvas();

        }
        //this.paintManager.currentImage = imgIndex;
        if(levelsMenuUI) {
            levelsMenuUI.gameObject.SetActive(false);
           
        }
        if(layerUI){
            layerUI.gameObject.SetActive(true);
        }
        
        this.mainCamera.transform.position = new Vector3(this.LevelCords.x,this.LevelCords.y,this.LevelCords.z);
        if (imgIndex == 0){
            tutorialManager.StartRunning();
            tutorialManager.firstPopUp();
        }else {
            Time.timeScale = 1f;
        }
        AudioManager.StartFadingOutMenuMusic();
    }
}
