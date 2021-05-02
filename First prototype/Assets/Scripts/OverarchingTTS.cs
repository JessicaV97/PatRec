using Happify.Levels;
using Happify.User;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

namespace Happify.TextToSpeech
{
	public class OverarchingTTS : MonoBehaviour
	{
		private static OverarchingTTS _instance;
		public static OverarchingTTS Instance => _instance;
		private UserDescription _currentUser;

		private void Awake()
		{
			if (_instance == null)
			{
				_instance = this;
				// Ensure that script does not get destroyed when changing scene.
				DontDestroyOnLoad(this);
			}
            else
                Destroy(this);
        }

		// Start is called before the first frame update
		void Start()
		{
			_currentUser = UserManager.Instance.CurrentUser;
		}

		// Update is called once per frame
		void Update()
		{

		}

		public void OnSingleClick(string button, AudioSource audio)
		{
			if (button.Equals("Next"))
				LevelSwiper.Instance.NextLevelTopic();
			else if (button.Equals("Previous"))
				LevelSwiper.Instance.PreviousLevelTopic();
			else if (button.Equals("StudySymbols"))
				SceneManager.LoadScene("scn_StudyEnvironment");
			else if ((button.Equals("PlayGame") || button.Equals("NextLevel")) && _currentUser.NrOfLives != 0)
				SceneManager.LoadScene("scn_MainGameScreen");

			else if (button.Equals("Play") || button.Equals("BackToLevels"))
				SceneManager.LoadScene("scn_Levels");
			else if (button.Equals("Settings") /*|| button.Equals("BackToSettings")*/)
				SceneManager.LoadScene("scn_Settings");
			else if (button.Equals("Achievements") || button.Equals("BackToAchievements"))
				SceneManager.LoadScene("scn_Achievements");
			else if (button.Equals("ListOfScores"))
				AchievementsCollector.Instance.ShowScores();
			else if (button.Equals("BackToHome"))
				SceneManager.LoadScene("scn_MainMenu");
			else if (button.Equals("Pause"))
				PauseMenu.Instance.Pause();
			else if (button.Equals("Continue"))
				PauseMenu.Instance.Resume();
		}

		public void OnDoubleClick(string button, AudioSource audio)
		{
			int topic = LevelSwiper.GetLevel();
			if (button.Equals("Next") || button.Equals("NextBadge"))
				StartCoroutine(DownloadTheAudio("Volgende", audio));
			else if (button.Equals("Previous") || button.Equals("PreviousBadge"))
				StartCoroutine(DownloadTheAudio("Vorige", audio));
			else if (button.Equals("StudySymbols"))
			{
				if (topic == 1)
					StartCoroutine(DownloadTheAudio("Bestudeer patronen in de algemene context", audio));
				else
					StartCoroutine(DownloadTheAudio("Bestudeer patronen in de context van emoties en sfeer.", audio));
			}
			else if (button.Equals("PlayGame"))
			{
				if (topic == 1)
					StartCoroutine(DownloadTheAudio("Speel spel in de algemene context", audio));
				else
					StartCoroutine(DownloadTheAudio("Speel spel in de context van emoties en sfeer", audio));
			}
			else if (button.Equals("NextLevel"))
				if (topic == 1)
					StartCoroutine(DownloadTheAudio("Volgend level in de algemene context", audio));
				else
					StartCoroutine(DownloadTheAudio("Volgend level in de context van emoties en sfeer", audio));
			else if (button.Equals("Play") || button.Equals("BackToLevels"))
				StartCoroutine(DownloadTheAudio("Naar levels", audio));
			else if (button.Equals("Settings"))
				StartCoroutine(DownloadTheAudio("Naar instellingen", audio));
			else if (button.Equals("BackToSettings"))
				StartCoroutine(DownloadTheAudio("Terug naar instellingen", audio));
			else if (button.Equals("Achievements") || button.Equals("BackToAchievements"))
				StartCoroutine(DownloadTheAudio("Naar badges collectie", audio));
			else if (button.Equals("ListOfScores"))
				StartCoroutine(DownloadTheAudio("Naar de lijst met scores per persoon", audio));
			else if (button.Equals("BackToHome") || button.Equals("BackFromSettings"))
				StartCoroutine(DownloadTheAudio("Terug naar menu", audio));
			else if (button.Equals("replayPattern"))
				StartCoroutine(DownloadTheAudio("Speel patroon opnieuw", audio));
			else if (button.Equals("Pause"))
				StartCoroutine(DownloadTheAudio("Pauzeer het spel", audio));
			else if (button.Equals("Continue"))
				StartCoroutine(DownloadTheAudio("Speel verder", audio));
			else if (button.Equals("Lives") || button.Equals("ImageLives"))
				StartCoroutine(DownloadTheAudio("Aantal levens is " + _currentUser.NrOfLives.ToString(), audio));
			else if (button.Equals("ContextButton"))
				StartCoroutine(DownloadTheAudio("Context is " + QuizManager.Instance.GetContext, audio));
			else if (button.Equals("ScoreButton"))
				StartCoroutine(DownloadTheAudio("Score is " + QuizManager.Instance.GetScore.ToString(), audio));
			else if (button.Equals("LivesCharge"))
				StartCoroutine(DownloadTheAudio("Geladen voor "+ LivesManagement.getValue(), audio));

		}

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
}