using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;
using Happify.Client;
using System.Text.RegularExpressions;
using UnityEngine.Networking;
using Happify.User;
using Happify.Levels;

/// <summary>
/// Class that handles the quiz element of the app. Found in 'scn_MainGameScreen'. 
/// </summary>
public class QuizManager : MonoBehaviour
{
	private int _currentQuestion;

	//Collect game objects used in this script
	[SerializeField]
	private GameObject[] _options;
	[SerializeField]
	public List<QuestionAndAnswers> _QnA;
	[SerializeField]
	private GameObject _quizPanel;
	[SerializeField]
	private GameObject _wrongPanel;
	[SerializeField]
	private GameObject _correctPanel;
	[SerializeField]
	private GameObject _gameOverScreen;
	[SerializeField]
	private GameObject _levelCompletePanel;
	[SerializeField]
	private GameObject _xpBadgePanel;
	[SerializeField]
	private TextMeshProUGUI _questionText;
	[SerializeField]
	private TextMeshProUGUI _scoreText;
	[SerializeField]
	private TextMeshProUGUI _numberOfLives;
	[SerializeField]
	private TextMeshProUGUI _levelCompleteText;
	[SerializeField]
	private TextMeshProUGUI _xpBadgeText;
	[SerializeField]
	private TextMeshProUGUI _contextText;
	//Get sound effects
	[SerializeField]
	private AudioSource _correctSound;
	[SerializeField]
	private AudioSource _incorrectSound;
	[SerializeField]
	private AudioSource _audio;

	private static int _badgesEarned = 0;

	private bool _wrongActive = false;
	private bool _correctActive = false;

	private int _score = 0;

	private object[] PatternsComplete;

	public static int LevelIndex;
	public static string Topic;

	public System.Random r = new System.Random();

	private UserDescription currentUser;

	private static QuizManager _instance;
	public static QuizManager Instance => _instance;
	public int GetScore => _score;
	public string GetContext => Topic;

	public static Level UserSkill;

	/// <summary>
	/// Create instance of QuizManager and create an MQTT connection.
	/// </summary>
	public async void Awake()
	{
		if (_instance == null)
		{
			_instance = this;
        }

		//Ensure sounds don't play at start
		_correctSound.playOnAwake = false;
		_incorrectSound.playOnAwake = false;
		await MqttService.Instance.ConnectAsync();
	}

	/// <summary>
	/// Set other panels inactive so quiz is visible.
	/// Collect current user information and provide info on screen (number of lives).
	/// Collect information and data on the context that has been selected. 
	/// Create list of questions. 
	/// Randomize order of answer options. 
	/// </summary>
	public void Start()
	{
		_levelCompletePanel.SetActive(false);
		_gameOverScreen.SetActive(false);
		_levelCompletePanel.SetActive(false);

		currentUser = UserManager.Instance.CurrentUser;
		_numberOfLives.text = currentUser.NrOfLives.ToString();
		LevelIndex = LevelSwiper.GetLevel();

		if (LevelIndex != 1)
		{
			PatternsComplete = Resources.LoadAll("ScriptableObjects/SO_Emotions", typeof(SOPattern));
			UserSkill = currentUser.EmotionsLevel;
			Topic = "Emoties en sfeer";
			_contextText.text = "Emoties en sfeer";
		}
		else
		{
			PatternsComplete = Resources.LoadAll("ScriptableObjects/SO_General", typeof(SOPattern));
			UserSkill = currentUser.GeneralLevel;
			Topic = "Algemeen";
			_contextText.text = "Algemeen";
		}

		_scoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + _score;

		//Create Q&A set
		for (int i = 0; i < PatternsComplete.Length; i++)
		{
			if ((PatternsComplete[i] as SOPattern).Difficulty <= UserSkill)
			{
				List<int> listNumbers = new List<int>();
				int number;
				//For each element in the list of emotion patterns, grab 3 random integers to select answer options randomly
				for (int j = 0; j < 3; j++)
				{
					do
					{
						number = r.Next(0, PatternsComplete.Length);
					} while (listNumbers.Contains(number) || number == i);

					listNumbers.Add(number);
				}
				//Select answer options using the integers generated before this 
				Sprite[] AnswerOptions =
					{
						(PatternsComplete[listNumbers[0]] as SOPattern).PatternImage,
						(PatternsComplete[listNumbers[1]] as SOPattern).PatternImage,
						(PatternsComplete[listNumbers[2]] as SOPattern).PatternImage,
						(PatternsComplete[i] as SOPattern).PatternImage,
						};

				string[] AnswerStringsForTTS =
					{
						(PatternsComplete[listNumbers[0]] as SOPattern).PatternName,
						(PatternsComplete[listNumbers[1]] as SOPattern).PatternName,
						(PatternsComplete[listNumbers[2]] as SOPattern).PatternName,
						(PatternsComplete[i] as SOPattern).PatternName,
						};


				//Randomize answer options order
				for (int k = 0; k < AnswerOptions.Length; k++)
				{
					int l = r.Next(k, AnswerOptions.Length);
					Sprite temp = AnswerOptions[k];
					string temp2 = AnswerStringsForTTS[k];
					AnswerOptions[k] = AnswerOptions[l];
					AnswerStringsForTTS[k] = AnswerStringsForTTS[l];
					AnswerOptions[l] = temp;
					AnswerStringsForTTS[l] = temp2;
				}

				//Add question with options to the list of questions and answers
				_QnA.Add(new QuestionAndAnswers { Question = (PatternsComplete[i] as SOPattern).PatternName, Answers = AnswerOptions, AnswersAudio = AnswerStringsForTTS, CorrectAnswer = 1 + Array.IndexOf(AnswerOptions, (PatternsComplete[i] as SOPattern).PatternImage), Json = (PatternsComplete[i] as SOPattern).PatternJson });
			}
		}
		GenerateQuestion();
	}

