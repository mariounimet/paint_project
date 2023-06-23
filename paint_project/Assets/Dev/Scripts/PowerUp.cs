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
                Debug.Log("Probando ando jajaja");
                break;

            case PowerUpsAvailable.FireRate:
                Debug.Log("Entre en el fireRate");
                // Store default fire rate
                defaultFireRate = stats.GetFireRate();
                stats.SetFireRate(newFireRate);

                yield return new WaitForSeconds(duration);

                stats.SetFireRate(defaultFireRate);

                break;

        }
        
    }
}
