using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using Happify.User;
using Happify.TextToSpeech;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

/// <summary>
/// Class that is used to enable the speech synthesis for blind mode. Basically used throughout the whole app.  
/// </summary>
public class Navigation : MonoBehaviour, IPointerClickHandler
{
    public AudioSource Audio;
    //String that is used to collect the information that needs to be read out loud to a user using the app in blind mode
    private string _audioString;

    private UserDescription currentUser;

    private float interval = 0.3f;
    int tap = 0;
    string sceneName = "";

    /// <summary>
    /// Collect information of the current user and environment.
    /// If blind mode is on, provide speech synthesis to help the user navigate through the app. 
    /// </summary>
    public void Start()
    {
        currentUser = UserManager.Instance.CurrentUser;
        sceneName = SceneManager.GetActiveScene().name;
        if (!currentUser.RemainingVision && currentUser.RemainingHearing)
        {
            if (sceneName == "scn_MainMenu")
                _audioString = "In home scherm";
            else if (sceneName == "scn_Levels")
                _audioString = "In levels scherm";
            else if (sceneName == "scn_MainGameScreen")
                _audioString = "In spel";
            else if (sceneName == "scn_StudyEnvironment")
                _audioString = "In studeeromgeving";
            else if (sceneName == "scn_Settings")
                _audioString = "In instellingen";
            StartCoroutine(DownloadTheAudio(_audioString, Audio));
        }
            
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
                OverarchingTTS.Instance.OnSingleClick(objName, Audio);
            }
            else if (tap > 1)
            {
                OverarchingTTS.Instance.OndoubleClick(objName, Audio);
                tap = 0;
            }
        }
        else
            OverarchingTTS.Instance.OndoubleClick(objName, Audio);
    }

    IEnumerator DoubleTapInterval()
    {
        yield return new WaitForSeconds(interval);
        this.tap = 0;
    }

    /// <summary>
    /// Function to produce speech synthesis using google translate. Wifi needed. 
    /// </summary>
    /// <param name="message"></param>
    /// Message is the string of information you want to have read out loud
    /// <returns></returns>
    private IEnumerator DownloadTheAudio(string message, AudioSource audio)
    {
        using (UnityWebRequest website = UnityWebRequestMultimedia.GetAudioClip("https://translate.google.com/translate_tts?ie=UTF-8&total=1&idx=0&textlen=32&client=tw-ob&q=" + message + "&tl=NL", AudioType.MPEG))
        {
            yield return website.SendWebRequest();

            if (website.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(website.error);
            }
            else
            {
                AudioClip myClip = DownloadHandlerAudioClip.GetContent(website);
                audio.clip = myClip;
                audio.Play();
            }
        }
    }
}
