using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.Factory
{
public class RandSpawner : MonoBehaviour
{
    //[SerializeField] private LayerMask layerToCreate;
    [SerializeField] private Vector2 offset;
    [SerializeField] Factory[] factories;
    [SerializeField] private float spawnRate = 3f;

    private Factory factory;

    private void Update()
    {
        GetProductAtRand();
    }

    private IEnumerator GetProductAtRand() 
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);

        while (factory.canSpawn) {
            yield return wait;
            
            factory = factories[Random.Range(0, factories.Length)];
            //cada producto maneja su ubicacion de partida
            factory.GetProduct(offset);
        }
    }
}
}