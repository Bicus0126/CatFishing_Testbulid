using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class objectPool{
        public string tag;
        public GameObject prefab;
        public int size = 10;
    }

    #region Singleton
    public static ObjectPooler Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion
    
    public List<objectPool> poolList;
    public Dictionary<string, Queue<GameObject>> poolDict;
    
    void Start()
    {
        poolDict = new Dictionary<string, Queue<GameObject>>();

        foreach (objectPool pool in poolList)
        {
            Queue<GameObject> objPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, GameObject.Find("Blocks").transform);
                obj.SetActive(false);
                objPool.Enqueue(obj);
            }

            poolDict.Add(pool.tag, objPool);
        }
    }

    int SpawnedObj = 0;
    public GameObject SpawnFromPool (string tag, Vector3 position, Quaternion rotation)
    {
        if(!poolDict.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }
        
        if(SpawnedObj >= poolList.Find(x => x.tag == tag).size)
        {
            Debug.LogWarning("Pool with tag " + tag + " depleted.");
            return null;
        }
        SpawnedObj += 1;
        GameObject objectToSpawn = poolDict[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>();
        //calling OnObjectSpawn as spawn start()
        if(pooledObj != null)
        {
            pooledObj.OnObjectSpawn();
        }

        poolDict[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}
