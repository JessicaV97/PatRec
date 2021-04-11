using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Happify.User;

public class SettingsHandler : MonoBehaviour
{
	public Button DeafSet;
	public Sprite NoAudioPerception;
	public Sprite AudioPerception;

	public Button blindset;
	public Sprite NoVisualPerception;
	public Sprite VisualPerception;
	
	public void Start()
	{
		UserDescription currentUser = UserManager.Instance.CurrentUser;
		if (currentUser.RemainingVision)
			blindset.image.overrideSprite = VisualPerception;
		else
			blindset.image.overrideSprite = NoVisualPerception;
		
		if (currentUser.RemainingHearing)
			DeafSet.image.overrideSprite = AudioPerception;
		else
			DeafSet.image.overrideSprite = NoAudioPerception;
	}
	
	public void ChangeDeaf()
	{
		UserDescription currentUser = UserManager.Instance.CurrentUser;
		if (currentUser.RemainingHearing)
		{
			DeafSet.image.overrideSprite = NoAudioPerception;
			currentUser.RemainingHearing = false;
		} 
		else 
		{
			DeafSet.image.overrideSprite = AudioPerception;
			currentUser.RemainingHearing = true;
		}
	}
	
	public void ChangeBlind()
	{
		UserDescription currentUser = UserManager.Instance.CurrentUser;
		if (currentUser.RemainingVision)
		{
			blindset.image.overrideSprite = NoVisualPerception;
			currentUser.RemainingVision = false;
		} else {
			blindset.image.overrideSprite = VisualPerception;
			currentUser.RemainingVision = true;
		}
	}
	
	public void BackAndSettingsCheck() 
	{
		UserDescription currentUser = UserManager.Instance.CurrentUser;
		if (currentUser.RemainingHearing == false && currentUser.RemainingVision == false)
			Debug.Log("The app is not yet ready to be used with these settings unfortunately");
		else if (currentUser.RemainingHearing == true && currentUser.RemainingVision == false)
			Debug.Log("Settings set for remaining hearing (use of visuals minimalized)");
		else if (currentUser.RemainingHearing == false && currentUser.RemainingVision == true)
			Debug.Log("Settings set for remaining vision (auditory cues excluded)");
		else 
			Debug.Log("Both auditory and visual cues are included!");

        SceneManager.LoadScene("scn_MainMenu");  
    }  
}
