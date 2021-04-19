using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Happify.Client;
using System.Text.RegularExpressions;
using UnityEngine.Networking;
using System.Collections;
using Happify.User;
using Happify.Levels;

public class StudyManager : MonoBehaviour
{
	private Object[] patternsComplete;

	//Get objects for study environment
	public GameObject PatternVisual;
	public TextMeshProUGUI PatternTitle;
	public int PatternIndex = 0;
	public static int LevelIndex;

    public AudioSource _audio;

	private UserDescription currentUser;

	public async void Awake()
	{ 
		await MqttService.Instance.ConnectAsync();
		currentUser = UserManager.Instance.CurrentUser;
	}
	public void Start()
	{
		_audio = gameObject.GetComponent<AudioSource>();

		LevelIndex = LevelSwiper.GetLevel();
		if (LevelIndex != 1)
			patternsComplete = Resources.LoadAll("ScriptableObjects/SO_Emotions", typeof(SOPattern));
		else
			patternsComplete = Resources.LoadAll("ScriptableObjects/SO_General", typeof(SOPattern));

		SetPattern();
	}

	public void NextPattern()
	{
		if (PatternIndex == patternsComplete.Length - 1)
			PatternIndex = 0;
		else
			PatternIndex++;

		SetPattern();
		if (!currentUser.RemainingVision && currentUser.RemainingHearing)
			StartCoroutine(DownloadTheAudio((patternsComplete[PatternIndex] as SOPattern).PatternName));
	}
	
	public void PreviousPattern()
	{
		if (PatternIndex == 0) 
			PatternIndex = patternsComplete.Length - 1;
		else 
			PatternIndex--;

		SetPattern();
        if (!currentUser.RemainingVision && currentUser.RemainingHearing)
            StartCoroutine(DownloadTheAudio((patternsComplete[PatternIndex] as SOPattern).PatternName));
    }
	
	
	private void SetPattern()
    {
		PatternVisual.GetComponent<Image>().sprite = (patternsComplete[PatternIndex] as SOPattern).PatternImage;
		PatternTitle.GetComponent<TextMeshProUGUI>().text = (patternsComplete[PatternIndex] as SOPattern).PatternName;
	}

	public async void PlayPattern()
    {
		if (!currentUser.RemainingVision)
		{
			StartCoroutine(DownloadTheAudio((patternsComplete[PatternIndex] as SOPattern).PatternName));
		}

		string json = (patternsComplete[PatternIndex] as SOPattern).PatternJson.text;
        json = Regex.Replace(json, @"\t|\n|\r", "");
        json = json.Replace(" ", "");
        json = json.Replace("255", "1.0");
		json = json.Replace("islooped", "isLooped");
		await MqttService.Instance.PublishAsync("happify/play", json);
		

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