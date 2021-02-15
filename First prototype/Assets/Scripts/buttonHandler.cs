using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class buttonHandler : MonoBehaviour{
	 
    public void playGame() {  
	Debug.Log("blub");
        SceneManager.LoadScene("mainGameScreen");  
    }  
    public void goToSettings() {  
        SceneManager.LoadScene("settings");  
    }  
	
	public void goToAchievements() {  
        SceneManager.LoadScene("achievements");  
    }  
	
	public void goToHome() {  
        SceneManager.LoadScene("MainMenu");  
    }  
	public void goToLevels() {  
        SceneManager.LoadScene("levels");  
    } 
}
