using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Random = UnityEngine.Random;
using Happify.Client;


public class QuizManager : MonoBehaviour
{
    public List<QuestionAndAnswers> QnA;
    public GameObject[] options;
    public int currentQuestion;

    public GameObject Quizpanel;
	public GameObject wrongPanel;
	public GameObject correctPanel;
	public GameObject goScreen;

    public TextMeshProUGUI QuestionTxt;
    public TextMeshProUGUI ScoreTxt;
	public TextMeshProUGUI lives;
	public static int livesNr = 3;
	
	public bool wrongActive = false;
	public bool correctActive = false; 

    public int score;

	
	//Get sound effects
	public AudioSource correctSound;
	public AudioSource incorrectSound;

	public object[] patternsComplete;
	
	public static int levelIndex;

	public System.Random r = new System.Random();	

    public async void Awake()
	{
		//Ensure sounds don't play at start
		this.correctSound.playOnAwake = false;
		this.incorrectSound.playOnAwake = false;
		await MqttService.Instance.ConnectAsync();
		await MqttService.Instance.PublishAsync("suitceyes/tactile-board/test", "Hello World");
	}

	public void Start()
    {
		levelIndex = levelSwiper.getLevel();
		if (levelIndex != 5)
			patternsComplete = Resources.LoadAll("ScriptableObjects/SO_Emotions", typeof(SOPattern));
		else
			patternsComplete = Resources.LoadAll("ScriptableObjects/SO_General", typeof(SOPattern));

		goScreen.SetActive(false);
		
		//Create Q&A set
		for (int i = 0; i < patternsComplete.Length; i++)
		{
			List<int> listNumbers = new List<int>();
			int number;
			//For each element in the list of emotion patterns, grab 3 random integers to select answer options randomly
			for (int j = 0; j < 3; j++)
			{
				do
				{
					number = r.Next(1,  patternsComplete.Length);
				} while (listNumbers.Contains(number) || number == i);
				
				listNumbers.Add(number);
			}
			//Select answer options using the integers generated before this 
			Sprite[] answerOptions = 
				{
				(patternsComplete[listNumbers[0]] as SOPattern).patternImage,
				(patternsComplete[listNumbers[1]] as SOPattern).patternImage,
				(patternsComplete[listNumbers[2]] as SOPattern).patternImage,
				(patternsComplete[i] as SOPattern).patternImage,
				};

			//Randomize answer options order
			for (int k = 0; k < answerOptions.Length - 1; k++)
			{
				int l = r.Next(k, answerOptions.Length);
				Sprite temp = answerOptions[k];
				answerOptions[k] = answerOptions[l];
				answerOptions[l] = temp;
			}

			//Add question with options to the list of questions and answers
			QnA.Add( new QuestionAndAnswers {Question = (patternsComplete[i] as SOPattern).patternName, Answers = answerOptions, CorrectAnswer = 1+Array.IndexOf(answerOptions, (patternsComplete[i] as SOPattern).patternImage)});
		}
		generateQuestion();
    }
	
	private void Update()
	{
		ScoreTxt.GetComponent<TextMeshProUGUI>().text = "Score: "+ score;
		lives.GetComponent<TextMeshProUGUI>().text = livesNr.ToString();
		if (livesNr == 0)
			goScreen.SetActive(true);
	}
	

    public void correct()
    {
        //If correct answer was chosen
		//Play sound effect	
		if (settingsHandler.remainingHearing == true)
			correctSound.Play();
		//add 1 to score
		score += 1;
		//Remove question from list
        QnA.RemoveAt(currentQuestion);
		//Activate green screen with check mark
		if (settingsHandler.remainingVision == true){
			correctPanel.SetActive(true);
			correctActive = true;
		}
		//Start Coroutine to deactivate the green screen and initiate a new question
        StartCoroutine(waitForNext());
    }

    public void wrong()
    {
        //If wrong answer was chosen
		//Play sound effect
		if (settingsHandler.remainingHearing == true)
			incorrectSound.Play();
		//Remove 1 life
		livesNr -= 1; 
        // QnA.RemoveAt(currentQuestion);
		//Show red screen with cross
		if (settingsHandler.remainingVision == true){
			wrongPanel.SetActive(true);
			wrongActive = true;
		}
		//Start coroutine to deactive the red screen and initate a new question
        StartCoroutine(waitForNext());
    }

    IEnumerator waitForNext()
    {
		// Wait for 1.5 second (deactive active screen) and then offer new question. 
        yield return new WaitForSeconds(1.5f);
		if (wrongActive == true){
			wrongActive = false;
			wrongPanel.SetActive(false);
		} else if (correctActive == true){
			correctActive = false;
			correctPanel.SetActive(false);
		}
        generateQuestion();
    }

    void SetAnswers()
    {
		// From given answer options
        for (int i = 0; i < options.Length; i++)
        {
            // options[i].GetComponent<Image>().color = options[i].GetComponent<AnswerScript>().startColor;
			// By default set answer to be incorrect
            options[i].GetComponent<AnswerScript>().isCorrect = false;
			// Change text of option buttons to text of possible answers
            options[i].transform.GetChild(0).GetComponent<Image>().sprite = QnA[currentQuestion].Answers[i];

			// Set answer to be the correct one if it has been indicated as corrrect answer to the question.
			if (QnA[currentQuestion].CorrectAnswer == i + 1)
				options[i].GetComponent<AnswerScript>().isCorrect = true;
        }
    }

	//Select new question
    void generateQuestion()
    {
		playPattern();
		if (livesNr == 0){
			Debug.Log("You ran out of lives. Please wait till you have a new one before you continue");
			// livesManagement.increaseLives();
		}
		else if(QnA.Count > 0)
        {	
			// Select question randomly from list of questions
            currentQuestion = Random.Range(0, QnA.Count);

            // Set question text (i.e. context)
			QuestionTxt.text = QnA[currentQuestion].Question;
			// Connect possible answers to question to the buttons 
            SetAnswers();
        }
        else
        {
			//Indicate that there are no questions left -> Level complete
            ScoreManager.updateScore(score);
			Debug.Log("Out of Questions");
			// if (livesNr < 3){
				// livesManagement.increaseLives();
			// }
        }
    }

	void playPattern()
    {
		
	}
	
	public static int getLives()
	{
		return livesNr;
	}
	
	public void goToHome() 
	{  
        SceneManager.LoadScene("scn_MainMenu");  
    } 
	

}