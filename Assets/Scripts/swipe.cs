using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class swipe : MonoBehaviour
{
    public GameObject scrollbar, imageContent;
    private float scroll_pos = 0;
    float[] pos;
    private bool runIt = false;
    private float time;
    private Button takeTheBtn;
    int btnNumber = 0;

    public float mainSize = 0.9f;
    public float othersSize = 0.7f;

    private void Start()
    {
        startColor();
        backCardColor.GetComponent<Image>().color = karışık;
    }
    void Update()
    {
        Swiping();
    }

    private void Swiping()
    {
        pos = new float[transform.childCount];
        float distance = 1f / (pos.Length - 1f);

        if (runIt)
        {
            GecisiDuzenle(distance, pos, takeTheBtn);
            time += Time.deltaTime;

            if (time > 1f)
            {
                time = 0;
                runIt = false;
            }
        }

        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
        }

        if (Input.GetMouseButton(0))
        {
            scroll_pos = scrollbar.GetComponent<Scrollbar>().value;
        }
        else
        {
            for (int i = 0; i < pos.Length; i++)
            {
                if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
                {
                    scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[i], 0.1f);
                }
            }
        }


        for (int i = 0; i < pos.Length; i++)
        {
            if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
            {
                changeMainCardColorWhenClicked(i);
                transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(mainSize, mainSize), 0.1f); // mevcut seçili kart veya en öndeki görünün kart
                imageContent.transform.GetChild(i).localScale = Vector2.Lerp(imageContent.transform.GetChild(i).localScale, new Vector2(1.2f, 1.2f), 0.1f); // seçili buton
                //Color color1;
                //ColorUtility.TryParseHtmlString("#CB34A1", out color1);
                //imageContent.transform.GetChild(i).GetComponent<Image>().color = color1;
                for (int j = 0; j < pos.Length; j++)
                {
                    if (j != i)
                    {
                        //Color color2;
                        //ColorUtility.TryParseHtmlString("#CB34A1", out color2);
                        //imageContent.transform.GetChild(j).GetComponent<Image>().color = color2;
                        transform.GetChild(j).localScale = Vector2.Lerp(transform.GetChild(j).localScale, new Vector2(othersSize, othersSize), 0.1f); // arkada kalan kartların boyutu
                        imageContent.transform.GetChild(j).localScale = Vector2.Lerp(imageContent.transform.GetChild(j).localScale, new Vector2(0.8f, 0.8f), 0.1f); // seçilmemiş butonlar
                    }
                }
            }
        }
    }

    private void GecisiDuzenle(float distance, float[] pos, Button btn)
    {
        for (int i = 0; i < pos.Length; i++)
        {
            if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
            {
                scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[btnNumber], 1f * Time.deltaTime);

            }
        }

        for (int i = 0; i < btn.transform.parent.transform.childCount; i++)
        {
            btn.transform.name = ".";
        }

    }
    public void WhichBtnClicked(Button btn)
    {
        btn.transform.name = "clicked";
        for (int i = 0; i < btn.transform.parent.transform.childCount; i++)
        {
            if (btn.transform.parent.transform.GetChild(i).transform.name == "clicked")
            {
                btnNumber = i;
                takeTheBtn = btn;
                time = 0;
                scroll_pos = (pos[btnNumber]);
                runIt = true;
                changeMainCardColorWhenClicked(btnNumber);
            }
        }
    }

    public GameObject backCardColor;

    Color karışık;
    Color matematik;
    Color bilim;
    Color sanat;
    Color tarih;
    Color coğrafya;
    Color spor;
    Color eğlence;

    private void changeMainCardColorWhenClicked(int btnNumber)
    {
        if (btnNumber == 0)
        {
            //mainCardColors[0].GetComponent<Image>().color = karışık; //kategori kartlarının rengini değiştirir.
            backCardColor.GetComponent<Image>().color = karışık; //arkadaki kartın rengini değiştirir.
        }
        else if (btnNumber == 1)
        {
            //mainCardColors[1].GetComponent<Image>().color = matematik;
            backCardColor.GetComponent<Image>().color = matematik;
        }
        else if (btnNumber == 2)
        {
            //categoryCardColors[2].GetComponent<Image>().color = bilim;
            backCardColor.GetComponent<Image>().color = bilim;
        }
        else if (btnNumber == 3)
        {
            //categoryCardColors[3].GetComponent<Image>().color = sanat;
            backCardColor.GetComponent<Image>().color = sanat;
        }
        else if (btnNumber == 4)
        {
            //categoryCardColors[4].GetComponent<Image>().color = tarih;
            backCardColor.GetComponent<Image>().color = tarih;
        }
        else if (btnNumber == 5)
        {
            //categoryCardColors[5].GetComponent<Image>().color = coğrafya;
            backCardColor.GetComponent<Image>().color = coğrafya;
        }
        else if (btnNumber == 6)
        {
            //categoryCardColors[6].GetComponent<Image>().color = sport;
            backCardColor.GetComponent<Image>().color = spor;
        }
        else if (btnNumber == 7)
        {
            //categoryCardColors[7].GetComponent<Image>().color = eğlence;
            backCardColor.GetComponent<Image>().color = eğlence;
        }
    }

    private void startColor() //geçiş butonlarının rengini değiştirir.
    {
        ColorUtility.TryParseHtmlString("#E2DFFF", out karışık);
        imageContent.transform.GetChild(0).GetComponent<Image>().color = karışık;

        ColorUtility.TryParseHtmlString("#525252", out matematik);
        imageContent.transform.GetChild(1).GetComponent<Image>().color = matematik;

        ColorUtility.TryParseHtmlString("#0C92CF", out bilim);
        imageContent.transform.GetChild(2).GetComponent<Image>().color = bilim;

        ColorUtility.TryParseHtmlString("#E32425", out sanat);
        imageContent.transform.GetChild(3).GetComponent<Image>().color = sanat;

        ColorUtility.TryParseHtmlString("#A63E27", out tarih);
        imageContent.transform.GetChild(4).GetComponent<Image>().color = tarih;

        ColorUtility.TryParseHtmlString("#227915", out coğrafya);
        imageContent.transform.GetChild(5).GetComponent<Image>().color = coğrafya;

        ColorUtility.TryParseHtmlString("#F7F835", out spor);
        imageContent.transform.GetChild(6).GetComponent<Image>().color = spor;

        ColorUtility.TryParseHtmlString("#D500DC", out eğlence);
        imageContent.transform.GetChild(7).GetComponent<Image>().color = eğlence;
    }

}