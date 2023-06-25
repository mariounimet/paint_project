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
    private List<List<int>> levelWaves;
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

    public void setEnemyDictionary(List<List<int>> waves)
    {
        levelWaves = waves;
        poolDictionary.Clear();
        List<int> wave = waves[stageNumber];
        int cont = 0;
        foreach (Pool pool in pools)
        {
            if(wave.Contains(cont))
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
        if(stageNumber < 3)
        {
            stageNumber += 1;
        }
        else{
            stageNumber = 0;
        }
            foreach (Pool pool in pools)
            {
                if(poolDictionary.ContainsKey(pool.tag))
                {
                    foreach (GameObject enemy in poolDictionary[pool.tag])
                    {
                        enemy.GetComponent<EnemyStatesScript>().DeSpawn();
                    }
                }
            }
        setEnemyDictionary(levelWaves);
    }

    public void resetStageNumber()
    {
    foreach (Pool pool in pools)
        {
            if(poolDictionary.ContainsKey(pool.tag))
            {
                foreach (GameObject enemy in poolDictionary[pool.tag])
                {
                    enemy.GetComponent<EnemyStatesScript>().DeSpawn();
                }
            }
        }
        stageNumber = 0;
    }
}
