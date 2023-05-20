using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    ObjectPooler objectPooler;
    [SerializeField] private float spawnRate = 1f;

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }

    void FixedUpdate () {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);

        objectPooler.SpawnFromPool("Enemy", transform.position, Quaternion.identity);
    }
}
