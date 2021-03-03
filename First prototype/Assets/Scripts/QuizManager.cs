using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

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

    private void Start()
    {
        //Get number of questions
		totalQuestions = QnA.Count;
		// Start with a question
        generateQuestion();
		
		wrongPanel.SetActive(false);
		correctPanel.SetActive(false);
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
		// Wait for 1 second and then offer new question. 
        yield return new WaitForSeconds(1.0f);
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
