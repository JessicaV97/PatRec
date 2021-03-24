using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class settingsHandler : MonoBehaviour
{
	public static bool remainingHearing = true;
	public Button deafset;
	public Sprite doofx;
	public Sprite doof;
	private int counterd = 0; 
	
	public static bool remainingVision = true;
	public Button blindset;
	public Sprite blindx;
	public Sprite blind;
	private int counterb = 0; 
	
	public void Start()
	{
		if (remainingVision)
			blindset.image.overrideSprite = blind;
		else
			blindset.image.overrideSprite = blindx;
		
		if (remainingVision)
			deafset.image.overrideSprite = doof;
		else
			deafset.image.overrideSprite = doofx;
	}
	
	public void changeDeaf()
	{
		counterd++;
		if (counterd % 2 == 0){
			deafset.image.overrideSprite = doofx;
			remainingHearing = false;
		} 
		else 
		{
			deafset.image.overrideSprite = doof;
			remainingHearing = true;
		}
	}
	
	public void changeBlind()
	{
		counterb++;
		if (counterb % 2 == 0){
			blindset.image.overrideSprite = blindx;
			remainingVision = false;
		} else {
			blindset.image.overrideSprite = blind;
			remainingVision = true; 
		}
	}
	
	public void backAndSettingsCheck() 
	{  
		if (remainingHearing == false && remainingVision == false)
			Debug.Log("The app is not yet ready to be used with these settings unfortunately");
		else if (remainingHearing == true && remainingVision == false)
			Debug.Log("Settings set for remaining hearing (use of visuals minimalized)");
		else if (remainingHearing == false && remainingVision == true)
			Debug.Log("Settings set for remaining vision (auditory cues excluded)");
		else 
			Debug.Log("Both auditory and visual cues are included!");

        SceneManager.LoadScene("scn_MainMenu");  
    }  
}
