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
    private float timeLastHit = 0;
    private bool isOnCooldown;
    private float timeForRun;

  
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
        isOnCooldown = false;

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
        StopCooldown();
    }

    private void StopCooldown() {
        if (((Time.time - timeLastHit) > 2)) {
            isOnCooldown = false;
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
            timeLastHit = Time.time;
            isOnCooldown = true;
            StartCoroutine(CooldownEffect());
        }
    }

    public bool VerifyCooldown() {
        bool result = false;
        if  (Time.time > nextFireTime) {
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

public IEnumerator CooldownEffect()
{
    var spriteRenderer = GetComponent<SpriteRenderer>();
    float firstEffect = 0.4f * cooldownTime;
    float secondEffect = 0.3f * cooldownTime;
    float thirdEffect = 0.2f * cooldownTime;
    float fourthEffect = 0.1f * cooldownTime;

    int runNumber = 0;

    while (runNumber < 4)
    {
        float timeForRun;
        if (runNumber == 0)
        {
            timeForRun = firstEffect;
        }
        else if (runNumber == 1)
        {
            timeForRun = secondEffect;
        }
        else if (runNumber == 2)
        {
            timeForRun = thirdEffect;
        }
        else if (runNumber == 3)
        {
            timeForRun = fourthEffect;
        }
        else
        {
            timeForRun = 0;
        }

        int i = 0;
        float timeForSleep = timeForRun / 8;
        bool whiteOn = false;

        while (i < 10)
        {
            if (whiteOn)
            {
                if (health == 3)
                {
                    spriteRenderer.color = presetColor;
                }
                else if (health == 2)
                {
                    spriteRenderer.color = life2Color;
                }
                else if (health == 1)
                {
                    spriteRenderer.color = life1Color;
                }
            }
            else
            {
                spriteRenderer.color = Color.black;
            }

            whiteOn = !whiteOn;
            i++;

            yield return new WaitForSeconds(timeForSleep);
            yield return null;
        }

        runNumber++;
    }
}
 

    // private void FixedUpdate() {
    //     rb.velocity = new Vector2(mx, my).normalized * speed;
    // }

    // private void Shoot() {
    //     Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
    // }
}
