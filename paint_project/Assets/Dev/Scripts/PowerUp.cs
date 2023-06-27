using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpsAvailable
{
    HealthBoost,
    Invulnerable,
    FireRate
}

public class PowerUp : MonoBehaviour
{
    public PowerUpsAvailable powerUp;
    public float newFireRate = 0.2f;
    public float duration = 2f;
    private float defaultFireRate = 0f;
    private AudioManagerScript audioManager;

    private void Start() {
        this.audioManager = GameObject.Find("AudioManager").GetComponent<AudioManagerScript>();
    }
    void OnTriggerEnter2D (Collider2D other)
    {
        // Check if the other collider is the player ship
        if (other.CompareTag("Player"))
        {
            StartCoroutine (Pickup(other));
        }
    }

    IEnumerator Pickup(Collider2D player)
    {
        // Store all the player settings and properties in a "Player" component
        this.audioManager.PlayenemyDieSound(3);
        Player stats = player.GetComponent<Player>();
        switch (powerUp)
        {
            case PowerUpsAvailable.HealthBoost:

                // Adds 1 HP
                stats.AddHealth();

                //TODO: Add effect onCollide
                Destroy(gameObject);
                break;

            case PowerUpsAvailable.Invulnerable:
                // First we "hide" the powerup, after the effect it gets destroyed
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<CircleCollider2D>().enabled = false;

                player.GetComponent<CircleCollider2D>().enabled = false; // Disables player collider so it's "invulnerable"
                yield return StartCoroutine(stats.CooldownEffect()); // Applies effect of invulnerable, and the yield is so it awaits it
                player.GetComponent<CircleCollider2D>().enabled = true; // Enables the collider once again
                Destroy(gameObject);
                break;

            case PowerUpsAvailable.FireRate:
                // First we "hide" the powerup, after the effect it gets destroyed
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<CircleCollider2D>().enabled = false;

                // Store default fire rate and apply new Fire Rate to the player
                defaultFireRate = stats.GetFireRate();
                stats.SetFireRate(newFireRate);

                // Wait for effect to happen and then restore normal fire rate
                yield return new WaitForSeconds(duration);
                stats.SetFireRate(defaultFireRate);

                //Destroy the powerup instantiation
                Destroy(gameObject);
                break;

        }
        
    }
}
