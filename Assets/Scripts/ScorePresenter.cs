using TMPro;
using UnityEngine;

public class ScorePresenter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    public int CurrentPoints = 0;

    private void Awake()
    {
        scoreText.SetText(CurrentPoints.ToString());
    }

    private void OnEnable()
    {
        Enemy.OnEnemyDeath += IncreaseScore;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyDeath -= IncreaseScore;
    }

    private void IncreaseScore(int points)
    {
        CurrentPoints += points;
        scoreText.SetText(CurrentPoints.ToString());
    }
}
