using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsHandler : MonoBehaviour
{
	public static bool remainingHearing = UserCreator.user1.RemainingHearing;
	public Button deafset;
	public Sprite NoAudioPerception;
	public Sprite AudioPerception;

	public static bool remainingVision = UserCreator.user1.RemainingVision;
	public Button blindset;
	public Sprite NoVisualPerception;
	public Sprite VisualPerception;
	
	public void Start()
	{
		if (remainingVision)
			blindset.image.overrideSprite = VisualPerception;
		else
			blindset.image.overrideSprite = NoVisualPerception;
		
		if (remainingHearing)
			deafset.image.overrideSprite = AudioPerception;
		else
			deafset.image.overrideSprite = NoAudioPerception;
	}
	
	public void ChangeDeaf()
	{
		if (remainingHearing)
		{
			deafset.image.overrideSprite = NoAudioPerception;
			UserCreator.user1.RemainingHearing = false;
			remainingHearing = false; 
		} 
		else 
		{
			deafset.image.overrideSprite = AudioPerception;
			UserCreator.user1.RemainingHearing = true;
			remainingHearing = true;
		}
	}
	
	public void ChangeBlind()
	{
		if (remainingVision)
		{
			blindset.image.overrideSprite = NoVisualPerception;
			UserCreator.user1.RemainingVision = false;
			remainingVision = false;
		} else {
			blindset.image.overrideSprite = VisualPerception;
			UserCreator.user1.RemainingVision = true;
			remainingVision = true;
		}
	}
	
	public void BackAndSettingsCheck() 
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
