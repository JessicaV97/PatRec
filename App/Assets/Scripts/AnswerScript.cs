using UnityEngine;
using UnityEngine.EventSystems;
using Happify.User;
using System.Collections;

/// <summary>
/// Class is used in 'scn_MainGameScreen'. Checks whether the chosen answer was correct or not.
/// The single vs double tapping is used to determine whether something needs to be read out loud in blind mode. 
/// </summary>
public class AnswerScript : MonoBehaviour, IPointerClickHandler
{
    public bool IsCorrect = false; 
    public QuizManager QuizManager;
    public string AudioName;

    public static AnswerScript AS;

    private UserDescription currentUser;

    private float interval = 0.3f;
    private int tap = 0;

    private void Awake() 
    { 
        AS = this;
        currentUser = UserManager.Instance.CurrentUser;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!currentUser.RemainingVision && currentUser.RemainingHearing)
        {
            tap++;

            if (tap == 1)
            {
                StartCoroutine(DoubleTapInterval());
                OnSingleClick();
            }
            else if (tap > 1)
            {
                OnDoubleClick();
                tap = 0;
            }
        }
        else
            OnDoubleClick();
    }

    IEnumerator DoubleTapInterval()
    {
        yield return new WaitForSeconds(interval);
        this.tap = 0;
    }

    void OnDoubleClick()
    {
        if (IsCorrect)
            QuizManager.Correct();
        else
            QuizManager.Wrong();
    }

    void OnSingleClick()
    {
        QuizManager.ReadPattern(AudioName);
    }
}
