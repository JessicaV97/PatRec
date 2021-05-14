using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Happify.User;

/// <summary>
/// Class to adjust settings of a user. 
/// </summary>
public class SettingsHandler : MonoBehaviour
{
	//Collect game objects used in this script
	[SerializeField]
	private Button _deafSet;
	[SerializeField]
	private Sprite _noAudioPerception;
	[SerializeField]
	private Sprite _audioPerception;

	[SerializeField]
	private Button _blindSet;
	[SerializeField]
	private Sprite _noVisualPerception;
	[SerializeField]
	private Sprite _visualPerception;

	private UserDescription currentUser;

	/// <summary>
	/// Collect information of the current user and adapt visuals based on that.
	/// </summary>
    public void Start()
	{
		currentUser = UserManager.Instance.CurrentUser;
		if (currentUser.RemainingVision)
			_blindSet.image.overrideSprite = _visualPerception;
		else
			_blindSet.image.overrideSprite = _noVisualPerception;
		
		if (currentUser.RemainingHearing)
			_deafSet.image.overrideSprite = _audioPerception;
		else
			_deafSet.image.overrideSprite = _noAudioPerception;
	}

	/// <summary>
	/// Adjust auditory perception setting
	/// </summary>
	public void ChangeDeaf()
	{
		currentUser = UserManager.Instance.CurrentUser;
		if (currentUser.RemainingHearing)
		{
			_deafSet.image.overrideSprite = _noAudioPerception;
			currentUser.RemainingHearing = false;
		} 
		else 
		{
			_deafSet.image.overrideSprite = _audioPerception;
			currentUser.RemainingHearing = true;
		}
	}

	/// <summary>
	/// Adjust visual perception setting
	/// </summary>
	public void ChangeBlind()
	{
		currentUser = UserManager.Instance.CurrentUser;
		if (currentUser.RemainingVision)
		{
			_blindSet.image.overrideSprite = _noVisualPerception;
			currentUser.RemainingVision = false;
		} else {
			_blindSet.image.overrideSprite = _visualPerception;
			currentUser.RemainingVision = true;
		}
		
	}
	
	/// <summary>
	/// When leaving the settings environment, store the adjustments that have been made. 
	/// Go bach to home page. 
	/// </summary>
	public void BackAndSettingsCheck() 
	{
		if (currentUser.RemainingHearing == false && currentUser.RemainingVision == false)
			Debug.Log("The app is not yet ready to be used with these settings unfortunately");
		else if (currentUser.RemainingHearing == true && currentUser.RemainingVision == false)
			Debug.Log("Settings set for remaining hearing (use of visuals minimalized)");
		else if (currentUser.RemainingHearing == false && currentUser.RemainingVision == true)
			Debug.Log("Settings set for remaining vision (auditory cues excluded)");
		else 
			Debug.Log("Both auditory and visual cues are included!");
		UserManager.Instance.Save();
		SceneManager.LoadScene("scn_MainMenu");  
    }  
}
