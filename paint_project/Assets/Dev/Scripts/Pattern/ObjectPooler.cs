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
    
    public int stageNumber;
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Start() {
        stageNumber = 0;
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
    }
    
    public GameObject SpawnFromPool (string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exists.");
            return null;
        }
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        if(objectToSpawn.activeInHierarchy)
        {
            poolDictionary[tag].Enqueue(objectToSpawn);
            return null;
        }

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

    public void setEnemyDictionary(List<int> poolsToUse)
    {
        poolDictionary.Clear();
        int cont = 0;
        foreach (Pool pool in pools)
        {
            if(poolsToUse[stageNumber].find(cont))
            {
                Queue<GameObject> objectPool = new Queue<GameObject>();

                for (int i = 0; i < pool.size; i++) {
                    GameObject obj = Instantiate(pool.prefab);
                    obj.SetActive(false);
                    objectPool.Enqueue(obj);
                }

                poolDictionary.Add(pool.tag, objectPool);
            }
            cont += 1;
        }
    }

    public void changeStage()
    {
        foreach (Pool pool in pools)
        {
            foreach (GameObject enemy in poolDictionary[pool.tag])
            {
                enemy.GetComponent<EnemyStatesScript>().DeSpawn();
            }
        }
    }
}
