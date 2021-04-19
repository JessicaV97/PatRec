using Happify.TextToSpeech;
using Happify.User;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour, IPointerClickHandler
{

	private static PauseMenu _instance;
	public static PauseMenu Instance => _instance;

	public GameObject PauseMenuUI;
	public AudioSource Audio;

	private float interval = 0.3f;
	int tap = 0;

	private UserDescription currentUser;

	private void Awake()
	{
		if (_instance == null)
		{
			_instance = this;
			// Ensure that script does not get destroyed when changing scene.
			//DontDestroyOnLoad(this);
		}
		//else
		//	Destroy(this);
	}

	public void Start()
	{
		PauseMenuUI.SetActive(false);
		currentUser = UserManager.Instance.CurrentUser;
	}
	
	public void Resume()
	{
        PauseMenuUI.SetActive(false);
	}
	
	public void Pause()
	{
		UserManager.Instance.Save();
		PauseMenuUI.SetActive(true);
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		string objName = eventData.selectedObject.name;

		if (!currentUser.RemainingVision && currentUser.RemainingHearing)
		{
			tap++;

			if (tap == 1)
			{
				StartCoroutine(DoubleTapInterval());
				OverarchingTTS.Instance.OnDoubleClick(objName, Audio);
			}
			else if (tap > 1)
			{
				OverarchingTTS.Instance.OnSingleClick(objName);
				tap = 0;
			}
		}
		else
			OverarchingTTS.Instance.OnSingleClick(objName);
	}

	IEnumerator DoubleTapInterval()
	{
		yield return new WaitForSeconds(interval);
		this.tap = 0;
	}
}
