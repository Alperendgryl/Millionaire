using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Scoreboard : MonoBehaviour
{

    public TMP_Text[] names;
    public TMP_Text[] scoresTXT;
    private int[] ID;
    private int[] scores;
    

    //public TMP_Text namess;
    //public TMP_Text scoretxt;
    //private int ýd;
    //private int score;

    private void Start()
    {
        ID = new int[names.Length];
        scores = new int[names.Length];
        RandomScoreboard();
    }

    public void RandomScoreboard()
    {
        for (int i = 0; i < names.Length; i++)
        {
            ID[i] = Random.Range(1,100000);
            names[i].text = "Kullanýcý" + ID[i];

            scores[i] = Random.Range(1, 100000);
            scoresTXT[i].text = scores[i].ToString();
        }
    }
}
