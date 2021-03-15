using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;


public class buttonHandler : MonoBehaviour{
	
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
	public static List<PatternComplete> emotionsComplete = new List<PatternComplete>();
	
	// Class that handles the main menu  and reads in the given data
 
    public void goToSettings() {  
        SceneManager.LoadScene("settings");  
    }  
	
	public void goToAchievements() {  
        SceneManager.LoadScene("achievements");  
    }  
	
	public void goToHome() {  
        SceneManager.LoadScene("MainMenu");  
    }  
	public void goToLevels() {  
        SceneManager.LoadScene("levels");  
    } 
	
	public void Start(){
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
	}
	
	public static List<PatternComplete> getEmotionsList(){
		return emotionsComplete;	
	}
}
