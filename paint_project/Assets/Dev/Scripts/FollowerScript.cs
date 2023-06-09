using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class FollowerScript : Enemy
{
      
    private Rigidbody2D Rigidbody2D;
    public GameObject player;
    private float speed;
    private bool acelerate;
    private PaintManagerScript PaintManager;
    private AudioManagerScript audioManager;
    public GameObject deathParticles;

    // public List<GameObject> powerUps;

    // Start is called before the first frame update
    void Start()
    {
        //audioSource = this.GetComponent<AudioSource>();
        PaintManager = GameObject.Find("Lienzo").GetComponent<PaintManagerScript>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManagerScript>();
        player =  GameObject.Find("Ship");
        Rigidbody2D = GetComponent<Rigidbody2D>();

        speed = 0;
        acelerate = true;
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody2D.angularVelocity = 300;
        if(speed < 1 && acelerate)
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
    }

    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime); 
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
        if (hasSound) {
            this.audioManager.PlayenemyDieSound(0);
        }
       
        Instantiate(deathParticles, this.transform.position, Quaternion.identity);
        InstantiatePowerUp();
        PaintManager.detectPaint(transform.position);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other) {
       
        if(other.CompareTag("Player"))
        {
   
            other.GetComponent<Player>().HitBullet();
            Instantiate(deathParticles, this.transform.position, Quaternion.identity);
            PaintManager.detectPaint(transform.position);
            gameObject.SetActive(false); //Este destroy realmente va a ser una llamada a la funcion de object pool
        }
    }
}
