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
	}
	
	public void Pause()
	{
		PauseMenuUI.SetActive(true);
	}
}
