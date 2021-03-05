using UnityEngine;


[System.Serializable]
public class QuestionAndAnswers
{
    public string Question; // Do not get from inspector but use existing lists
    public Sprite[] Answers;
    public int CorrectAnswer;
}
