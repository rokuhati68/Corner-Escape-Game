using UnityEngine;
using System;

public sealed class GameManager:MonoBehaviour
{
    public static GameManager Instance{get; private set;}
    public float gameTime;
    public event Action OnGameOver;
    public event Action OnGameStart;
    void Awake()
    {
        if(Instance != null && Instance != this) {Destroy(gameObject); return;}
        Instance = this;
        DontDestroyOnLoad(gameObject);
        
    }
    
    void Update()
    {
        gameTime += Time.deltaTime;
    }

    public void GameOver()
    {
        OnGameOver?.Invoke();
        
    }
    public void GameStart()
    {
        OnGameStart?.Invoke();
    }
    public void ActGameOver()
    {
        OnGameOver?.Invoke();
    }
    public void ActGameStart()
    {
        OnGameStart?.Invoke();
    }
}