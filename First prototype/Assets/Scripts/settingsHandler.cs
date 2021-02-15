using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class settingsHandler : MonoBehaviour
{
	public Button deafset;
	public Sprite doofx;
	public Sprite doof;
	private int counterd = 0; 
	
	public Button blindset;
	public Sprite blindx;
	public Sprite blind;
	private int counterb = 0; 
	
	public void changeDeaf(){
		counterd++;
		if (counterd % 2 == 0){
			deafset.image.overrideSprite = doofx;
		} else {
			deafset.image.overrideSprite = doof;
		}
	}
	
	public void changeBlind(){
		counterb++;
		if (counterb % 2 == 0){
			blindset.image.overrideSprite = blindx;
		} else {
			blindset.image.overrideSprite = blind;
		}
	}
}
