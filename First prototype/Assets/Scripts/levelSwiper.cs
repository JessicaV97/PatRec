using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using TMPro;

public class levelSwiper : MonoBehaviour{ 

	// private Vector3 panelLocation;
	// public float percentTreshold = 0.2f;
	// public float easing = 0.5f;
	
	//Get objects for choosing level
	public GameObject levelVisual;
	public TextMeshProUGUI levelTitle;
	public static int levelIndex = 0;
	
	
	private Object[] levels;
	string[] levelNames = new string[] { "Emoties en Sfeer", "Eten en drinken", "Personen", "Objecten", "Ruimtes en Richting", "Algemeen"};

    // Start is called before the first frame update
    void Start(){
		createListOfLevelSprites();
        // panelLocation = transform.position;
    }
	
	private void Update(){
		levelVisual.GetComponent<Image>().sprite = levels[levelIndex] as Sprite;
		levelTitle.GetComponent<TextMeshProUGUI>().text = levelNames[levelIndex];
	}
	
	public void nextPattern(){
		if (levelIndex == levels.Length - 1){
			levelIndex = 0; 
		} else {
			levelIndex += 1;
		}
	}
	
	public void previousPattern(){
		if (levelIndex == 0){ 
			levelIndex = levels.Length - 1;
		} else {
			levelIndex -= 1;
		}
	}
	
	void createListOfLevelSprites()
	{
        levels = Resources.LoadAll("levels", typeof(Sprite));
		foreach (Sprite level in levels){
			Debug.Log(level.name);
		}
	}

    // public void OnDrag (PointerEventData data){
		// float difference = data.pressPosition.x - data.position.x;
		// transform.position = panelLocation - new Vector3(difference, 0,0);
	// }
	
	// public void OnEndDrag(PointerEventData data){
		// float percentage = (data.pressPosition.x - data.position.x) / Screen.width;
		// if(Mathf.Abs(percentage) >= percentTreshold){
			// Vector3 newLocation = panelLocation;
			// if(percentage > 0){
				// newLocation+= new Vector3(-Screen.width, 0, 0);
			// } else if (percentage<0){
				// newLocation += new Vector3(Screen.width, 0, 0);
			// }
			// StartCoroutine (SmoothMove(transform.position, newLocation, easing));
			// panelLocation = newLocation;
		// } else {
			// StartCoroutine (SmoothMove(transform.position, panelLocation, easing));
		// }
	// }
	
	// IEnumerator SmoothMove(Vector3 startpos, Vector3 endpos, float seconds){
		// float t = 0f;
		// while (t <= 1.0){
			// t += Time.deltaTime / seconds;
			// transform.position = Vector3.Lerp(startpos, endpos, Mathf.SmoothStep(0f, 1f, t));
			// yield return null;
		// }
	// }
	public static int getLevel(){
		return levelIndex;
	}
	
	public void studyEmotions() { 
		SceneManager.LoadScene("studyEnvironment");  
    } 
}