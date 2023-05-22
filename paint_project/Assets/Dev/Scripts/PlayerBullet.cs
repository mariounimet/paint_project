using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [Range(1, 10)]
    [SerializeField] private float speed = 10f;

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
        if(other.CompareTag("ShooterScript"))
        {
            destroyBulletPlayer();
            other.GetComponent<ShooterScript>().Die(); 
        } else if (other.CompareTag("Boundary")) {
            destroyBulletPlayer();
        }

    }

    public void destroyBulletPlayer()
    {
        Destroy(gameObject);
    }
}
