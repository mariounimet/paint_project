using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class KamikazeScript : Enemy
{
    public GameObject bullet;
    public GameObject cam;
    public GameObject player;
    private Vector3 moveTo;
    private PaintManagerScript PaintManager;
    private Rigidbody2D Rigidbody2D;
    private bool newMoveTo;
    private float shotActive;
    private float speed;
    private float distanceToNew;
    private float rotationModifier;
    private float moveToX;
    private float moveToY;
    private AudioSource audioSource;
    public GameObject deathParticles;
    public AudioClip shooterBulletSound;
    private AudioManagerScript audioManager;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManagerScript>();
        PaintManager = GameObject.Find("Lienzo").GetComponent<PaintManagerScript>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        player =  GameObject.Find("Ship");
        cam = GameObject.Find("Main Camera");
        MoveToSpawnPoint();
        speed = 0;
        rotationModifier = 90;
        newMoveTo = true;
        move();
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody2D.angularVelocity = 500;
        if(transform.position.x == moveTo.x && transform.position.y == moveTo.y){
            if(newMoveTo)
            {
                speed = 0;
                newMoveTo = false;
                Invoke("move", 1);
            }
            else
            {
                //ChageRotation(player.transform.position);
            }
            
        }else if (!PauseMenu.GameIsPaused){
            moveTo = new Vector3(cam.transform.position.x +  moveToX, cam.transform.position.y +  moveToY, 0);
            distanceToNew = Vector3.Distance(moveTo, transform.position);
            if(Vector3.Distance(moveTo, transform.position) >= distanceToNew/2)
            {
                speed += (float)0.002;
            }
            else if(speed > 0.5)
            {
                speed -= (float)0.002;
            }
            //ChageRotation(moveTo);
            transform.position = Vector2.MoveTowards(transform.position, moveTo, speed * Time.deltaTime);
        }
    }
    private void ChageRotation(Vector3 direction)
    {
        Vector3 vectorToTarget = direction - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - rotationModifier;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * (float)5);
    }
    private void move()
    {
        moveToX = Random.Range(-2.0f, 2.0f);
        moveToY = Random.Range(-3.5f, 3.5f);
        moveTo = new Vector3(cam.transform.position.x + moveToX, cam.transform.position.y + moveToY, 0);
        distanceToNew = Vector3.Distance(moveTo, transform.position);
        newMoveTo = true;
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
        shotActive = Time.time;
        transform.rotation = Quaternion.identity;
        InstantiatePowerUp();
        Instantiate(deathParticles, this.transform.position, Quaternion.identity);
        if(hasSound){
        this.audioManager.PlayenemyDieSound(2);

        }
        // this.audioSource.PlayOneShot(this.shooterBulletSound);
        
        GameObject b1 = Instantiate(bullet, transform.position, Quaternion.identity);
        GameObject b2 = Instantiate(bullet, transform.position, Quaternion.identity);
        GameObject b3 = Instantiate(bullet, transform.position, Quaternion.identity);
        GameObject b4 = Instantiate(bullet, transform.position, Quaternion.identity);
        GameObject b5 = Instantiate(bullet, transform.position, Quaternion.identity);
        GameObject b6 = Instantiate(bullet, transform.position, Quaternion.identity);

        Quaternion angulob1 = transform.rotation;
        Quaternion angulob2 = transform.rotation;
        Quaternion angulob3 = transform.rotation;
        Quaternion angulob4 = transform.rotation;
        Quaternion angulob5 = transform.rotation;
        Quaternion angulob6 = transform.rotation;

        angulob1.z += (float)(0);
        b1.GetComponent<BulletScript>().setDirection(angulob1);

        angulob2.z += (float)(0.5);
        b2.GetComponent<BulletScript>().setDirection(angulob2);

        angulob3.z += (float)(-0.5);
        b3.GetComponent<BulletScript>().setDirection(angulob3);

        
        angulob4.z += (float)(150);
        b4.GetComponent<BulletScript>().setDirection(angulob4);
        
        angulob5.z += (float)(1.5);
        b5.GetComponent<BulletScript>().setDirection(angulob5);

        angulob6.z += (float)(-1.5);
        b6.GetComponent<BulletScript>().setDirection(angulob6);


        PaintManager.detectPaint(transform.position);
        gameObject.SetActive(false);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            shotActive = Time.time;
            other.GetComponent<Player>().HitBullet();
            Instantiate(deathParticles, this.transform.position, Quaternion.identity);
            PaintManager.detectPaint(transform.position);
            gameObject.SetActive(false); //Este destroy realmente va a ser una llamada a la funcion de object pool
        }

    }
}


