using UnityEngine;
using UnityEngine.EventSystems;
using Happify.User;

public class AnswerScript : MonoBehaviour, IPointerClickHandler
{
    public bool IsCorrect = false; 
    public QuizManager QuizManager;
    public string AudioName;

    public static AnswerScript AS;

    private void Awake() 
    { 
        AS = this;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        UserDescription currentUser = UserManager.Instance.CurrentUser;
        int clickCount = eventData.clickCount;
        if (!currentUser.RemainingVision && currentUser.RemainingHearing)
        {
            if (clickCount == 2)
                OnSingleClick(); //Name is conflicting
            else if (clickCount == 1)
                OnDoubleClick(); //Name is conflicting
        }
        else
            OnSingleClick();
    }

    void OnSingleClick()
    {
        if (IsCorrect)
            QuizManager.Correct();
        else
            QuizManager.Wrong();
    }

    void OnDoubleClick()
    {
        Debug.Log("Double Clicked");
        QuizManager.ReadPattern(AudioName);
    }

}
