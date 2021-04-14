using UnityEngine;
using UnityEngine.EventSystems;
using Happify.User;
using System.Collections;

public class AnswerScript : MonoBehaviour, IPointerClickHandler
{
    public bool IsCorrect = false; 
    public QuizManager QuizManager;
    public string AudioName;

    public static AnswerScript AS;

    private UserDescription currentUser;

    private float interval = 0.3f;
    int tap = 0;

    private void Awake() 
    { 
        AS = this;
        currentUser = UserManager.Instance.CurrentUser;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        string objName = eventData.selectedObject.name;
        if (!currentUser.RemainingVision && currentUser.RemainingHearing)
        {
            tap++;

            if (tap == 1)
            {
                StartCoroutine(DoubleTapInterval());
                OnDoubleClick(/*objName*/);
            }
            else if (tap > 1)
            {
                OnSingleClick(/*objName*/);
                tap = 0;
            }
        }
        else
            OnSingleClick(/*objName*/);
    }

    IEnumerator DoubleTapInterval()
    {
        yield return new WaitForSeconds(interval);
        this.tap = 0;
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
