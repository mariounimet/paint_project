using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

abstract class Enemy : MonoBehaviour
{
    public List<GameObject> powerUpPrefabs;
    public abstract void Shoot();
    public abstract void MoveToSpawnPoint();
    public abstract void Spawn();
    public abstract void Die(bool hasSound);

    public void InstantiatePowerUp()
    {
        if (hasPowerUp())
        {
            System.Random rand = new System.Random();
            var n = Convert.ToInt32(rand.Next(0, 3));
            Instantiate(powerUpPrefabs[n], this.transform.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("no");
        }
    }

    public bool hasPowerUp()
    {
        System.Random rand = new System.Random();
        int chances = Convert.ToInt32(rand.Next(0, 101));
        Debug.Log(chances);
        return chances < 10;
    }
}
