using UnityEngine; 
using System.Collections.Generic; 
using Pool = ObjectPool.Pool;

public class SpawnerManager : MonoBehaviour 
{ 
    public float totalWeight;
    public Transform[] spawner;
    [SerializeField]
    public float spawnTime;
    public float cumultiveTime;
    [SerializeField]
    Transform player;
    public bool isGame = false;
    void Start()
    {
        GameManager.Instance.OnGameOver += GameOver;
        GameManager.Instance.OnGameStart += GameReady;
    }
    void GameReady()
    {
        MakeWeight();
        isGame = true;
    }
    public void MakeWeight()
    {
        
        int cnt = 0;
        foreach (Pool pool in ObjectPool.Instance.pools)
        {
            totalWeight += pool.weight;
            cnt +=1;
        }
        
    }
    public Pool ChooseFire()
    {
        float randomValue = Random.Range(0f, totalWeight);
        float cumulativeWeight = 0f;
        foreach(Pool pool in ObjectPool.Instance.pools)
        {
            cumulativeWeight += pool.weight;
            if(randomValue < cumulativeWeight)
            {
               
                return pool;
            }
        }
        return null;
    }
    public Vector3 ChooseSpawn()
    {
        int spawnCnt = spawner.Length;
        var spawnPoint = Random.Range(0, spawnCnt);
        return spawner[spawnPoint].transform.position;

    }
    public Vector3 CalcLaunch(Vector3 position)
    {
        Vector3 var = player.position - position;
        return var;
}
    public void FireLaunch()
    {  
        Pool pool = ChooseFire();
        string tag = pool.tag;
        Vector3 position = ChooseSpawn();
        Vector3 var = CalcLaunch(position);
        var objectSpawn = ObjectPool.Instance.GetPoolObject(tag, position);
        objectSpawn.Launch(var);
    }
    void Update()
    {
        if(isGame)
        {
            cumultiveTime += Time.deltaTime;
            if (cumultiveTime > spawnTime)
                {
                    FireLaunch();
                    cumultiveTime = 0;
                }
        }
    }
    void GameOver()
    {
        totalWeight = 0;
        isGame = false;
        
    }
    void OnDisable()
    {
        GameManager.Instance.OnGameOver -= GameOver;
        GameManager.Instance.OnGameStart -= GameReady;

    }
}