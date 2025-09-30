using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public FireBase prefab;
        public int size;
        public float weight;
    }
    public List<ObjectPool.Pool> pools;
    public Dictionary<string, Queue<FireBase>> poolDictionary;
    public static ObjectPool Instance;
    void Awake()
    {   
        Instance = this;
    
        poolDictionary = new Dictionary<string, Queue<FireBase>>();
        

    }
    void Start()
    {
        foreach(Pool pool in pools)
        {
            Queue<FireBase> objectPool = new Queue<FireBase>();
            for(int i = 0; i< pool.size; i++)
            {
                CreateOne(pool, objectPool);
            }
            poolDictionary.Add(pool.tag, objectPool);
        }
    }
    public FireBase GetPoolObject(string tag, Vector3 position)
    {
        var objectToSpawn = poolDictionary[tag].Dequeue();
        objectToSpawn.gameObject.SetActive(true);
        objectToSpawn.transform.position = position;
        return objectToSpawn;
    }

    public void ReturnToPool(FireBase objectToReturn, string tag)
    {
        objectToReturn.gameObject.SetActive(false);
        poolDictionary[tag].Enqueue(objectToReturn);
    }
    public void CreateOne(Pool pool, Queue<FireBase> objectPool)
    {
        FireBase obj = Instantiate(pool.prefab);
        obj.gameObject.SetActive(false);
        objectPool.Enqueue(obj);
    }
}