using UnityEngine;

public class PauseMenu : MonoBehaviour
{	
	public GameObject PauseMenuUI;
	
	public void Start()
	{
		PauseMenuUI.SetActive(false);
	}
	
	public void Resume()
	{
		PauseMenuUI.SetActive(false);
		//Time.timeScale = 1f;
	}
	
	public void Pause()
	{
		PauseMenuUI.SetActive(true);
		//Time.timeScale = 0f;
	}
}
