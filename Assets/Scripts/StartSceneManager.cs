using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSceneManager : MonoBehaviour
{
    [SerializeField] GameObject panelMain;
    [SerializeField] GameObject panelWhoFirst;
    [SerializeField] Button buttonStart;
    [SerializeField] Button buttonQuit;
    [SerializeField] Button buttonPlayerStart;
    [SerializeField] Button buttonEnemyStart;
    [SerializeField] Button buttonBack;

    private void Start()
    {
        panelMain.SetActive(true);
        panelWhoFirst.SetActive(false);
        buttonStart.onClick.AddListener(OnStartClick);
        buttonQuit.onClick.AddListener(OnQuitClick);
        buttonPlayerStart.onClick.AddListener(OnPlayerStartClick);
        buttonEnemyStart.onClick.AddListener(OnEnemyStartClick);
        buttonBack.onClick.AddListener(OnBackClick);
    }

    public void OnStartClick()
    {
        panelMain.SetActive(false);
        panelWhoFirst.SetActive(true);
    }

    public void OnQuitClick()
    {
        Application.Quit();
    }

    public void OnPlayerStartClick()
    {
        GameSettings.Instance.PlayerFirst = true;
        SceneManager.LoadScene("Main");
    }

    public void OnEnemyStartClick()
    {
        GameSettings.Instance.PlayerFirst = false;
        SceneManager.LoadScene("Main");
    }

    public void OnBackClick()
    {
        panelWhoFirst.SetActive(false);
        panelMain.SetActive(true);
    }
}
