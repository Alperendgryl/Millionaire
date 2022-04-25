using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameOverScene : MonoBehaviour
{
    public TMP_Text coin_TXT;
    public TMP_Text score_TXT;
    public TMP_Text total_Correct_TXT;
    public TMP_Text lvl_TXT;

    private void Start()
    {
        gameOverScene();
    }

    private void gameOverScene()
    {
        score_TXT.text = (PlayerPrefs.GetInt("totalCorrect") * 25).ToString() + " PUAN"; // Xp per question = 25 
        coin_TXT.text = (PlayerPrefs.GetInt("totalCorrect") * 10).ToString() + " JETON"; // Coin per question = 10;
        total_Correct_TXT.text = PlayerPrefs.GetInt("totalCorrect").ToString() + " DOGRU";

        lvl_TXT.text = PlayerPrefs.GetInt("LVL").ToString();

        //PlayerPrefs.SetInt("current_XP", (PlayerPrefs.GetInt("current_XP") + PlayerPrefs.GetInt("totalCorrect") * 25));



        PlayerPrefs.SetInt("totalCorrect", 0);
    }
}