	/// <summary>
	/// A correct answer was chosen so show a green screen and play sound effect (based on settings).
	/// Update score and remove question from list of questions. 
	/// </summary>
	public void Correct()
	{
		if (currentUser.RemainingHearing)
			_correctSound.Play();

		_score++;
		_scoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + _score;

		_QnA.RemoveAt(_currentQuestion);
		_correctPanel.SetActive(true);
		_correctActive = true;

		//Start Coroutine to deactivate the green screen and initiate a new question
		StartCoroutine(WaitForNext());
	}

	/// <summary>
	/// An incorrect answer was chosen so show a red screen and play sound effect (based on settings).
	/// Update score. Remove a live. 
	/// In case the user has 0 lives, end the game. 
	/// </summary>
	public void Wrong()
	{
		currentUser = UserManager.Instance.CurrentUser;

		//Play sound effect
		if (currentUser.RemainingHearing)
			_incorrectSound.Play();

		currentUser.NrOfLives--;

		_score--;
		_scoreText.text = "Score: " + _score;
		_numberOfLives.text = UserManager.Instance.CurrentUser.NrOfLives.ToString();

		_wrongPanel.SetActive(true);
		_wrongActive = true;

		if (currentUser.NrOfLives == 0)
		{
			_gameOverScreen.SetActive(true);
			currentUser.NrOfLives = 0;
            UserManager.Instance.Save();
			return; 
        }

		//Start coroutine to deactive the red screen and initate a new question
		StartCoroutine(WaitForNext());

	}

	/// <summary>
	/// Handles the temporary activation of a panel showing the user whether he answered correctly or not. 
	/// Probes the next question. 
	/// </summary>
	/// <returns></returns>
	IEnumerator WaitForNext()
	{
		// Wait for 1.5 second (deactive active screen) and then offer new question. 
		yield return new WaitForSeconds(1.5f);
		if (_wrongActive)
		{
			_wrongActive = false;
			_wrongPanel.SetActive(false);
		}
		else if (_correctActive)
		{
			_correctActive = false;
			_correctPanel.SetActive(false);
		}
		GenerateQuestion();
	}

	/// <summary>
	/// This fuction handles showing the right visualizations of the answer options and providing TTS with the names of the answer options. 
	/// In blind mode it will show empty body visualizations. 
	/// </summary>
	void SetAnswers()
	{
		// From given answer options
		for (int i = 0; i < _options.Length; i++)
		{
			// By default set answer to be incorrect
			_options[i].GetComponent<AnswerScript>().IsCorrect = false;
			// Change text of option buttons to text of possible answers
			if (currentUser.RemainingVision)
				_options[i].transform.GetChild(0).GetComponent<Image>().sprite = _QnA[_currentQuestion].Answers[i];
			else
				_options[i].transform.GetChild(0).GetComponent<Image>().sprite = (Sprite) Resources.Load("sprt_empty", typeof(Sprite));
			_options[i].GetComponent<AnswerScript>().AudioName = _QnA[_currentQuestion].AnswersAudio[i];

			// Set answer to be the correct one if it has been indicated as corrrect answer to the question.
			if (_QnA[_currentQuestion].CorrectAnswer == i + 1)
				_options[i].GetComponent<AnswerScript>().IsCorrect = true;
		}
	}

