using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.Factory
{
public class RandSpawner : MonoBehaviour
{
    //[SerializeField] private LayerMask layerToCreate;
    [SerializeField] private Vector3 offset;
    [SerializeField] Factory[] factories;
    [SerializeField] private float spawnRate = 1f;

    private bool canSpawn;
    private Factory factory;

    private void Update()
    {
        GetProductAtRand();
    }

    private void GetProductAtRand() 
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);

        while (canSpawn) {
            yield return wait;
            
            int rand = Random.Range(0, enemyPrefabs.Length);
            GameObject enemyToSpawn = enemyPrefabs[rand];

            Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
        }
    }
}
}