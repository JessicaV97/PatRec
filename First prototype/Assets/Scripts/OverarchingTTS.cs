using Happify.Levels;
using Happify.User;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

namespace Happify.TextToSpeech
{
	/// <summary>
	/// Class that produces the speech synthesis. 
	/// In blind mode tapping a button reads out loud the message, tapping twice is using the button. 
	/// What is said or where to go (scenes) is hard coded. 
	/// </summary>
	public class OverarchingTTS : MonoBehaviour
	{
		// Create instance of TTS (text to speech)
		private static OverarchingTTS _instance;
		public static OverarchingTTS Instance => _instance;

		private UserDescription _currentUser;

		/// <summary>
		/// Create instance that does not get destroyed when switching scenes.
		/// </summary>
		private void Awake()
		{
			if (_instance == null)
			{
				_instance = this;
				DontDestroyOnLoad(this);
			}
            else
                Destroy(this);
        }

		/// <summary>
		/// Collect current user information
		/// </summary>
		void Start()
		{
			_currentUser = UserManager.Instance.CurrentUser;
		}

		/// <summary>
		/// On double tapping in blind mode, perform the action that belongs to the button 
		/// (e.g. switching scenes, selecting next/previous element in list). 
		/// </summary>
		/// <param name="button"></param>
		/// Corresponds to the button that is clicked
		/// <param name="audio"></param>
		/// Corresponds to the audio source that is available in the environment that is used. 
		public void OndoubleClick(string button, AudioSource audio)
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
			else if (button.Equals("Settings"))
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

		/// <summary>
		/// On single tap, produce speech to inform the user what whill happen if the same button would be tapped twice. 
		/// </summary>
		/// <param name="button"></param>
		/// <param name="audio"></param>
		public void OnSingleClick(string button, AudioSource audio)
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
			{
				_currentUser = UserManager.Instance.CurrentUser;
				StartCoroutine(DownloadTheAudio("Aantal levens is " + _currentUser.NrOfLives.ToString(), audio));
			}
			else if (button.Equals("ContextButton"))
				StartCoroutine(DownloadTheAudio("Context is " + QuizManager.Instance.GetContext, audio));
			else if (button.Equals("ScoreButton"))
				StartCoroutine(DownloadTheAudio("Score is " + QuizManager.Instance.GetScore.ToString(), audio));
			else if (button.Equals("LivesCharge"))
				StartCoroutine(DownloadTheAudio("Geladen voor " + LivesManagement.GetValue(), audio));

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
}