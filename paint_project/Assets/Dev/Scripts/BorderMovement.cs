using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderMovement : MonoBehaviour
{
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraPosition = cam.transform.position;
        transform.position = new Vector3(cameraPosition.x, cameraPosition.y, cameraPosition.z);
    }
}
