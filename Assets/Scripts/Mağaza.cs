using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Mağaza : MonoBehaviour
{
    private int Joker_Price;

    private bool canBuy = false;

    [SerializeField] GameObject health_Panel;
    [SerializeField] GameObject coin_Panel;
    [SerializeField] GameObject time_Panel;
    [SerializeField] GameObject next_Panel;
    [SerializeField] GameObject half_Panel;

    [SerializeField] Button[] HeartButtons;
    [SerializeField] Button[] TimeButtons;
    [SerializeField] Button[] NextButtons;
    [SerializeField] Button[] HalfButtons;

    public void HeartMethod(int count)
    {
        if ((PlayerPrefs.GetInt("Heart") + count) <= 9) // Can buy
        {
            if (PlayerPrefs.GetInt("Coin") - Joker_Price >= 0)
            {
                canBuy = true;
                PriceMethod(Joker_Price);
                PlayerPrefs.SetInt("Heart", (PlayerPrefs.GetInt("Heart") + count));
            }
        }
        else //Cannot buy
        {
            if (PlayerPrefs.GetInt("Heart") == 9)
            {
                StartCoroutine(NotEnoughMoney(health_Panel, Color.green));
            }
            else
            {
                StartCoroutine(NotEnoughMoney(coin_Panel, Color.red));
            }
            canBuy = false;
        }

    }

    public void TimeMethod(int count)
    {
        if ((PlayerPrefs.GetInt("TimeCount") + count) <= 9) // Can buy
        {
            if (PlayerPrefs.GetInt("Coin") - Joker_Price >= 0)
            {
                canBuy = true;
                PriceMethod(Joker_Price);
                PlayerPrefs.SetInt("TimeCount", (PlayerPrefs.GetInt("TimeCount") + count));
            }
        }
        else //Cannot buy
        {
            if (PlayerPrefs.GetInt("TimeCount") == 9)
            {
                StartCoroutine(NotEnoughMoney(time_Panel, Color.green));
            }
            else
            {
                StartCoroutine(NotEnoughMoney(coin_Panel, Color.red));
            }
            canBuy = false;
        }

    }

    public void NextMethod(int count)
    {
        if ((PlayerPrefs.GetInt("NextCount") + count) <= 9) // Can buy
        {
            if (PlayerPrefs.GetInt("Coin") - Joker_Price >= 0)
            {
                canBuy = true;
                PriceMethod(Joker_Price);
                PlayerPrefs.SetInt("NextCount", (PlayerPrefs.GetInt("NextCount") + count));
            }
        }
        else //Cannot buy
        {
            if (PlayerPrefs.GetInt("NextCount") == 9)
            {
                StartCoroutine(NotEnoughMoney(next_Panel, Color.green));
            }
            else
            {
                StartCoroutine(NotEnoughMoney(coin_Panel, Color.red));
            }
            canBuy = false;
        }

    }

    public void HalfMethod(int count)
    {
        if ((PlayerPrefs.GetInt("HalfCount") + count) <= 9) // Can buy
        {
            if(PlayerPrefs.GetInt("Coin") - Joker_Price >= 0)
            {
                canBuy = true;
                PriceMethod(Joker_Price);
                PlayerPrefs.SetInt("HalfCount", (PlayerPrefs.GetInt("HalfCount") + count));
            }
        }
        else //Cannot buy
        {
            if(PlayerPrefs.GetInt("HalfCount") == 9)
            {
                StartCoroutine(NotEnoughMoney(half_Panel, Color.green));
            }
            else
            {
                StartCoroutine(NotEnoughMoney(coin_Panel, Color.red));
            }
            
            canBuy = false;
        }
    }

    public void PriceMethod(int price)
    {
        Joker_Price = price;

        if (PlayerPrefs.GetInt("Coin") - price >= 0 && canBuy)
        {
            PlayerPrefs.SetInt("Coin", (PlayerPrefs.GetInt("Coin") - price));
            Joker_Price = 0;
        }
    }

    IEnumerator NotEnoughMoney(GameObject panel, Color color)
    {
        panel.GetComponent<Image>().color = color;
        panel.transform.localScale = new Vector2(1.1f, 1.1f);
        yield return new WaitForSeconds(2f);
        panel.GetComponent<Image>().color = color;
        panel.transform.localScale = new Vector2(1f, 1f);

        //yeterli coin yok, yeteri kadar jokere sahipsin vs. uyarısı
    }
}