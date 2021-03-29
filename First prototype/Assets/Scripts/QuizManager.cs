using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;
using Happify.Client;
using System.Text.RegularExpressions;

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

	public TextMeshProUGUI QuestionTxt;
	public TextMeshProUGUI ScoreTxt;
	public TextMeshProUGUI Lives;
	public TextMeshProUGUI LevelCompleteTxt;
	public static int livesNr = 3;

	public bool WrongActive = false;
	public bool CorrectActive = false;

	public int score = 0;


	//Get sound effects
	public AudioSource CorrectSound;
	public AudioSource IncorrectSound;

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

	public static int UserSkill;

	public void Start()
	{
		LevelIndex = LevelSwiper.GetLevel();
		if (LevelIndex != 5)
		{
			PatternsComplete = Resources.LoadAll("ScriptableObjects/SO_Emotions", typeof(SOPattern));
			UserSkill = UserCreator.user1.EmotionsLevel;
			Topic = "Emoties en sfeer";
		}
		else
		{
			PatternsComplete = Resources.LoadAll("ScriptableObjects/SO_General", typeof(SOPattern));
			UserSkill = UserCreator.user1.GeneralLevel;
			Topic = "Algemeen";
		}

		GoScreen.SetActive(false);
		LevelCompletePanel.SetActive(false);
		ScoreTxt.GetComponent<TextMeshProUGUI>().text = "Score: " + score;

		//Create Q&A set
		for (int i = 0; i < PatternsComplete.Length; i++)
		{
			if ((PatternsComplete[i] as SOPattern).difficulty <= UserSkill)
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
						(PatternsComplete[listNumbers[0]] as SOPattern).patternImage,
						(PatternsComplete[listNumbers[1]] as SOPattern).patternImage,
						(PatternsComplete[listNumbers[2]] as SOPattern).patternImage,
						(PatternsComplete[i] as SOPattern).patternImage,
						};

				//Randomize answer options order
				for (int k = 0; k < AnswerOptions.Length - 1; k++)
				{
					int l = r.Next(k, AnswerOptions.Length);
					Sprite temp = AnswerOptions[k];
					AnswerOptions[k] = AnswerOptions[l];
					AnswerOptions[l] = temp;
				}

				//Add question with options to the list of questions and answers
				QnA.Add(new QuestionAndAnswers { Question = (PatternsComplete[i] as SOPattern).patternName, Answers = AnswerOptions, CorrectAnswer = 1 + Array.IndexOf(AnswerOptions, (PatternsComplete[i] as SOPattern).patternImage), Json = (PatternsComplete[i] as SOPattern).patternJson });
			}
		}
		GenerateQuestion();
    }
	
	private void Update()
	{
		
		Lives.GetComponent<TextMeshProUGUI>().text = livesNr.ToString();
		if (livesNr == 0)
			GoScreen.SetActive(true);
	}
	

    public void Correct()
    {
        //If correct answer was chosen
		//Play sound effect	
		if (SettingsHandler.remainingHearing)
			CorrectSound.Play();
		//add 1 to score
		score++;
		ScoreTxt.GetComponent<TextMeshProUGUI>().text = "Score: " + score;
		//Remove question from list
		QnA.RemoveAt(CurrentQuestion);
		//Activate green screen with check mark
		if (SettingsHandler.remainingVision){
			CorrectPanel.SetActive(true);
			CorrectActive = true;
		}
		//Start Coroutine to deactivate the green screen and initiate a new question
		StartCoroutine(WaitForNext());
    }

    public void Wrong()
    {
		//If wrong answer was chosen
		score--;
		ScoreTxt.GetComponent<TextMeshProUGUI>().text = "Score: " + score;
		//Play sound effect
		//if (SettingsHandler.remainingHearing)
		if (UserCreator.user1.RemainingHearing)
			IncorrectSound.Play();
		//Remove 1 life
		//livesNr -= 1;
		UserCreator.user1.NrOfLives -= 1;
		//Show red screen with cross
		if (UserCreator.user1.RemainingVision){
			WrongPanel.SetActive(true);
			WrongActive = true;
		}
		//Start coroutine to deactive the red screen and initate a new question
        StartCoroutine(WaitForNext());
    }

    IEnumerator WaitForNext()
    {
		// Wait for 1.5 second (deactive active screen) and then offer new question. 
        yield return new WaitForSeconds(1.5f);
		if (WrongActive){
			WrongActive = false;
			WrongPanel.SetActive(false);
		} else if (CorrectActive){
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
			Options[i].GetComponent<AnswerScript>().isCorrect = false;
			// Change text of option buttons to text of possible answers
			Options[i].transform.GetChild(0).GetComponent<Image>().sprite = QnA[CurrentQuestion].Answers[i];

			// Set answer to be the correct one if it has been indicated as corrrect answer to the question.
			if (QnA[CurrentQuestion].CorrectAnswer == i + 1)
				Options[i].GetComponent<AnswerScript>().isCorrect = true;
        }
    }

	//Select new question
    void GenerateQuestion()
    {
		Debug.Log(QnA.Count);

		if (livesNr == 0){
			Debug.Log("You ran out of lives. Please wait till you have a new one before you continue");
			// livesManagement.increaseLives();
		}
		else if(QnA.Count > 0)
        {	
			// Select question randomly from list of questions
            CurrentQuestion = Random.Range(0, QnA.Count);

            // Set question text (i.e. context)
			QuestionTxt.text = QnA[CurrentQuestion].Question;
            // Connect possible answers to question to the buttons 
            SetAnswers();
			PlayPattern();
		}
		else
        {
			//Level Complete
            ScoreManager.UpdateScore(score);
			////Check for xp badges
			//if (ScoreManager.TotalXP >= 100)
			//	AchievementsCollector.AddXPBadge(4);
			//else if (ScoreManager.TotalXP >= 50)
			//	AchievementsCollector.AddXPBadge(3);
			//else if (ScoreManager.TotalXP >= 25)
			//	AchievementsCollector.AddXPBadge(2);
			//else if (ScoreManager.TotalXP >= 10)
			//	AchievementsCollector.AddXPBadge(1);

			Debug.Log("Out of Questions");
			Debug.Log(Topic);
			Debug.Log(UserSkill);
			LevelCompleteTxt.text = AchievementsCollector.PopUpAchievement(Topic, UserSkill);

			if (Topic == "Emoties en sfeer")
				UserCreator.user1.EmotionsLevel++;
			else
				UserCreator.user1.GeneralLevel++;
			LevelCompletePanel.SetActive(true);
			

			// if (livesNr < 3){
				// livesManagement.increaseLives();
			// }
        }
    }

	public async void PlayPattern()
	{
		string json = QnA[CurrentQuestion].Json.text;
		json = Regex.Replace(json, @"\t|\n|\r", "");
		json = json.Replace(" ", "");
		await MqttService.Instance.PublishAsync("happify/tactile-board/test", json);
	}

	public static int GetLives()
	{
		return livesNr;
	}
}