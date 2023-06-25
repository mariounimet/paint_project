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

    private bool newMoveTo;
    private float shotActive;
    private float speed;
    private float distanceToNew;
    private float rotationModifier;
    private float moveToX;
    private float moveToY;
    private AudioSource audioSource;
    public AudioClip shooterBulletSound;
    private AudioManagerScript audioManager;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManagerScript>();
        PaintManager = GameObject.Find("Lienzo").GetComponent<PaintManagerScript>();
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
        if(transform.position.x == moveTo.x && transform.position.y == moveTo.y){
            if(newMoveTo)
            {
                speed = 0;
                newMoveTo = false;
                Invoke("move", 1);
            }
            else
            {
                ChageRotation(player.transform.position);
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
            ChageRotation(moveTo);
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
        if(hasSound){
        this.audioManager.PlayenemyDieSound(1);

        }
        this.audioSource.PlayOneShot(this.shooterBulletSound);
        GameObject b1 = Instantiate(bullet, transform.position, Quaternion.identity);
        Quaternion angulob1 = transform.rotation;
        angulob1.y += (float)(0);
        transform.rotation = angulob1;
        b1.GetComponent<BulletScript>().setDirection(transform.rotation);

        GameObject b2 = Instantiate(bullet, transform.position, Quaternion.identity);
        Quaternion angulob2 = transform.rotation;
        angulob2.y += (float)(0.5);
        transform.rotation = angulob2;
        b2.GetComponent<BulletScript>().setDirection(transform.rotation);

        GameObject b3 = Instantiate(bullet, transform.position, Quaternion.identity);
        Quaternion angulog = transform.rotation;
        angulog.y += (float)(1);
        transform.rotation = angulog;
        b3.GetComponent<BulletScript>().setDirection(transform.rotation);

        GameObject b4 = Instantiate(bullet, transform.position, Quaternion.identity);
        Quaternion anguloh = transform.rotation;
        anguloh.x += (float)(1.5);
        transform.rotation = anguloh;
        b4.GetComponent<BulletScript>().setDirection(transform.rotation);

        GameObject b5 = Instantiate(bullet, transform.position, Quaternion.identity);
        Quaternion anguloi = transform.rotation;
        anguloi.x += (float)(2);
        transform.rotation = anguloi;
        b5.GetComponent<BulletScript>().setDirection(transform.rotation);

        GameObject b6 = Instantiate(bullet, transform.position, Quaternion.identity);
        Quaternion angulob6 = transform.rotation;
        angulob6.x += (float)(2);
        transform.rotation = angulob6;
        b6.GetComponent<BulletScript>().setDirection(transform.rotation);


        PaintManager.detectPaint(transform.position);
        gameObject.SetActive(false);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            shotActive = Time.time;
            other.GetComponent<Player>().HitBullet();
            PaintManager.detectPaint(transform.position);
            gameObject.SetActive(false); //Este destroy realmente va a ser una llamada a la funcion de object pool
        }

    }
}


