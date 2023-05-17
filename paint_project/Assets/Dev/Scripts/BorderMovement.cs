using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderMovement : MonoBehaviour
{
    private Camera cam;
    public Vector3 vectorCamObj;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        vectorCamObj = transform.position - cam.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = cam.transform.position + vectorCamObj;
    }
}
