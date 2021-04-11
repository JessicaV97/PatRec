using Happify.Levels;
using Happify.User;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LevelsTTS : MonoBehaviour, IPointerClickHandler
{

    public AudioSource _audio;
	// Start is called before the first frame update
	public void OnPointerClick(PointerEventData eventData)
	{
		int clickCount = eventData.clickCount;
		string objName = eventData.selectedObject.name;
		UserDescription currentUser = UserManager.Instance.CurrentUser;
		if (!currentUser.RemainingVision && currentUser.RemainingHearing)
		{
			if (clickCount == 2)
			{
				OnSingleClick(objName);
			}
			else if (clickCount == 1)
			{
				OnDoubleClick(objName);
			}
		}
		else
		{
			//string objName = eventData.selectedObject.name;
			OnSingleClick(objName);
		}
	}

	void OnDoubleClick(string button)
	{
		if (button.Equals("Next"))
			StartCoroutine(DownloadTheAudio("Volgende"));
		else if (button.Equals("Previous"))
			StartCoroutine(DownloadTheAudio("Vorige"));
		else if (button.Equals("StudySymbols"))
        {
			int topic = LevelSwiper.GetLevel();
			if (topic == 5)
				StartCoroutine(DownloadTheAudio("Bestudeer patronen in de algemene context"));
			else
				StartCoroutine(DownloadTheAudio("Bestudeer patronen in de context van emoties en sfeer."));
		}
		else if (button.Equals("PlayGame"))
        {
			int topic = LevelSwiper.GetLevel();
			if (topic == 5)
				StartCoroutine(DownloadTheAudio("Speel spel in de algemene context"));
			else
				StartCoroutine(DownloadTheAudio("Speel spel in de context van emoties en sfeer"));
		}
			
	}

	void OnSingleClick(string button)
	{
		if (button.Equals("Next"))
			LevelSwiper.Instance.NextPattern();
		else if (button.Equals("Previous"))
			LevelSwiper.Instance.PreviousPattern();
		else if (button.Equals("StudySymbols"))
			SceneManager.LoadScene("scn_StudyEnvironment");
		else if (button.Equals("PlayGame"))
			SceneManager.LoadScene("scn_MainGameScreen");
	}

	IEnumerator DownloadTheAudio(string message)
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
				_audio.clip = myClip;
				_audio.Play();
			}
		}
	}
}
