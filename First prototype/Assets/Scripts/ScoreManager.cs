using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{	
	// public static ScoreManager scoreMan;
	
	
	public static int totalXP = 0; 
	public TextMeshProUGUI status; 

	// Update  total xp with points obtained in a level
    public static void updateScore(int levelPoints)
    {
		totalXP += levelPoints;
		Debug.Log("Level score =  " + levelPoints);
		Debug.Log("Total xp = " + totalXP);
		
    }
	
	private void Update(){
		status.GetComponent<TextMeshProUGUI>().text = totalXP.ToString() + "/10";
	}
	
	public void goToHome() {  
        SceneManager.LoadScene("MainMenu");  
    } 

	
}
