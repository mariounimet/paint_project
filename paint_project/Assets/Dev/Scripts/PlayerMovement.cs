using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float maxSpeed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // GetAxis nos da un FLOAT desde -1.0 hasta 1.0
        Vector3 pos = transform.position;

        pos.y += Input.GetAxis("Vertical") * Time.deltaTime * maxSpeed;

        transform.position = pos;
    }
}
