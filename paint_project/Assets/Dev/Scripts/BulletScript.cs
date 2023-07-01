using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public float speed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
        if(!Spawner.canSpawn)
        {
            destroyBullet();
        }
    }

    public void setDirection(Quaternion rot)
    {
        transform.rotation = rot;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            destroyBullet();
            other.GetComponent<Player>().HitBullet(); 
        } 
// else if (other.CompareTag("Boundary")) {
//             destroyBullet();
//         }
    }

     private void OnCollisionEnter2D(Collision2D other) {
   

        bool boundaryColission = (other.gameObject.name == "TopBoundary") || (other.gameObject.name == "BottomBoundary") ||(other.gameObject.name == "RightBoundary") ||(other.gameObject.name == "LeftBoundary"); 

        if (boundaryColission) {
          
            destroyBullet();  
            }
    }

    public void destroyBullet()
    {
        Destroy(gameObject);
    }
}
