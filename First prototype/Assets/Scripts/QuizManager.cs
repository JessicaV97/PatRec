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

public class QuizManager : MonoBehaviour
{
	public List<QuestionAndAnswers> QnA;

	public GameObject[] Options;
	public int CurrentQuestion;

	public GameObject Quizpanel;
	public GameObject WrongPanel;
	public GameObject CorrectPanel;
	public GameObject GoScreen;
	public GameObject LevelCompletePanel;
	public GameObject XPBadgePanel;

	public TextMeshProUGUI QuestionTxt;
	public TextMeshProUGUI ScoreTxt;
	public TextMeshProUGUI Lives;
	public TextMeshProUGUI LevelCompleteTxt;
	public TextMeshProUGUI XPBadgeTxt;

	public bool WrongActive = false;
	public bool CorrectActive = false;

	private int _score = 0;

	//Get sound effects
	public AudioSource CorrectSound;
	public AudioSource IncorrectSound;
	public AudioSource _audio;

	public object[] PatternsComplete;

	public static int LevelIndex;
	public static string Topic;

	public System.Random r = new System.Random();



	public async void Awake()
	{
		//Ensure sounds don't play at start
		this.CorrectSound.playOnAwake = false;
		this.IncorrectSound.playOnAwake = false;
		await MqttService.Instance.ConnectAsync();
	}

	public static Level UserSkill;

