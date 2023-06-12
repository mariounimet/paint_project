using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    //public int imgIndex;
    //public string img;
    public Vector3 LevelMenuCords;
    //public PaintManagerScript paintManager;
    private Camera mainCamera;
    public GameObject layerUI;
    public GameObject levelsMenuUI;
    
    void Start()
    {
        layerUI.gameObject.SetActive(false);
        mainCamera = Camera.main;
    }
    
    void Update()
    {
        
    }

    public void lvlSelector(){
        //print("Soy un nivel ");
        //paintManager.curreentImage = imgIndex;
        levelsMenuUI.gameObject.SetActive(false);
        layerUI.gameObject.SetActive(true);
        this.mainCamera.transform.position = new Vector3(this.LevelMenuCords.x,this.LevelMenuCords.y,this.LevelMenuCords.z);
        Time.timeScale = 1f;
    }
}
