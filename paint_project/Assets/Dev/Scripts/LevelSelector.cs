using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    public int level;
    public string img;
    public Vector3 LevelMenuCords;
    private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void lvlSelector(){
        print("Soy un nivel "+level);
        this.mainCamera.transform.position = new Vector3(this.LevelMenuCords.x,this.LevelMenuCords.y,this.LevelMenuCords.z);
    }
}
