using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelValues : MonoBehaviour
{
    [Header("Daily Health")]
    public float msToWait; // 86400000f
    private ulong lastHeartCollected;
    public TMP_Text timer;
    public TMP_Text totalHeartTXT;

    public bool fullHeart = false;

    [Header("JokersTXT")]
    public TMP_Text half_TXT;
    public TMP_Text next_TXT;
    public TMP_Text time_TXT;

    [Header("OtherValues")]
    public TMP_Text Coin_TXT;

    [Header("LevelBar")]
    private float max_XP = 250f;
    private float current_XP = 1f;
    private float player_Level = 1f;

    public TMP_Text current_max_TXT;
    public TMP_Text player_Level_TXT;

    public Image XP_bar;

    void Start()
    {
        try
        {
            lastHeartCollected = ulong.Parse(PlayerPrefs.GetString("lastHeartCollected"));
        }
        catch (Exception)
        {
            Debug.Log("Parse Error");
        }
    }

    void Update()
    {
        IsHearthReady();
        toString();
    }

    #region LevelBar
    private void xp()
    {
        XP_bar.fillAmount += (current_XP / max_XP);

        if (current_XP >= max_XP)
        {
            player_Level++;
            current_XP = 0;
            max_XP += max_XP;
        }
    }

    private void LevelBar()
    {
        player_Level_TXT.text = player_Level.ToString();
        current_max_TXT.text = current_XP + "/" + max_XP;

        PlayerPrefs.SetInt("max_XP", (int)max_XP);
        PlayerPrefs.SetInt("current_XP", (int)current_XP);
        PlayerPrefs.SetInt("player_Level", (int)player_Level);
    }
    #endregion

    #region DailyHealth
    private bool IsHearthReady()
    {
        totalHeartTXT.text = PlayerPrefs.GetInt("Heart").ToString();

        if (PlayerPrefs.GetInt("Heart") != 9)
        {
            float secondsLeft = secondsLeftMethod();

            if (secondsLeft < 0)
            {
                lastHeartCollected = (ulong)DateTime.Now.Ticks;
                PlayerPrefs.SetString("lastHeartCollected", lastHeartCollected.ToString());
                PlayerPrefs.SetInt("Heart", (PlayerPrefs.GetInt("Heart"))); // + 1
                return true;
            }
        }
        else
        {
            timer.text = "DOLU";
        }
        return false;
    }

    private float secondsLeftMethod()
    {
        ulong diff = (ulong)DateTime.Now.Ticks - lastHeartCollected;
        ulong m = diff / TimeSpan.TicksPerMillisecond;
        float secondsLeft = (float)(msToWait - m) / 1000.0f;

        string r = "";

        r += ((int)secondsLeft / 3600).ToString("00") + ":";
        secondsLeft -= ((int)secondsLeft / 3600) * 3600;
        r += ((int)secondsLeft / 60).ToString("00") + ":";
        r += (secondsLeft % 60).ToString("00");
        timer.text = r;
        return secondsLeft;
    }
    #endregion

    #region BarValues
    private void toString()
    {

        Coin_TXT.text = PlayerPrefs.GetInt("Coin").ToString();

        if (PlayerPrefs.GetInt("HalfCount") == 9)
        {
            half_TXT.text = "DOLU";
            fullHeart = true;
        }
        else
        {
            half_TXT.text = PlayerPrefs.GetInt("HalfCount").ToString();
        }

        if (PlayerPrefs.GetInt("NextCount") == 9)
        {
            next_TXT.text = "DOLU";
        }
        else
        {
            next_TXT.text = PlayerPrefs.GetInt("NextCount").ToString();
        }

        if (PlayerPrefs.GetInt("TimeCount") == 9)
        {
            time_TXT.text = "DOLU";
        }
        else
        {
            time_TXT.text = PlayerPrefs.GetInt("TimeCount").ToString();
        }
    }
    #endregion
}
