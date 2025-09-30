using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartButton:MonoBehaviour
{   
    [SerializeField]
    Button startButton;
    void Awake()
    {
        startButton.onClick.AddListener(()=>
        {
            OnClicked();
        });
    }
    void OnClicked()
    {
        GameManager.Instance.ActGameStart();
    }
    void GameStart()
    {
        startButton.gameObject.SetActive(false);
    }
    void GameOver()
    {
        startButton.gameObject.SetActive(true);
    }
    void Start()
    {
        GameManager.Instance.OnGameStart += GameStart;
        GameManager.Instance.OnGameOver += GameOver;
    }
    void OnDisable()
    {
        GameManager.Instance.OnGameStart -= GameStart;
        GameManager.Instance.OnGameOver -= GameOver;
    }
}