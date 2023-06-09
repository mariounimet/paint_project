using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CameraShake;


public class Player : MonoBehaviour
{
    Color presetColor = new Color(0, 255, 0);
    Color life3Color = new Color(0, 191, 0);
    Color life2Color = new Color(207, 255, 0);
    Color life1Color = new Color(255, 0, 0);
    public AudioClip shipHitSound;
    public AudioClip bulletShotSound;
    public AudioSource shootAudioSource;
    public AudioSource getHitAudioSource;
    public int health = 3;
    private float cooldownTime = 2;
    private float nextFireTime = 0;
    private float timeLastHit = 0;
    private bool isOnCooldown;
    private float timeForRun;
    private bool canTakeDamage;

    // variables bullet
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    [Range(0.1f, 3f)]
    [SerializeField] private float fireRate = 0.8f;
    private float fireTimer;
    public PauseMenu PauseMenuScript;
    public Vector3 initialPlayerCoords;
    private BlastWaveVFX blastWave;
    public float originalFireRate;


    // Start is called before the first frame update
    void Start()
    {
        setCanTakeDamage(true);
        blastWave = GetComponent<BlastWaveVFX>();

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
        if ((Time.time - timeLastHit) > 2) {
            isOnCooldown = false;
        }
    }

    private void Shoot(){
        Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
        this.shootAudioSource.PlayOneShot(this.bulletShotSound);
    }

    public float GetFireRate() 
    {
        // return this.fireRate;
        return originalFireRate;
    }

    public void SetFireRate(float fireRate)
    {
        this.fireRate = fireRate;
    }

    public void setCanTakeDamage(bool state)
    {
        this.canTakeDamage = state;
    }

    public bool getCanTakeDamage()
    {
        return this.canTakeDamage;
    }

    public void HitBullet() {
        if (VerifyCooldown() && canTakeDamage) {
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

    public void AddHealth()
    {
        if (health < 3)
        {
            health++;
            Debug.Log("Added 1 hp");
            ChangeSpriteColor(isFromHit: false);
        }
    }
    public void ReduceHealth() {
        // Shakes the screen when the player gets hit
        CameraShaker.Presets.Explosion2D(positionStrength: 10f, rotationStrength: 10f, duration: 2f);
        health--;
        ChangeSpriteColor();

    }

    public void ChangeSpriteColor(bool isFromHit = true)
    {
        if (health == 3)
        {
            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.color = presetColor;
            blastWave.createWave(presetColor);
            if (isFromHit){
                this.getHitAudioSource.PlayOneShot(this.shipHitSound);
            }
            
        }
        else if (health == 2)
        {
            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.color = life2Color;
            blastWave.createWave(life2Color);
            if (isFromHit) {
                this.getHitAudioSource.PlayOneShot(this.shipHitSound);
            }
        }
        else if (health == 1)
        {
            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.color = life1Color;
            blastWave.createWave(life1Color);
            if (isFromHit) {
            this.getHitAudioSource.PlayOneShot(this.shipHitSound);

            }
        }
        else if (health <= 0)
        {
            //PlayerDie(true);
            showGameOverDecision();
        }
    }

    public void Hit() {}

    public void resetPlayer(bool isDie){
       
        this.transform.position = initialPlayerCoords;
        GameObject.Find("ObjectPooler").GetComponent<ObjectPooler>().resetStageNumber();
        health = 3;
        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = presetColor;
        Time.timeScale = 0f;
        GameObject.Find("Lienzo").GetComponent<PaintManagerScript>().ResetCanvas();
        GameObject.Find("Lienzo").GetComponent<PaintManagerScript>().resetProgressBar();
        GameObject.Find("Lienzo").GetComponent<GridManagerScript>().resetIsPaintedMatrix();
        Camera.main.orthographicSize = 4.5f;

       
        if (!isDie) {
             GameObject.Find("AudioManager").GetComponent<AudioManagerScript>().resetMusic();
            GameObject.Find("AudioManager").GetComponent<AudioManagerScript>().playEndSound(isDie);
            this.PauseMenuScript.ShowVictory();
        } 

    }

    public void showGameOverDecision(){
        GameObject.Find("AudioManager").GetComponent<AudioManagerScript>().resetMusic();
        GameObject.Find("AudioManager").GetComponent<AudioManagerScript>().playEndSound(true);
        this.PauseMenuScript.ShowGameOver();
        Time.timeScale = 0f;
    }

    public void revivePlayer(){
        GameObject.Find("AudioManager").GetComponent<AudioManagerScript>().continueMusic();
        this.PauseMenuScript.ContinueGame();
        Time.timeScale = 1f;
        health = 3;
        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = presetColor;
    }

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
