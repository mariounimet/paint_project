using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveGrid : MonoBehaviour, IPooledObject
{
    public GameObject cam;
    public GameObject player;
    private Vector3 moveTo;

    private bool newMoveTo;
    private float speed;
    private float distanceToNew;
    private float rotationModifier;

    // Start is called before the first frame update
    public void OnObjectSpawn()
    {
        speed = 0;
        rotationModifier = 90;
        newMoveTo = true;
        moveTo = new Vector3(cam.transform.position.x +  Random.Range(-2.0f, 2.0f), cam.transform.position.y +  Random.Range(-3.5f, 3.5f), 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x == moveTo.x && transform.position.y == moveTo.y){
            if(newMoveTo)
            {
                speed = 0;
                newMoveTo = false;
                Invoke("move", 3);
            }
            else
            {
                Vector3 vectorToTarget = player.transform.position - transform.position;
                float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - rotationModifier;
                Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * (float)2);
            }
            
        }else{
            if(Vector3.Distance(moveTo, transform.position) >= distanceToNew/2)
            {
                speed += (float)0.001;
            }
            else if(speed > 0.2)
            {
                speed -= (float)0.001;
            }
            Vector3 vectorToTarget = moveTo - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - rotationModifier;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * (float)2);
            transform.position = Vector2.MoveTowards(transform.position, moveTo, speed * Time.deltaTime);
        }
    }

    void move()
    {
        moveTo = new Vector3(cam.transform.position.x +  Random.Range(-2.0f, 2.0f), cam.transform.position.y +  Random.Range(-3.5f, 3.5f), 0);
        distanceToNew = Vector3.Distance(moveTo, transform.position);
        newMoveTo = true;
    }

    void delay()
    {
        // print("aaaa");
    }

    public void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            Destroy(other.gameObject);
            player = null;
        } else if (other.gameObject.CompareTag("Bullet")) {
            Destroy(other.gameObject);
            gameObject.SetActive(false);
        }
    }
}