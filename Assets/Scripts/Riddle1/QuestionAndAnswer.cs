using System.Collections.Generic;

[System.Serializable]
public class QuestionAndAnswer
{
    public string Question;
    public string[] Answers;
    public int CorrectAnswer;
    public string[] Images;
}

[System.Serializable]
public class QnAWrapper
{
    public List<QuestionAndAnswer> QnA;
}