	public void Start()
	{
		LevelIndex = LevelSwiper.GetLevel();
		if (LevelIndex != 5)
		{
			PatternsComplete = Resources.LoadAll("ScriptableObjects/SO_Emotions", typeof(SOPattern));
			UserSkill = UserCreator.User1.EmotionsLevel;
			Topic = "Emoties en sfeer";
		}
		else
		{
			PatternsComplete = Resources.LoadAll("ScriptableObjects/SO_General", typeof(SOPattern));
			UserSkill = UserCreator.User1.GeneralLevel;
			Topic = "Algemeen";
		}

		GoScreen.SetActive(false);
		LevelCompletePanel.SetActive(false);
		ScoreTxt.GetComponent<TextMeshProUGUI>().text = "Score: " + _score;

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
						number = r.Next(1, PatternsComplete.Length);
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
				for (int k = 0; k < AnswerOptions.Length - 1; k++)
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
				QnA.Add(new QuestionAndAnswers { Question = (PatternsComplete[i] as SOPattern).PatternName, Answers = AnswerOptions, AnswersAudio = AnswerStringsForTTS, CorrectAnswer = 1 + Array.IndexOf(AnswerOptions, (PatternsComplete[i] as SOPattern).PatternImage), Json = (PatternsComplete[i] as SOPattern).PatternJson });
			}
		}
		GenerateQuestion();
	}

	public void Correct()
	{
		//If correct answer was chosen
		//Play sound effect	
		if (UserCreator.User1.RemainingHearing)
			CorrectSound.Play();
		//add 1 to score
		_score++;
		ScoreTxt.GetComponent<TextMeshProUGUI>().text = "Score: " + _score;
		//Remove question from list
		QnA.RemoveAt(CurrentQuestion);
		//Activate green screen with check mark
		if (UserCreator.User1.RemainingVision)
		{
			CorrectPanel.SetActive(true);
			CorrectActive = true;
		}
		//Start Coroutine to deactivate the green screen and initiate a new question
		StartCoroutine(WaitForNext());
	}

	public void Wrong()
	{
		//If wrong answer was chosen
		_score--;
		ScoreTxt.GetComponent<TextMeshProUGUI>().text = "Score: " + _score;
		//Play sound effect
		//if (SettingsHandler.remainingHearing)
		if (UserCreator.User1.RemainingHearing)
			IncorrectSound.Play();
		//Remove 1 life
		//livesNr -= 1;
		UserCreator.User1.NrOfLives -= 1;
		Lives.GetComponent<TextMeshProUGUI>().text = UserCreator.User1.NrOfLives.ToString();
		//Show red screen with cross
		if (UserCreator.User1.RemainingVision)
		{
			WrongPanel.SetActive(true);
			WrongActive = true;
		}
		if (UserCreator.User1.NrOfLives == 0)
			GoScreen.SetActive(true);
		//Start coroutine to deactive the red screen and initate a new question
		StartCoroutine(WaitForNext());
	}

	IEnumerator WaitForNext()
	{
		// Wait for 1.5 second (deactive active screen) and then offer new question. 
		yield return new WaitForSeconds(1.5f);
		if (WrongActive)
		{
			WrongActive = false;
			WrongPanel.SetActive(false);
		}
		else if (CorrectActive)
		{
			CorrectActive = false;
			CorrectPanel.SetActive(false);
		}
		GenerateQuestion();
	}

	void SetAnswers()
	{
		// From given answer options
		for (int i = 0; i < Options.Length; i++)
		{
			// options[i].GetComponent<Image>().color = options[i].GetComponent<AnswerScript>().startColor;
			// By default set answer to be incorrect
			Options[i].GetComponent<AnswerScript>().IsCorrect = false;
			// Change text of option buttons to text of possible answers
			Options[i].transform.GetChild(0).GetComponent<Image>().sprite = QnA[CurrentQuestion].Answers[i];
			Options[i].GetComponent<AnswerScript>().AudioName = QnA[CurrentQuestion].AnswersAudio[i];

			// Set answer to be the correct one if it has been indicated as corrrect answer to the question.
			if (QnA[CurrentQuestion].CorrectAnswer == i + 1)
				Options[i].GetComponent<AnswerScript>().IsCorrect = true;
		}
	}

	//Select new question
	void GenerateQuestion()
	{

		if (UserCreator.User1.NrOfLives == 0)
		{
			Debug.Log("You ran out of lives. Please wait till you have a new one before you continue");
			// livesManagement.increaseLives();
		}
		else if (QnA.Count > 0)
		{
			// Select question randomly from list of questions
			CurrentQuestion = Random.Range(0, QnA.Count);

			// Connect possible answers to question to the buttons 
			SetAnswers();
			if (!UserCreator.User1.RemainingVision && UserCreator.User1.RemainingHearing)
			{
				QuestionTxt.text = "... ";
				StartCoroutine(DownloadTheAudio(QnA[CurrentQuestion].Question));
			}
			else
			{
				// Set question text (i.e. context)
				QuestionTxt.text = QnA[CurrentQuestion].Question;
			}
			PlayPattern();
		}
		else
		{
			Debug.Log("Level Complete");
			//Level Complete
			ScoreManager.UpdateScore(_score);

			////Check for xp badges
			if (ScoreManager.TotalXP >= 100)
			{
				XPBadgeTxt.text = AchievementsCollector.AddXpBadge(4);
				XPBadgePanel.SetActive(true);
				if (!UserCreator.User1.RemainingVision && UserCreator.User1.RemainingHearing)
					ReadPattern(XPBadgeTxt.text);
			}
			else if (ScoreManager.TotalXP >= 50)
			{
				XPBadgeTxt.text = AchievementsCollector.AddXpBadge(3);
				XPBadgePanel.SetActive(true);
				if (!UserCreator.User1.RemainingVision && UserCreator.User1.RemainingHearing)
					ReadPattern(XPBadgeTxt.text);
			}
			else if (ScoreManager.TotalXP >= 25)
			{
				XPBadgeTxt.text = AchievementsCollector.AddXpBadge(2);
				XPBadgePanel.SetActive(true);
				if (!UserCreator.User1.RemainingVision && UserCreator.User1.RemainingHearing)
					ReadPattern(XPBadgeTxt.text);
			}
			else if (ScoreManager.TotalXP >= 10)
			{
				XPBadgeTxt.text = AchievementsCollector.AddXpBadge(1);
				XPBadgePanel.SetActive(true);
				if (!UserCreator.User1.RemainingVision && UserCreator.User1.RemainingHearing)
					ReadPattern(XPBadgeTxt.text);
			}

			LevelCompleteTxt.text = AchievementsCollector.PopUpAchievement(Topic, UserSkill);
			if (!UserCreator.User1.RemainingVision && UserCreator.User1.RemainingHearing)
				ReadPattern(AchievementsCollector.PopUpAchievement(Topic, UserSkill));

			if (Topic == "Emoties en sfeer")
				UserCreator.User1.EmotionsLevel++;
			else
				UserCreator.User1.GeneralLevel++;
			LevelCompletePanel.SetActive(true);
		}
	}

	public async void PlayPattern()
	{
		string json = QnA[CurrentQuestion].Json.text;
		json = Regex.Replace(json, @"\t|\n|\r", "");
		json = json.Replace(" ", "");
		json = json.Replace("255", "1.0");
		await MqttService.Instance.PublishAsync("happify/tactile-board/test", json);
	}

	public void ClosePanel()
	{
		XPBadgePanel.SetActive(false);
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
	public void ReadPattern(string message)
	{
		StartCoroutine(DownloadTheAudio(message));
	}


}