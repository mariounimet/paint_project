using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Spawner : MonoBehaviour
{
    ObjectPooler objectPooler;
    [SerializeField] private float spawnRate = 3f;
    [SerializeField] private bool canSpawn = true;

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
        StartCoroutine(Factory());
    }

    private IEnumerator Factory () {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);
        
        while (canSpawn) {
            yield return wait;
            
            int index = Random.Range(0, objectPooler.poolDictionary.Count);
            string enemyToSpawn = objectPooler.poolDictionary.ElementAt(index).Key;

            objectPooler.SpawnFromPool(enemyToSpawn, transform.position, Quaternion.identity);
        }
    }
}
