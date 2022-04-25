using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Answer : MonoBehaviour
{
    public bool isCorrect = false;

    public QuizManager quizManager;

    private Color startColor;

    private void Start()
    {
        startColor = GetComponent<Image>().color;
    }

    public void AnswerMethod()
    {
        Invoke("SetDefaultColor", 2f);

        if (isCorrect)
        {
            GetComponent<Image>().color = Color.green;
            quizManager.Correct();
        }
        else
        {
            quizManager.NotCorrect();
            setWrongColor();
        }
    }
    public void setWrongColor()
    {
        for (int i = 0; i < quizManager.wrong_Answers.Count; i++)
        {
            quizManager.wrong_Answers[i].GetComponent<Image>().color = Color.red;
            
        }
        for (int i = 0; i < quizManager.correct_Answers.Count; i++)
        {
            quizManager.correct_Answers[i].GetComponent<Image>().color = Color.green;
        }
    }

    private void SetDefaultColor()
    {
        GetComponent<Image>().color = startColor;
    }


}
