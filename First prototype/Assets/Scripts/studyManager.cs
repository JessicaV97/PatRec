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
	private List<PatternComplete> patternsComplete = new List<PatternComplete>();
	
	//Get objects for study environment
	public GameObject patternVisual;
	public TextMeshProUGUI patternTitle;
	public int patternIndex = 0;
	public static int levelIndex;


	public void Start()
    {
		levelIndex = levelSwiper.getLevel();
		if (levelIndex != 5){
			patternsComplete = buttonHandler.getEmotionsList();
		} else {
			patternsComplete = buttonHandler.getGeneralList();
		}
    }
	
	private void Update(){
		patternVisual.GetComponent<Image>().sprite = patternsComplete[patternIndex].patternImage;
		patternTitle.GetComponent<TextMeshProUGUI>().text = patternsComplete[patternIndex].patternName;
	}

	public void nextPattern(){
		if (patternIndex == patternsComplete.Count - 1){
			patternIndex = 0; 
		} else {
			patternIndex += 1;
		}
	}
	
	public void previousPattern(){
		if (patternIndex == 0){ 
			patternIndex = patternsComplete.Count - 1;
		} else {
			patternIndex -= 1;
		}
	}
	
	public void goToLevels() {  
        SceneManager.LoadScene("levels");  
    } 
	
	
}