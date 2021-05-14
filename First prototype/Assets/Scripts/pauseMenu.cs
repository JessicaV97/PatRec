using Happify.TextToSpeech;
using Happify.User;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Class that takes care of the pause panel in main game environment. 
/// </summary>
public class PauseMenu : MonoBehaviour, IPointerClickHandler
{
	//Create pause instance
	private static PauseMenu _instance;
	public static PauseMenu Instance => _instance;

	//Collect game objects used in this script
	[SerializeField]
	private GameObject pauseMenuUI;
	[SerializeField]
	private AudioSource _audio;

	private float interval = 0.3f;
	int tap = 0;

	private UserDescription currentUser;

	/// <summary>
	/// Create pause instance
	/// </summary>
	private void Awake()
	{
		if (_instance == null)
		{
			_instance = this;
		}
	}

	/// <summary>
	/// Deactivate pause panel. Collect current user information. 
	/// </summary>
	public void Start()
	{
		pauseMenuUI.SetActive(false);
		currentUser = UserManager.Instance.CurrentUser;
	}
	
	/// <summary>
	/// Deactivate pause panel
	/// </summary>
	public void Resume()
	{
        pauseMenuUI.SetActive(false);
	}
	
	/// <summary>
	/// Activate pause panel. 
	/// Store user manager instance.
	/// </summary>
	public void Pause()
	{
		UserManager.Instance.Save();
		pauseMenuUI.SetActive(true);
	}

	/// <summary>
	/// Track double vs single tapping.
	/// In blind mode, the buttons will be read out loud. 
	/// </summary>
	/// <param name="eventData"></param>
	public void OnPointerClick(PointerEventData eventData)
	{
		string objName = eventData.selectedObject.name;

		if (!currentUser.RemainingVision && currentUser.RemainingHearing)
		{
			tap++;

			if (tap == 1)
			{
				StartCoroutine(DoubleTapInterval());
				OverarchingTTS.Instance.OnSingleClick(objName, _audio);
			}
			else if (tap > 1)
			{
				OverarchingTTS.Instance.OndoubleClick(objName, _audio);
				tap = 0;
			}
		}
		else
			OverarchingTTS.Instance.OndoubleClick(objName, _audio);
	}

	IEnumerator DoubleTapInterval()
	{
		yield return new WaitForSeconds(interval);
		this.tap = 0;
	}
}
