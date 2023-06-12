using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    public int imgIndex;
    public string img;
    public Vector3 LevelMenuCords;
    public PaintManagerScript paintManager;
    private Camera mainCamera;
    public GameObject joystick;
    public GameObject progressBar;
    public GameObject text;
    public GameObject pauseBotton;
    // Start is called before the first frame update
    void Start()
    {
        joystick.gameObject.SetActive(false);
        progressBar.gameObject.SetActive(false);
        text.gameObject.SetActive(false);
        pauseBotton.gameObject.SetActive(false);
        mainCamera = Camera.main;
        Time.timeScale = 0f;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void lvlSelector(){
        print("Soy un nivel ");
        paintManager.curreentImage = imgIndex;
        joystick.gameObject.SetActive(true);
        progressBar.gameObject.SetActive(true);
        text.gameObject.SetActive(true);
        pauseBotton.gameObject.SetActive(true);
        this.mainCamera.transform.position = new Vector3(this.LevelMenuCords.x,this.LevelMenuCords.y,this.LevelMenuCords.z);
        Time.timeScale = 1f;
    }
}
