using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionLoader : MonoBehaviour
{
    public List<QuestionAndAnswer> QnA;

    void Start()
    {
        LoadQuestionsFromJSON();
        foreach (var question in QnA)
        {
            Debug.Log($"Question: {question.Question}");
            Debug.Log($"Answers: {string.Join(", ", question.Answers)}");
            Debug.Log($"Correct Answer Index: {question.CorrectAnswer}");
        }
    }

    private void LoadQuestionsFromJSON()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("questions");
        if (jsonFile != null)
        {
            QnAWrapper qnaWrapper = JsonUtility.FromJson<QnAWrapper>(jsonFile.text);
            QnA = qnaWrapper.QnA;
        }
        else
        {
            Debug.LogError("JSON file not found or loaded incorrectly.");
        }
    }

}
