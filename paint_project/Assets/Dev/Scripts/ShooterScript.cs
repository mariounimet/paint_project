using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ShooterScript : Enemy
{
    public GameObject bullet;
    public GameObject cam;
    public GameObject player;
    private Vector3 moveTo;
    private PaintManagerScript PaintManager;

    private bool newMoveTo;
    private bool shotActive;
    private float speed;
    private float distanceToNew;
    private float rotationModifier;
    private AudioSource audioSource;
    public AudioClip shooterDieSound;
    public AudioClip shooterBulletSound;
    // Start is called before the first frame update

    public ShooterScript(GameObject c, GameObject p)
    {
        this.player = p;
        this.cam = c;
    }

    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        PaintManager = GameObject.Find("Lienzo").GetComponent<PaintManagerScript>();
        player =  GameObject.Find("Ship");
        cam = GameObject.Find("Main Camera");
        MoveToSpawnPoint();
        speed = 0;
        rotationModifier = 90;
        newMoveTo = true;
        shotActive = true;
        moveTo = new Vector3(cam.transform.position.x +  Random.Range(-2.0f, 2.0f), cam.transform.position.y +  Random.Range(-3.5f, 3.5f), 0);
    }
    void Update() {
        
    }
    void FixedUpdate() {
        if(transform.position.x == moveTo.x && transform.position.y == moveTo.y){
            if(newMoveTo)
            {
                speed = 0;
                newMoveTo = false;
                Invoke("move", 3);
                Invoke("Shoot", 1);
                Invoke("Shoot", 2);
            }
            else
            {
                ChageRotation(player.transform.position);
            }
            
        }else{
            if(Vector3.Distance(moveTo, transform.position) >= distanceToNew/2)
            {
                speed += (float)0.005;
            }
            else if(speed > 0.5)
            {
                speed -= (float)0.005;
            }
            ChageRotation(moveTo);
            transform.position = Vector2.MoveTowards(transform.position, moveTo, speed * Time.deltaTime);

        }
    }
    // Update is called once per frame
    private void ChageRotation(Vector3 direction)
    {
        Vector3 vectorToTarget = direction - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - rotationModifier;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * (float)5);
    }
    private void move()
    {
        shotActive = true;
        moveTo = new Vector3(cam.transform.position.x +  Random.Range(-2.0f, 2.0f), cam.transform.position.y +  Random.Range(-3.5f, 3.5f), 0);
        distanceToNew = Vector3.Distance(moveTo, transform.position);
        newMoveTo = true;
    }

    public override void Shoot()
    {
        if(shotActive)
        {
            this.audioSource.PlayOneShot(this.shooterBulletSound);
            GameObject b = Instantiate(bullet, transform.position, Quaternion.identity);
            b.GetComponent<BulletScript>().setDirection(transform.rotation);
        }
    }
    public override void MoveToSpawnPoint()
    {

    }
    public override void Spawn()
    {

    }
    public override void Die()
    {
        shotActive = false;
        this.audioSource.PlayOneShot(this.shooterDieSound);
        PaintManager.detectPaint(transform.position);
        gameObject.SetActive(false);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<Player>().HitBullet();
            PaintManager.detectPaint(transform.position);
            gameObject.SetActive(false); //Este destroy realmente va a ser una llamada a la funcion de object pool
        }

    }
}
