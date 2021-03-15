using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using TMPro;


public class studyManager : MonoBehaviour
{
	private List<PatternComplete> emotionsComplete = new List<PatternComplete>();
	
	//Get objects for study environment
	public GameObject patternVisual;
	public TextMeshProUGUI patternTitle;
	public int patternIndex = 0;


	public void Start()
    {
		emotionsComplete = buttonHandler.getEmotionsList();
    }
	
	private void Update(){
		patternVisual.GetComponent<Image>().sprite = emotionsComplete[patternIndex].patternImage;
		patternTitle.GetComponent<TextMeshProUGUI>().text = emotionsComplete[patternIndex].patternName;
	}

	public void nextPattern(){
		if (patternIndex == emotionsComplete.Count - 1){
			patternIndex = 0; 
		} else {
			patternIndex += 1;
		}
	}
	
	public void previousPattern(){
		if (patternIndex == 0){ 
			patternIndex = emotionsComplete.Count - 1;
		} else {
			patternIndex -= 1;
		}
	}
	
	public void goToLevels() {  
        SceneManager.LoadScene("levels");  
    } 
	
	
}