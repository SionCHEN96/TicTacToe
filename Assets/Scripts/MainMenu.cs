using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button buttonBack;
    [SerializeField] private Button buttonQuit;
    [SerializeField] private Button buttonRestart;

    [Header("Texts")]
    [SerializeField] private string textPlayerWin;
    [SerializeField] private string textEnemyWin;
    [SerializeField] private string textDraw;

    [Header("Settings")]
    [SerializeField] private float restartFadeInDuration = 1f;

    private TextMeshProUGUI textEndGame;

    public static MainMenu Instance { get; private set; }

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

        textEndGame = buttonRestart.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        buttonBack.onClick.AddListener(OnBackClick);
        buttonQuit.onClick.AddListener(OnQuitClick);
        buttonRestart.onClick.AddListener(OnRestartClick);

        buttonRestart.gameObject.SetActive(false);
    }

    public void OnBackClick()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void OnQuitClick()
    {
        Application.Quit();
    }

    public void OnPlayerWin()
    {
        textEndGame.text = textPlayerWin;
        buttonRestart.gameObject.SetActive(true);
        StartCoroutine(FadeInButtonRestart());
    }

    public void OnEnemyWin()
    {
        textEndGame.text = textEnemyWin;
        buttonRestart.gameObject.SetActive(true);
        StartCoroutine(FadeInButtonRestart());
    }

    public void OnDraw()
    {
        textEndGame.text = textDraw;
        buttonRestart.gameObject.SetActive(true);
        StartCoroutine(FadeInButtonRestart());
    }

    public void OnRestartClick()
    {
        BoardManager.Instance.ResetGame();
    }

    public void OnStartGame()
    {
        buttonRestart.gameObject.SetActive(false);
    }

    IEnumerator FadeInButtonRestart()
    {
        buttonRestart.interactable = false;
        float elapsedTime = 0f;
        textEndGame.color = new Color(textEndGame.color.r, textEndGame.color.g, textEndGame.color.b, 0);
        while (elapsedTime < restartFadeInDuration)
        {
            elapsedTime += Time.deltaTime;
            textEndGame.color = new Color(textEndGame.color.r, textEndGame.color.g, textEndGame.color.b, elapsedTime / restartFadeInDuration);
            yield return null;
        }

        buttonRestart.interactable = true;
    }
}
