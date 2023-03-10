using TMPro;
using UnityEngine;

public class ScorePresenter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI lastScoreText;

    private readonly string HighScoreKey = "HighScore";
    private readonly string LastScoreKey = "LastScore";

    public int CurrentPoints = 0;
    public int HighScore;
    public int LastScore;

    private void OnEnable()
    {
        scoreText.SetText(CurrentPoints.ToString());
        LoadScores();
        Enemy.OnEnemyDeath += IncreaseScore;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyDeath -= IncreaseScore;
        SaveScores();
    }

    public void SaveScores()
    {
        if (CurrentPoints > HighScore)
        {
            PlayerPrefs.SetInt(HighScoreKey, CurrentPoints);
        }

        PlayerPrefs.SetInt(LastScoreKey, CurrentPoints);
        PlayerPrefs.Save();
    }

    public void LoadScores()
    {
        HighScore = PlayerPrefs.GetInt(HighScoreKey);
        highScoreText.SetText(HighScore.ToString());

        LastScore = PlayerPrefs.GetInt(LastScoreKey);
        lastScoreText.SetText(LastScore.ToString());
    }

    private void IncreaseScore(int points)
    {
        CurrentPoints += points;
        scoreText.SetText(CurrentPoints.ToString());
    }

    public void ResetCurrentScore()
    {
        CurrentPoints = 0;
        scoreText.SetText(CurrentPoints.ToString());
    }
}
