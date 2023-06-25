using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class TankScript : Enemy
{
    
    Color presetColor = new Color(0, 255, 0);
    Color life2Color = new Color(207, 255, 0);
    Color life1Color = new Color(255, 0, 0);  
    private Rigidbody2D Rigidbody2D;
    public GameObject player;
    private float speed;
    private bool acelerate;
    private PaintManagerScript PaintManager;
    private AudioManagerScript audioManager;
    public GameObject deathParticles;
    public Vector3 playerPosition; 
    private float health;

    // Start is called before the first frame update
    void Start()
    {
        //audioSource = this.GetComponent<AudioSource>();
        health = 3;
        var spriteRenderer = GetComponent<SpriteRenderer>();
        PaintManager = GameObject.Find("Lienzo").GetComponent<PaintManagerScript>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManagerScript>();
        player =  GameObject.Find("Ship");
        Rigidbody2D = GetComponent<Rigidbody2D>();
        playerPosition = player.transform.position;
        speed = 0;
        acelerate = true;
    }

    // Update is called once per frame
    void Update()
    {

        Rigidbody2D.angularVelocity = 10;
        if(speed <= 0 && acelerate)
        {
            acelerate = false; 
            Invoke("MaxVelocity", 2);
        }
        else if(speed > 0)
        {
            speed -= (float)0.002;
        }
    }
    private void MaxVelocity(){
        speed = 1;
        acelerate = true;
        playerPosition = player.transform.position;
    }
    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerPosition, speed * Time.deltaTime); 
    }

    public override void Shoot()
    {

    }
    public override void MoveToSpawnPoint()
    {

    }
    public override void Spawn()
    {

    }
    public override void Die(bool hasSound)
    {
        PaintManager.detectPaint(transform.position);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other) {
       
        if(other.CompareTag("Player"))
        {
   
            other.GetComponent<Player>().HitBullet();
            PaintManager.detectPaint(transform.position);
            gameObject.SetActive(false); //Este destroy realmente va a ser una llamada a la funcion de object pool
        }
    }

    public void ReduceHealth() {
        health--;

        if (health == 2) {
            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.color = life2Color;
        }else if (health == 1) {
            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.color = life1Color;
        } else if (health <= 0) {
            //PlayerDie(true);
            Die(true);
            
        }
    }
}
