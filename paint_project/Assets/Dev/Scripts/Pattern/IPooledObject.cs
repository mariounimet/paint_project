using UnityEngine;

public interface IPooledObject
{
    void OnObjectSpawn();
    
    void OnCollisionEnter2D(Collision2D other);
}
