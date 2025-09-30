using UnityEngine;

public class Timer:MonoBehaviour
{
    public float gameTime;

    void Update()
    {
        gameTime += Time.deltaTime;
    }
    void Start()
    {
        TimerReset();
        GameManager.Instance.OnGameStart += TimerReset;
    }
    void TimerReset()
    {
        gameTime = 0;
    }
    void OnDisable()
    {
        GameManager.Instance.OnGameStart -= Start;
    }
}