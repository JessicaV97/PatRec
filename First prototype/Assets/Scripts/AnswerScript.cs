using UnityEngine;

public class AnswerScript : MonoBehaviour
{
    public bool IsCorrect = false;
    public QuizManager QuizManager;

    public void Answer()
    {
        if(IsCorrect)
            QuizManager.Correct();
        else         
            QuizManager.Wrong();
    }

}
