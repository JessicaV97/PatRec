using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Happify.Client;
using System.Text.RegularExpressions;
using UnityEngine.Networking;
using System.Collections;
using Happify.User;
using Happify.Levels;

/// <summary>
/// Class that handles everything around the study environment. 
/// </summary>
public class StudyManager : MonoBehaviour
{
	private Object[] _patternsComplete;
	private int _patternIndex = 0;
	private AudioSource _audio;
	//Collect game objects used in this script
	[SerializeField]
	private GameObject _patternVisual;
	[SerializeField]
	private TextMeshProUGUI _patternTitle;
	[SerializeField]
	private GameObject _mainCamera;
	[SerializeField]
	private UserDescription _currentUser;

	public static int LevelIndex;

	/// <summary>
	/// Create MQTT connection and collect current user information
	/// </summary>
	public async void Awake()
	{ 
		await MqttService.Instance.ConnectAsync();
		_currentUser = UserManager.Instance.CurrentUser;
	}

	/// <summary>
	/// Get audio source from camera and conclude which context has been selected. 
	/// Collect patterns for that topic. 
	/// </summary>
	public void Start()
	{
		_audio = _mainCamera.GetComponent<AudioSource>();

		LevelIndex = LevelSwiper.GetLevel();
		if (LevelIndex != 1)
			_patternsComplete = Resources.LoadAll("ScriptableObjects/SO_Emotions", typeof(SOPattern));
		else
			_patternsComplete = Resources.LoadAll("ScriptableObjects/SO_General", typeof(SOPattern));

		SetPattern();
	}

	/// <summary>
	/// Go to next pattern in list of patterns. 
	/// Update index.
	/// Probe function to adjust the information on screen
	/// </summary>
	public void NextPattern()
	{
		if (_patternIndex == _patternsComplete.Length - 1)
			_patternIndex = 0;
		else
			_patternIndex++;

		SetPattern();
		if (!_currentUser.RemainingVision && _currentUser.RemainingHearing)
			StartCoroutine(DownloadTheAudio("Volgende. " + (_patternsComplete[_patternIndex] as SOPattern).PatternName));
	}

	/// <summary>
	/// Go to previous pattern in list of patterns. 
	/// Update index.
	/// /// Probe function to adjust the information on screen
	/// </summary>
	public void PreviousPattern()
	{
		if (_patternIndex == 0) 
			_patternIndex = _patternsComplete.Length - 1;
		else 
			_patternIndex--;

		SetPattern();
        if (!_currentUser.RemainingVision && _currentUser.RemainingHearing)
            StartCoroutine(DownloadTheAudio("Vorige. " + (_patternsComplete[_patternIndex] as SOPattern).PatternName));
    }
	
	/// <summary>
	/// Update the visual and title when next or previous pattern button has been pressed. 
	/// </summary>
	private void SetPattern()
    {
		_patternVisual.GetComponent<Image>().sprite = (_patternsComplete[_patternIndex] as SOPattern).PatternImage;
		_patternTitle.GetComponent<TextMeshProUGUI>().text = (_patternsComplete[_patternIndex] as SOPattern).PatternName;
	}

	/// <summary>
	/// Send message over MQTT connection to play the pattern. 
	/// Small json adoptations to match the expectations of the listener or improve readability. 
	/// </summary>
	public async void PlayPattern()
    {
		if (!_currentUser.RemainingVision)
		{
			StartCoroutine(DownloadTheAudio((_patternsComplete[_patternIndex] as SOPattern).PatternName));
		}

		string json = (_patternsComplete[_patternIndex] as SOPattern).PatternJson.text;
        json = Regex.Replace(json, @"\t|\n|\r", "");
        json = json.Replace(" ", "");
        json = json.Replace("255", "1.0");
		json = json.Replace("islooped", "isLooped");
		await MqttService.Instance.PublishAsync("happify/play", json);
	}

	/// <summary>
	/// Function to produce speech synthesis using google translate. Wifi needed. 
	/// </summary>
	/// <param name="message"></param>
	/// Message is the string of information you want to have read out loud
	/// <returns></returns>
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