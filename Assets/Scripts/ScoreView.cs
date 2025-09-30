using UnityEngine.UI;
using UnityEngine;
using TMPro;
using unityroom.Api;
public class ScoreView: MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI text;
    public Timer timer;
    void ShowScore()
    {
        string score = timer.gameTime.ToString("F2");
        scoreText.text = score + "s";
        UnityroomApiClient.Instance.SendScore(1, float.Parse(score), ScoreboardWriteMode.Always);
        scoreText.gameObject.SetActive(true);
        text.gameObject.SetActive(true);
    }
    void DisapperScore()
    {
        scoreText.gameObject.SetActive(false);
        text.gameObject.SetActive(false);
    }
    void Start()
    {
        GameManager.Instance.OnGameOver += ShowScore;
        GameManager.Instance.OnGameStart += DisapperScore;
    }
    void OnDisable()
    {   
        GameManager.Instance.OnGameOver -= ShowScore;
        GameManager.Instance.OnGameStart += DisapperScore;
    }

}