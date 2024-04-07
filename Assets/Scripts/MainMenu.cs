using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button buttonBack;
    [SerializeField] private Button buttonQuit;

    private void Start()
    {
        buttonBack.onClick.AddListener(OnBackClick);
        buttonQuit.onClick.AddListener(OnQuitClick);
    }

    public void OnBackClick()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void OnQuitClick()
    {
        Application.Quit();
    }
}
