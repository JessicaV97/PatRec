using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsHandler : MonoBehaviour
{
	public static bool RemainingHearing = UserCreator.User1.RemainingHearing;
	public Button DeafSet;
	public Sprite NoAudioPerception;
	public Sprite AudioPerception;

	public static bool RemainingVision = UserCreator.User1.RemainingVision;
	public Button blindset;
	public Sprite NoVisualPerception;
	public Sprite VisualPerception;
	
	public void Start()
	{
		if (RemainingVision)
			blindset.image.overrideSprite = VisualPerception;
		else
			blindset.image.overrideSprite = NoVisualPerception;
		
		if (RemainingHearing)
			DeafSet.image.overrideSprite = AudioPerception;
		else
			DeafSet.image.overrideSprite = NoAudioPerception;
	}
	
	public void ChangeDeaf()
	{
		if (RemainingHearing)
		{
			DeafSet.image.overrideSprite = NoAudioPerception;
			UserCreator.User1.RemainingHearing = false;
			RemainingHearing = false; 
		} 
		else 
		{
			DeafSet.image.overrideSprite = AudioPerception;
			UserCreator.User1.RemainingHearing = true;
			RemainingHearing = true;
		}
	}
	
	public void ChangeBlind()
	{
		if (RemainingVision)
		{
			blindset.image.overrideSprite = NoVisualPerception;
			UserCreator.User1.RemainingVision = false;
			RemainingVision = false;
		} else {
			blindset.image.overrideSprite = VisualPerception;
			UserCreator.User1.RemainingVision = true;
			RemainingVision = true;
		}
	}
	
	public void BackAndSettingsCheck() 
	{  
		if (RemainingHearing == false && RemainingVision == false)
			Debug.Log("The app is not yet ready to be used with these settings unfortunately");
		else if (RemainingHearing == true && RemainingVision == false)
			Debug.Log("Settings set for remaining hearing (use of visuals minimalized)");
		else if (RemainingHearing == false && RemainingVision == true)
			Debug.Log("Settings set for remaining vision (auditory cues excluded)");
		else 
			Debug.Log("Both auditory and visual cues are included!");

        SceneManager.LoadScene("scn_MainMenu");  
    }  
}
