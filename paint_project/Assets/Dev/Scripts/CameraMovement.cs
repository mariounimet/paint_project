using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float moveSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraPosition = transform.position;

        if (Input.GetKey(KeyCode.UpArrow)) {

            cameraPosition.y +=  moveSpeed * Time.deltaTime;

        }

        transform.position = cameraPosition;
    }
}
