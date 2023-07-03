using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class DasherScript : Enemy
{
     
    private Rigidbody2D Rigidbody2D;
    private GameObject player;
    private GameObject Dash1;
    private GameObject Dash2;
    private GameObject Dash3;
    private GameObject Dash4;
    private GameObject Dash5;
    private float speed;
    private bool acelerate;
    private float rotationModifier;
    private PaintManagerScript PaintManager;
    private AudioManagerScript audioManager;
    public GameObject deathParticles;
    public Vector3 DashPosition; 
    private float DashPoint;
    public Vector3 position;

    // Start is called before the first frame update
    void Start()
    {
        //audioSource = this.GetComponent<AudioSource>();
        var spriteRenderer = GetComponent<SpriteRenderer>();
        PaintManager = GameObject.Find("Lienzo").GetComponent<PaintManagerScript>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManagerScript>();
        Dash1 = GameObject.Find("Dash1");
        Dash2 = GameObject.Find("Dash2");
        Dash3 = GameObject.Find("Dash3");
        Dash4 = GameObject.Find("Dash4");
        Dash5 = GameObject.Find("Dash5");
        rotationModifier = 90;
        player =  GameObject.Find("Ship");
        Rigidbody2D = GetComponent<Rigidbody2D>();
        DashPosition = Dash1.transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
     
        
        if(speed <= 0)
        {   
            acelerate = false; 
            DashPoint = (int)Random.Range(0, 4);
            if(0 == DashPoint){
                DashPosition = Dash1.transform.position;
            }else if (1 == DashPoint){
                DashPosition = Dash2.transform.position;
            }else if (2 == DashPoint){
                DashPosition = Dash3.transform.position;
            }else if (3 == DashPoint){
                DashPosition = Dash4.transform.position;
            }else if (4 == DashPoint){
                DashPosition = Dash5.transform.position;
            }
            
            Invoke("MaxVelocity", 1);
        }else if(speed > 0)
        {
            speed -= (float)0.01;
        }
        ChageRotation(DashPosition);
    }
    private void MaxVelocity(){
        
       
        speed = 5;
        acelerate = true;
    }
    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, DashPosition, speed * Time.deltaTime); 
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
        if(hasSound) {
            this.audioManager.PlayenemyDieSound(0);
        }
        InstantiatePowerUp();
        Instantiate(deathParticles, this.transform.position, Quaternion.identity);
     
        gameObject.SetActive(false);
        speed = 5;
    }

    private void OnTriggerEnter2D(Collider2D other) {
       //sjdbaisbd
        if(other.CompareTag("Player"))
        {
   
            other.GetComponent<Player>().HitBullet();
            Instantiate(deathParticles, this.transform.position, Quaternion.identity);
            PaintManager.detectPaint(transform.position);
            gameObject.SetActive(false); //Este destroy realmente va a ser una llamada a la funcion de object pool
        }
    }
    private void ChageRotation(Vector3 direction)
    {
        Vector3 vectorToTarget = direction - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - rotationModifier;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * (float)5);
    }
    
}
