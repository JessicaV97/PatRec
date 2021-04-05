using UnityEngine;

[System.Serializable]
public class QuestionAndAnswers
{
    public string Question;
    public Sprite[] Answers;
    public string[] AnswersAudio;
    public int CorrectAnswer;
    public TextAsset Json;
}
