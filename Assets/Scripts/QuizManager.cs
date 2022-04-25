using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class QuizManager : MonoBehaviour
{
    public new ParticleSystem particleSystem;

    #region List

    public List<QuestionsAndAnswers> QnA;
    public List<GameObject> wrong_Answers;
    public List<GameObject> correct_Answers;

    #endregion

    public GameObject[] options;
    private int Current_Question;

    #region countDown

    public Slider countDown;
    private bool correctBool = false;

    #endregion

    #region txt

    [Header("TextField")]
    public TMP_Text Total_Score_TXT;
    public TMP_Text Question_value_TXT;
    public Text Question_TXT;

    #endregion

    #region jokers

    [Header("HalfNHalf")]
    public Button Half_N_Half_Button;
    private int Random1;
    private int Random2;

    [Header("NextQuestion")]
    public Button Next_Question_Button;

    [Header("AddTime")]
    public Button Add_Time_Button;

    #endregion

    private void Start()
    {
        GenerateQuestion();
    }
    
    private void Update()
    {
        NotEnoughHeart();

        if (!correctBool)
        {
            TimesUp();
        }
    }

    private void NotEnoughHeart()
    {
        if(PlayerPrefs.GetInt("Heart") == 0)
        {
            Debug.Log("Not enough heart");

            SceneManager.LoadScene("MainMenu");

            //Anamenüye dön ya da mağazaya git paneli açılacak | HOME BUTON IMG , SHOP BUTON IMG |
        }
    }

    private int CurrentQuestionValue = 1;

    void GenerateQuestion()
    {
        Question_value_TXT.text = "SORU " + CurrentQuestionValue.ToString();

        correctBool = false;

        countDown.value = countDown.maxValue;

        ResetGame();

        if (QnA.Count > 0)
        {
            Current_Question = Random.Range(0, QnA.Count);

            Question_TXT.text = QnA[Current_Question].Question; 

            SetAnswer();
        }
        else
        {
            Debug.Log("Soruların hepsi bitti.");

            GameOver();
        }

        CurrentQuestionValue++;
    }

    void SetAnswer()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<Answer>().isCorrect = false;

            options[i].transform.GetChild(0).GetComponent<Text>().text = QnA[Current_Question].Answers[i];

            if (QnA[Current_Question].CorrectElement == i)
            {
                options[i].GetComponent<Answer>().isCorrect = true;

                correct_Answers.Add(options[i]);
            }
            else
            {
                wrong_Answers.Add(options[i]);
            }
        }
    }

    private int totalCorrect = 0;

    public void Correct()
    {
        totalCorrect++;

        correctBool = true;

        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<Button>().interactable = false;
        }

        QnA.RemoveAt(Current_Question); // Çözülen soruyu listeden çıkartıyor

        particleSystem.Play();

        Invoke("GenerateQuestion", 2f);

        PlayerPrefs.SetInt("totalCorrect", totalCorrect);
    }

    public void NotCorrect()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<Button>().interactable = false; // Yanlış cevap verdikten sonra bütün butonları menüye dönene kadar kapatıyor
        }
        PlayerPrefs.SetInt("Heart", PlayerPrefs.GetInt("Heart") - 1);

        QnA.RemoveAt(Current_Question);

        Invoke("GameOver", 2f);
    }

    private void ResetGame()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<Button>().interactable = true;

            options[i].GetComponent<Button>().image.color = Color.white;

            wrong_Answers.Remove(options[i]); // bir önceki soruda depolanan yanlış cevapları resetliyor

            correct_Answers.Remove(options[i]); // bir önceki soruda depolanan doğru cevapları resetliyor
        }
    }

    private void TimesUp()
    {
        countDown.value -= 1 * Time.deltaTime; //countDownSpeed

        if (countDown.value <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    #region Jokers
    public void HalfnHalf()
    {
        if (PlayerPrefs.GetInt("HalfCount") != 0)
        {
            int c = 0;

            while (c != 2)
            {
                this.Random1 = Random.Range(0, 3);

                this.Random2 = Random.Range(0, 3);


                if (this.Random1 == this.Random2)
                {
                    this.Random1 = Random.Range(0, 3);

                    this.Random2 = Random.Range(0, 3);
                }
                else
                {
                    ColorAndInteractableHalfNHalf(false, Color.red);

                    Half_N_Half_Button.interactable = false;

                    c = 2;

                    PlayerPrefs.SetInt("HalfCount", (PlayerPrefs.GetInt("HalfCount") - 1));

                    Debug.Log(Random1 + "" + Random2);
                }
            }
        }
    }
    private void ColorAndInteractableHalfNHalf(bool c, Color color)
    {
        wrong_Answers[Random1].GetComponent<Button>().interactable = c;

        wrong_Answers[Random1].GetComponent<Button>().image.color = color;

        wrong_Answers[Random2].GetComponent<Button>().interactable = c;

        wrong_Answers[Random2].GetComponent<Button>().image.color = color;
    }

    public void AddMoreTime()
    {
        if (PlayerPrefs.GetInt("TimeCount") != 0)
        {
            countDown.value += 5;

            Add_Time_Button.interactable = false;

            PlayerPrefs.SetInt("TimeCount", (PlayerPrefs.GetInt("TimeCount") - 1));
        }
    }

    public void NextQuestion() 
    {
        if(PlayerPrefs.GetInt("NextCount") != 0)
        {
            PlayerPrefs.SetInt("NextCount", (PlayerPrefs.GetInt("NextCount") - 1));

            Next_Question_Button.interactable = false;

            QnA.RemoveAt(Current_Question);

            GenerateQuestion();
        }
    }

    #endregion

    #region Getter_Setter
    
    public int getTotalCorrect()
    {
        return totalCorrect;
    }

    public void setTotalCorrect(int totalCorrect)
    {
        this.totalCorrect = totalCorrect;
    }

    #endregion
}
