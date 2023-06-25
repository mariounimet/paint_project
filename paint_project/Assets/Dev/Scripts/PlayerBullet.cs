using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [Range(1, 20)]
    [SerializeField] public float speed = 10f;

    [Range(1, 10)]
    [SerializeField] private float lifeTime = 1f;

    private Rigidbody2D rb;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate(){
        rb.velocity = transform.up * speed;

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // print("Choque");
        // destroyBulletPlayer();
        // if(other.CompareTag("FollowerScript")) //TODO nombre correcto
        // {
        //     destroyBulletPlayer();
        //     other.GetComponent<ShooterScript>().Die(); 
        // } 
        if(other.CompareTag("ShooterScript")) {
            destroyBulletPlayer();
            other.GetComponent<ShooterScript>().Die(true); 
            }

        if(other.CompareTag("Follower")) {
            destroyBulletPlayer();
            other.GetComponent<FollowerScript>().Die(true); 
            }
        if(other.CompareTag("Kamikaze")){
            destroyBulletPlayer();
            other.GetComponent<KamikazeScript>().Die(true); 
            }
        if(other.CompareTag("Tank")){
            destroyBulletPlayer();
            other.GetComponent<TankScript>().ReduceHealth(); 
        }
        if(other.CompareTag("Dasher")){
            destroyBulletPlayer();
            other.GetComponent<DasherScript>().Die(true); 
            }

        // if (other.CompareTag("Boundary")) {
        //     print("entro");
        //     destroyBulletPlayer();
            
        //     }

    }

    private void OnCollisionEnter2D(Collision2D other) {
   

        bool boundaryColission = (other.gameObject.name == "TopBoundary") || (other.gameObject.name == "BottomBoundary") ||(other.gameObject.name == "RightBoundary") ||(other.gameObject.name == "LeftBoundary"); 

        if (boundaryColission) {
          
            destroyBulletPlayer();
            
            }
    }

    public void destroyBulletPlayer()
    {
        Destroy(gameObject);
    }
}
