using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;

public class ScoreCounter : MonoBehaviour
{
    public static ScoreCounter Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI playerScoreText;
    [SerializeField] private TextMeshProUGUI drawScoreText;
    [SerializeField] private TextMeshProUGUI enemyScoreText;

    private int playerScore;
    private int drawScore;
    private int enemyScore;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        InitScore();
    }

    private void InitScore()
    {
        playerScore = 0;
        drawScore = 0;
        enemyScore = 0;

        playerScoreText.text = playerScore.ToString();
        drawScoreText.text = drawScore.ToString();
        enemyScoreText.text = enemyScore.ToString();
    }

    public void IncreasePlayerScore()
    {
        playerScore++;
        playerScoreText.text = playerScore.ToString();
    }

    public void IncreaseDrawScore()
    {
        drawScore++;
        drawScoreText.text = drawScore.ToString();
    }

    public void IncreaseEnemyScore()
    {
        enemyScore++;
        enemyScoreText.text = enemyScore.ToString();
    }
}
