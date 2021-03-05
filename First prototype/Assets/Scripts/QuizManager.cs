using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using TMPro;
using Random = UnityEngine.Random;

public class QuizManager : MonoBehaviour
{
    public List<QuestionAndAnswers> QnA;
    public GameObject[] options;
    public int currentQuestion;

    public GameObject Quizpanel;
	public GameObject wrongPanel;
	public GameObject correctPanel;

    public TextMeshProUGUI QuestionTxt;
    public TextMeshProUGUI ScoreTxt;
	
	public bool wrongActive = false;
	public bool correctActive = false; 

    int totalQuestions = 0;
    public int score;
	
	//Get json's from inspector
	public TextAsset alarm;
	public TextAsset alarm2;
	public TextAsset angry;
	public TextAsset applause;
	public TextAsset end;
	public TextAsset laughing;
	public TextAsset question;
	public TextAsset silence;
	
	//Initiate emotion patterns
	public Pattern alarmPattern;
	public Pattern alarm2Pattern;
	public Pattern angryPattern;
	public Pattern applausePattern;
	public Pattern endPattern;
	public Pattern laughingPattern;
	public Pattern questionPattern;
	public Pattern silencePattern;
	
	//Get sprites from inspector
	public Sprite alarmPatternIm;
	public Sprite alarm2PatternIm;
	public Sprite angryPatternIm;
	public Sprite applausePatternIm;
	public Sprite endPatternIm;
	public Sprite laughingPatternIm;
	public Sprite questionPatternIm;
	public Sprite silencePatternIm;

	public List<Pattern> emotions = new List<Pattern>();
	private List<PatternComplete> emotionsComplete = new List<PatternComplete>();

	
	public System.Random r = new System.Random();	

    public void Start()
    {
		//Herschrijven tot voor json bestand in mapje doe json.convert.DeserializeObject<Pattern>(naam.text)
		alarmPattern = JsonConvert.DeserializeObject<Pattern>(alarm.text);
		emotions.Add(alarmPattern);
		alarm2Pattern = JsonConvert.DeserializeObject<Pattern>(alarm2.text);
		emotions.Add(alarm2Pattern);
		angryPattern = JsonConvert.DeserializeObject<Pattern>(angry.text);
		emotions.Add(angryPattern);
		applausePattern = JsonConvert.DeserializeObject<Pattern>(applause.text);
		endPattern = JsonConvert.DeserializeObject<Pattern>(end.text);
		laughingPattern = JsonConvert.DeserializeObject<Pattern>(laughing.text);
		questionPattern = JsonConvert.DeserializeObject<Pattern>(question.text);
		silencePattern = JsonConvert.DeserializeObject<Pattern>(silence.text);
		
		emotionsComplete.Add(new PatternComplete {patternName = "Alarm", patternJson = alarmPattern, patternImage = alarmPatternIm, difficulty = 1});
		emotionsComplete.Add(new PatternComplete {patternName = "Alarm2", patternJson = alarm2Pattern, patternImage = alarm2PatternIm, difficulty = 1});
		emotionsComplete.Add(new PatternComplete {patternName = "Angry", patternJson = angryPattern, patternImage = angryPatternIm, difficulty = 1});
		emotionsComplete.Add(new PatternComplete {patternName = "Applause", patternJson = applausePattern, patternImage = applausePatternIm, difficulty = 2});
		emotionsComplete.Add(new PatternComplete {patternName = "End", patternJson = endPattern, patternImage = endPatternIm, difficulty = 2});
		emotionsComplete.Add(new PatternComplete {patternName = "Laughing", patternJson = laughingPattern, patternImage = laughingPatternIm, difficulty = 2});
		emotionsComplete.Add(new PatternComplete {patternName = "Silence", patternJson = silencePattern, patternImage = silencePatternIm, difficulty = 3});
		
		//Get number of questions
		totalQuestions = emotionsComplete.Count;
		// Start with a question
        generateQuestion();
		
		wrongPanel.SetActive(false);
		correctPanel.SetActive(false);
		
		
		for (int i = 0; i < emotionsComplete.Count; i++){
			List<int> listNumbers = new List<int>();
			int number;
			
			for (int j = 0; j < 3; j++){
				do {
					number = r.Next(1,  emotionsComplete.Count);
				} while (listNumbers.Contains(number) || number == i);
				listNumbers.Add(number);
			}
			
			Sprite[] answerOptions = {
				emotionsComplete[listNumbers[0]].patternImage,
				emotionsComplete[listNumbers[1]].patternImage,
				emotionsComplete[listNumbers[2]].patternImage,
				emotionsComplete[i].patternImage,
			};

			for (int k = 0; k < answerOptions.Length - 1; k++){
				int l = r.Next(k, answerOptions.Length);
				Sprite temp = answerOptions[k];
				answerOptions[k] = answerOptions[l];
				answerOptions[l] = temp;
			}
			
			QnA.Add( new QuestionAndAnswers {Question = emotionsComplete[i].patternName, Answers = answerOptions, CorrectAnswer = 1+Array.IndexOf(answerOptions, emotionsComplete[i].patternImage)});
		}
    }
	
	private void Update(){
		ScoreTxt.GetComponent<TextMeshProUGUI>().text = "Score: "+ score.ToString();
	}

    public void correct()
    {
        //If correct answer was chosen, add 1 to score and remove question from possible questions. Start coroutine to offer new question.
        score += 1;
        QnA.RemoveAt(currentQuestion);
		correctPanel.SetActive(true);
		correctActive = true;
        StartCoroutine(waitForNext());
    }

    public void wrong()
    {
        //If wrong answer was chosen, start coroutine to offer new question.
        // QnA.RemoveAt(currentQuestion);
		wrongPanel.SetActive(true);
		wrongActive = true;
        StartCoroutine(waitForNext());
    }

    IEnumerator waitForNext()
    {
		// Wait for 1.5 second and then offer new question. 
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
            options[i].GetComponent<Image>().color = options[i].GetComponent<AnswerScript>().startColor;
			// By default set answer to be incorrect
            options[i].GetComponent<AnswerScript>().isCorrect = false;
			// Change text of option buttons to text of possible answers
            options[i].transform.GetChild(0).GetComponent<Image>().sprite = QnA[currentQuestion].Answers[i];
            
            // Set answer to be the correct one if it has been indicated as corrrect answer to the question.
			if(QnA[currentQuestion].CorrectAnswer == i+1)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true;
            }
        }
    }

	//Select new question
    void generateQuestion()
    {
        if(QnA.Count > 0)
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
            Debug.Log("Out of Questions");
        }


    }
}
