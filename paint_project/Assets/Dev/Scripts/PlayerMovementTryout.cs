using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementTryout : MonoBehaviour
{
    public float moveSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPosition = transform.position;

        if (Input.GetKey(KeyCode.W)) {

            currentPosition.y +=  moveSpeed * Time.deltaTime;

        }

        if (Input.GetKey(KeyCode.A)) {

            currentPosition.x -=  moveSpeed * Time.deltaTime;
            
        }

        if (Input.GetKey(KeyCode.S)) {

            currentPosition.y -=  moveSpeed * Time.deltaTime;
            
        }

        if (Input.GetKey(KeyCode.D)) {

            currentPosition.x +=  moveSpeed * Time.deltaTime;
            
        }

        transform.position = currentPosition;
    }
}
