using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionManager : MonoBehaviour
{
    // Start is called before the first frame 
    Camera mainCamera;
  
    void Start()
    {
        mainCamera = Camera.main;
        ActivateSmallScreenMode();

       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateSmallScreenMode(){
        string msg = "Portrait " + ((float)Screen.width/(float)Screen.height).ToString();
        var lowerRightScreen = new Vector3(Screen.width, 0, mainCamera.transform.position.y);
        var lowerRight = mainCamera.ScreenToWorldPoint(lowerRightScreen);
        // print(msg);
    }

    // public void setAspectRatio(){
    //     Screen.SetResolution(720,1280,false);
    //     float ratio = mainCamera.orthographicSize/(Screen.height/Screen.width);
    //     mainCamera.orthographicSize = ratio*(Screen.height/Screen.width);
    // }
}
