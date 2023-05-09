using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveChase : MonoBehaviour
{

    private Rigidbody2D Rigidbody2D;
    public GameObject player;
    private float speed;
    private bool acelerate;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Rigidbody2D.angularVelocity = 300;

        speed = 0;
        acelerate = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(speed < 2 && acelerate)
        {
            speed += (float)0.001;
        }
        else
        {
            acelerate = false;
            speed -= (float)0.001;
            if (speed <= 0){
                acelerate = true;
            }
        }
        Debug.Log(speed);
    }

    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }
}
