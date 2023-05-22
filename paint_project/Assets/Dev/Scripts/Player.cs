using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Color presetColor = new Color(0, 255, 0);
    Color life2Color = new Color(207, 255, 0);
    Color life1Color = new Color(255, 0, 0);
    private int health = 3;

        // [SerializeField] private float speed = 5f;

    //Gun variables
    // [SerializeField] private GameObject bulletPrefab;
    // [SerializeField] private Transform firingPoint;
    // [Range(0.1f, 2f)]
    // [SerializeField] private float fireRate = 0.5f;

    // private Rigidbody2D rb;
    // private float mx;
    // private float my;

    // private float fireTimer;

    // private Vector2 mousePos;

  
    // variables bullet
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    [Range(0.1f, 3f)]
    [SerializeField] private float fireRate = 0.8f;
    private float fireTimer;
    // Start is called before the first frame update
    void Start()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = presetColor;

        // rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fireTimer <= 0f){
            Shoot();
            fireTimer = fireRate;
        } else {
            fireTimer -= Time.deltaTime;
        }
        
    }
    private void Shoot(){
        Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
    }
    public void HitBullet() {
        health--;
        if (health == 2) {
            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.color = life2Color;
        } else if (health == 1) {
            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.color = life1Color;
        } else if (health <= 0) {
            Application.Quit();
            Debug.Log("EndGame");
        }
    }

    public void HitShip() {
        Debug.Log("EndGame");
        Application.Quit();
    }



 

    // private void FixedUpdate() {
    //     rb.velocity = new Vector2(mx, my).normalized * speed;
    // }

    // private void Shoot() {
    //     Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
    // }
}
