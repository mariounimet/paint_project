using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatesScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject deathParticles;
    void Start()
    {

    }

    public void DeSpawn()
    {   Instantiate(deathParticles, this.transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
}
