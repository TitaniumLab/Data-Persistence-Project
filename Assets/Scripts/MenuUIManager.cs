using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIManager : MonoBehaviour
{
    //set buttons in main menu
    public void StartGame()
    {
        SceneManager.LoadScene("main");
    }

    public void LoadSettingsMenu()
    {
        SceneManager.LoadScene("Settings");
    }

    public void LoadScoreMenu()
    {
        SceneManager.LoadScene("Score");
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif

    }
}
