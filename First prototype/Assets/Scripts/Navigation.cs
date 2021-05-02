using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using Happify.User;
using Happify.TextToSpeech;

public class Navigation : MonoBehaviour, IPointerClickHandler
{
    public AudioSource Audio;
    // Start is called before the first frame update

    private UserDescription currentUser;

    private float interval = 0.3f;
    int tap = 0;


    public void Start()
    {
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
                OverarchingTTS.Instance.OnDoubleClick(objName, Audio);
            }
            else if (tap > 1)
            {
                OverarchingTTS.Instance.OnSingleClick(objName, Audio);
                tap = 0;
            }
        }
        else
            OverarchingTTS.Instance.OnSingleClick(objName, Audio);
    }

    IEnumerator DoubleTapInterval()
    {
        yield return new WaitForSeconds(interval);
        this.tap = 0;
    }
}