	/// <summary>
	/// Select new question, probes the playPattern function. 
	/// In case there are no questions left, the score will be added to the total number of experience points. 
	/// The app will check whether the user has earned another badge based on experience points (hard coded).
	/// In case a badge was earned, another panel will be activated. Otherwise a panel will be shown for completing a level.
	/// The user's level of experience in the given context will be increased with 1. 
	/// </summary>
	void GenerateQuestion()
	{
		if (_QnA.Count > 0)
		{
			// Select question randomly from list of questions
			_currentQuestion = Random.Range(0, _QnA.Count);

			// Connect possible answers to question to the buttons 
			SetAnswers();
			if (!currentUser.RemainingVision && currentUser.RemainingHearing)
			{
				_questionText.text = "... ";
			}
			else
				_questionText.text = _QnA[_currentQuestion].Question;
			PlayPattern();
		}
		else
		{
			//Level Complete
			currentUser = UserManager.Instance.CurrentUser;
			ScoreManager.TotalXP = currentUser.ExperiencePoints;
			ScoreManager.UpdateScore(_score);
			currentUser.ExperiencePoints = ScoreManager.TotalXP;
			UserManager.Instance.Save();

			////Check for xp badges
			if (ScoreManager.TotalXP >= 100 && _badgesEarned < 4)
			{
				_xpBadgeText.text = AchievementsCollector.AddXpBadge(4);
				_xpBadgePanel.SetActive(true);
				if (!currentUser.RemainingVision && currentUser.RemainingHearing)
					ReadPattern(_xpBadgeText.text);
				_badgesEarned++;
			}
			else if (ScoreManager.TotalXP >= 50 && _badgesEarned < 3)
			{
				_xpBadgeText.text = AchievementsCollector.AddXpBadge(3);
				_xpBadgePanel.SetActive(true);
				if (!currentUser.RemainingVision && currentUser.RemainingHearing)
					ReadPattern(_xpBadgeText.text);
				_badgesEarned++;
			}
			else if (ScoreManager.TotalXP >= 25 && _badgesEarned < 2)
			{
				_xpBadgeText.text = AchievementsCollector.AddXpBadge(2);
				_xpBadgePanel.SetActive(true);
				if (!currentUser.RemainingVision && currentUser.RemainingHearing)
					ReadPattern(_xpBadgeText.text);
				_badgesEarned++;
			}
			else if (ScoreManager.TotalXP >= 10 && _badgesEarned < 1)
			{
				_xpBadgeText.text = AchievementsCollector.AddXpBadge(1);
				_xpBadgePanel.SetActive(true);
				if (!currentUser.RemainingVision && currentUser.RemainingHearing)
					ReadPattern(_xpBadgeText.text);
				_badgesEarned++;
			}

			_levelCompleteText.text = AchievementsCollector.PopUpAchievement(Topic, UserSkill);
			if (!currentUser.RemainingVision && currentUser.RemainingHearing)
				ReadPattern(AchievementsCollector.PopUpAchievement(Topic, UserSkill));

			if (Topic == "Emoties en sfeer")
				currentUser.EmotionsLevel++;
				
			else
				currentUser.GeneralLevel++;
			UserManager.Instance.Save();
			_levelCompletePanel.SetActive(true);
		}
	}

	/// <summary>
	/// Send message to client (chairable) to play the pattern. Provide string on screen to see what is being played. 
	/// Small json adoptations to match the expectations of the listener or improve readability. 
	/// </summary>
	public async void PlayPattern()
	{
		string json = _QnA[_currentQuestion].Json.text;
		json = Regex.Replace(json, @"\t|\n|\r", "");
		json = json.Replace(" ", "");
		json = json.Replace("255", "1.0");
		json = json.Replace("islooped", "isLooped");
		await MqttService.Instance.PublishAsync("happify/play", json);
	}

	/// <summary>
	/// Deactivates XP badge panel. 
	/// </summary>
	public void ClosePanel()
	{
		_xpBadgePanel.SetActive(false);
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
	public void ReadPattern(string message)
	{
		StartCoroutine(DownloadTheAudio(message));
	}
}