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

    private readonly string PressAnyButtonText = "Press Any Button";

    private Coroutine timerRoutine;
    private int currentRoundTime;

    private void Awake()
    {
        Player.OnPlayerDeath += GameOver;
        InputSystem.onAnyButtonPress.CallOnce(StartGame);
        timer.SetText(PressAnyButtonText);
    }

    private void StartGame(InputControl _)
    {
        isGameActive = true;
        currentRoundTime = roundTime;
        enemySpawner.BeginSpawning();

        if (timerRoutine != null)
        {
            StopCoroutine(timerRoutine);
        }

        StartCoroutine(RoundTimerRoutine());
    }

    private void GameOver()
    {
        enemySpawner.StopSpawning();
        isGameActive = false;
        InputSystem.onAnyButtonPress.CallOnce(StartGame);
        timer.SetText(PressAnyButtonText);
    }

    private IEnumerator RoundTimerRoutine()
    {
        if (timerRoutine != null)
        {
            StopCoroutine(timerRoutine);
        }

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
