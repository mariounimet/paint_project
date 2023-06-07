using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    //Object Poll
    [System.Serializable]
    public class Pool
    {
        public string tag;
        [SerializeField] public GameObject prefab;
        public int size;
    }

    #region Singleton

    public static ObjectPooler Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion
    
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Start() {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++) {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
        Invoke("PauseEnemys", 10);
        Invoke("ResumeEnemys", 20);
    }
    
    public GameObject SpawnFromPool (string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exists.");
            return null;
        }
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>();

        if (pooledObj != null)
        {
            pooledObj.OnObjectSpawn();
        } 
        
        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

    public void PauseEnemys()
    {
        foreach (Pool pool in pools)
        {
            // Debug.Log(pool.tag);
            // Debug.Log(poolDictionary[pool.tag].Dequeue());

            foreach (GameObject enemy in poolDictionary[pool.tag])
            {
                enemy.GetComponent<EnemyStatesScript>().pause();
            }
        }
    }
    public void ResumeEnemys()
    {
        foreach (Pool pool in pools)
        {
            // Debug.Log(pool.tag);
            // Debug.Log(poolDictionary[pool.tag].Dequeue());

            foreach (GameObject enemy in poolDictionary[pool.tag])
            {
                enemy.GetComponent<EnemyStatesScript>().resume();
            }
        }
    }

}
