using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Spawner : MonoBehaviour
{
    public float offSetX;
    public float offSetY;
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

            Vector3 spawnPoint =  PickRandomSpawnPoint();
            objectPooler.SpawnFromPool(enemyToSpawn, spawnPoint, Quaternion.identity);
        }
    }

    private Vector3 PickRandomSpawnPoint(){
        int index = Random.Range(0,4);
        Vector3 spawn = transform.position;

        if (index == 0) {
            spawn.x += (offSetX*-1);
        } else if (index == 1) {
             spawn.x += (offSetX) ;
        } else if (index == 2) {
            spawn.y += (offSetY*-1);
        } else {
            spawn.y += (offSetY) ;
        }

        return spawn;


       
    }
}
