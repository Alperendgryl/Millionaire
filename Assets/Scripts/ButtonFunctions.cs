using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonFunctions : MonoBehaviour
{
    public void LoadScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
    public void openURL(string url)
    {
        Application.OpenURL(url);
    }

    public void ExitFromSession()
    {
        PlayerPrefs.SetInt("totalCorrect", 0);
    }
}
