using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class Enemy : MonoBehaviour
{
    public abstract void Shoot();
    public abstract void MoveToSpawnPoint();
    public abstract void Spawn();
    public abstract void Die(bool hasSound);
}
