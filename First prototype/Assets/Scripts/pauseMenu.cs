using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseMenu : MonoBehaviour
{
	public static bool GameIsPaused = false;
	
	public GameObject pauseMenuUI;
	
	public void Start(){
		pauseMenuUI.SetActive(false);
	}
	
	public void Resume(){
		pauseMenuUI.SetActive(false);
		Time.timeScale = 1f;
		GameIsPaused = false;
	}
	
	public void Pause(){
		pauseMenuUI.SetActive(true);
		Time.timeScale = 0f;
		GameIsPaused = true;
	}
}
