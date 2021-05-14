using UnityEngine;
/// <summary>
/// Class that creates objects for the questions in the multiple choice quiz. 
/// Stores the question (definition in string) and pattern in json) with the answer options and their visualizations.
/// Also the integer that matches the correct answer. 
/// </summary>
[System.Serializable]
public class QuestionAndAnswers
{
    public string Question;
    public Sprite[] Answers;
    public string[] AnswersAudio;
    public int CorrectAnswer;
    public TextAsset Json;
}
