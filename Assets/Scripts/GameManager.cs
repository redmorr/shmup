using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool isGameActive = false;
    [SerializeField][Range(1, 360)] private int roundTime = 60;
    [SerializeField] private TextMeshProUGUI timer;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private ScorePresenter scorePresenter;

    [SerializeField] private Transform startPosition;
    [SerializeField] private Player player;

    private readonly string PressAnyButtonText = "Press Any Button";

    public static event Action OnGameOver;

    private Coroutine timerRoutine;
    private int currentRoundTime;

    private void Awake()
    {

        Player.OnPlayerDeath += GameOver;
        InputSystem.onAnyButtonPress.CallOnce(StartGame);
        timer.SetText(PressAnyButtonText);
        player.gameObject.SetActive(false);
    }

    private void StartGame(InputControl _)
    {
        isGameActive = true;
        currentRoundTime = roundTime;
        enemySpawner.BeginSpawning();
        player.transform.position = startPosition.position;
        player.gameObject.SetActive(true);
        scorePresenter.LoadScores();
        scorePresenter.ResetCurrentScore();

        if (timerRoutine != null)
        {
            StopCoroutine(timerRoutine);
        }

        timerRoutine = StartCoroutine(RoundTimerRoutine());
    }

    private void GameOver()
    {
        if (timerRoutine != null)
        {
            StopCoroutine(timerRoutine);
        }
        OnGameOver?.Invoke();
        player.gameObject.SetActive(false);
        enemySpawner.StopSpawning();
        isGameActive = false;
        InputSystem.onAnyButtonPress.CallOnce(StartGame);
        timer.SetText(PressAnyButtonText);
        scorePresenter.SaveScores();
    }

    private IEnumerator RoundTimerRoutine()
    {
        timer.SetText(currentRoundTime.ToString());
        yield return new WaitForSeconds(1f);

        while (currentRoundTime > 0)
        {
            currentRoundTime--;
            timer.SetText(currentRoundTime.ToString());
            yield return new WaitForSeconds(1f);
        }

        GameOver();
    }
}
