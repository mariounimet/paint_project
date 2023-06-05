using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Color presetColor = new Color(0, 255, 0);
    Color life2Color = new Color(207, 255, 0);
    Color life1Color = new Color(255, 0, 0);
    public AudioClip shipHitSound;
    public AudioClip bulletShotSound;
    private AudioSource audioSource;
    private int health = 3;
    private float cooldownTime = 2;
    private float nextFireTime = 0;

  
    // variables bullet
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    [Range(0.1f, 3f)]
    [SerializeField] private float fireRate = 0.8f;
    private float fireTimer;

    // Start is called before the first frame update
    void Start()
    {
        this.audioSource = this.GetComponent<AudioSource>();
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
        this.audioSource.PlayOneShot(this.bulletShotSound);
    }

    public void HitBullet() {
        if (VerifyCooldown()) {
            ReduceHealth();
            nextFireTime = Time.time + cooldownTime;
        }
    }

    public bool VerifyCooldown() {
        bool result = false;
        if  (Time.time > nextFireTime) {
            nextFireTime = Time.time + cooldownTime;
            result = true;
        }
        return result;
    }

    public void ReduceHealth() {
        health--;
        this.audioSource.PlayOneShot(this.shipHitSound);
        if (health == 2) {
            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.color = life2Color;
        } else if (health == 1) {
            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.color = life1Color;
        } else if (health <= 0) {
            // Application.Quit();
        }
    }

    public void HitShip() {
        // Application.Quit();
    }

        public void Hit() {}


 

    // private void FixedUpdate() {
    //     rb.velocity = new Vector2(mx, my).normalized * speed;
    // }

    // private void Shoot() {
    //     Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
    // }
}
