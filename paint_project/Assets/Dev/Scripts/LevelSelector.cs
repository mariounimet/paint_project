using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    public int imgIndex;
    public Vector3 LevelCords;
    private PaintManagerScript paintManager;
    // private Camera mainCamera;
    private ObjectPooler objectPooler;
    public GameObject camera;
    // public GameObject joystick;
    // public GameObject progressBar;
    // public GameObject text;
    // public GameObject pauseBotton;
    private AudioManagerScript AudioManager;
    private TutorialManager tutorialManager;
    // Start is called before the first frame update
    public GameObject layerUI;
    public GameObject levelsMenuUI;

    private List<List<int>>[] levelWaves = new List<List<int>>[6];
    


    void Start()
    {
        // 0 follower, 1 shooter, 2 Kamikaze, 3 tank, 4 dasher
        levelWaves[0] = new List<List<int>>(){
            new List<int>(){0,3},
            new List<int>(){1, 2},
            new List<int>(){0, 1},
            new List<int>(){0, 1, 3}};
        levelWaves[1] = new List<List<int>>(){
            new List<int>(){0,4},
            new List<int>(){0, 4, 1},
            new List<int>(){4, 2, 3},
            new List<int>(){0, 1, 2, 4}};
        levelWaves[2] = new List<List<int>>(){
            new List<int>(){2, 0},
            new List<int>(){2, 1},
            new List<int>(){2, 0, 3},
            new List<int>(){3, 0, 1, 4}};
        levelWaves[3] = new List<List<int>>(){
            new List<int>(){0,3},
            new List<int>(){3,0,1},
            new List<int>(){3,2,4},
            new List<int>(){0,1,2,3,4}};
        levelWaves[4] = new List<List<int>>(){
            new List<int>(){0,1,2},
            new List<int>(){1,2,3},
            new List<int>(){2,3,4},
            new List<int>(){0,1,2,3,4}};
        levelWaves[5] = new List<List<int>>(){
            new List<int>(){0},
            new List<int>(){1,2,3,4},
            new List<int>(){0, 1,2,3},
            new List<int>(){0, 1,2,3,4}};

        objectPooler = GameObject.Find("ObjectPooler").GetComponent<ObjectPooler>();
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

        //camera = Camera.main;

        Time.timeScale = 0f;
    }
    
    
    // void Start()
    // {
    //     layerUI.gameObject.SetActive(false);
    //     camera = Camera.main;
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
      
        //this.mainCamera.transform.position = new Vector3(this.LevelCords.x,this.LevelCords.y,this.LevelCords.z);
        this.camera.transform.position = new Vector3(this.LevelCords.x,this.LevelCords.y,this.LevelCords.z);

        objectPooler.resetStageNumber();
        objectPooler.setEnemyDictionary(levelWaves[imgIndex]);

        if (imgIndex == 0){
            tutorialManager.StartRunning();
            tutorialManager.firstPopUp();
        }else {
            Time.timeScale = 1f;
        }
        
        GameObject.Find("Spawner").GetComponent<Spawner>().canSpawnChange(true);
        AudioManager.StartFadingOutMenuMusic();
    }
}
