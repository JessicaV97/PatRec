using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using Random = UnityEngine.Random;


public class buttonHandler : MonoBehaviour{
	
	private Object[] jsonsEmotions;
	private Object[] jsonsGeneral;
	private Object[] emotionImages;
	private Object[] generalImages;
	
	public List<Pattern> emotions = new List<Pattern>();
	public static List<PatternComplete> emotionsComplete = new List<PatternComplete>();
	
	public List<Pattern> general = new List<Pattern>();
	public static List<PatternComplete> generalComplete = new List<PatternComplete>();
	
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
		createListOfInput();
	}
	
	void createListOfInput()
	{
        jsonsEmotions = Resources.LoadAll("JsonsEmoties", typeof(TextAsset));
		jsonsGeneral = Resources.LoadAll("JsonsGeneral", typeof(TextAsset));
		
		foreach (TextAsset json in jsonsEmotions){
			emotions.Add(JsonConvert.DeserializeObject<Pattern>(json.ToString()));
		}
		
		foreach (TextAsset json in jsonsGeneral){
			general.Add(JsonConvert.DeserializeObject<Pattern>(json.ToString()));
		}
		
		emotionImages = Resources.LoadAll("EmotiesEnSfeer", typeof(Sprite));
		generalImages = Resources.LoadAll("Algemeen", typeof(Sprite));
		Debug.Log(emotionImages.Length.ToString());
		Debug.Log(emotions.Count);
		Debug.Log(generalImages.Length.ToString());
		Debug.Log(general.Count);
		for (int i = 0; i < emotionImages.Length; i++){
			emotionsComplete.Add(new PatternComplete{patternName = emotionImages[i].name, patternJson = emotions[i], patternImage = emotionImages[i] as Sprite, difficulty = Random.Range(1, 4)});
		}
		
		for (int i = 0; i < generalImages.Length; i++){
			generalComplete.Add(new PatternComplete{patternName = generalImages[i].name, patternJson = general[i], patternImage = generalImages[i] as Sprite, difficulty = Random.Range(1, 4)});
		}
	}
	
	public static List<PatternComplete> getEmotionsList(){
		return emotionsComplete;	
	}
	
	public static List<PatternComplete> getGeneralList(){
		return generalComplete;	
	}
}
