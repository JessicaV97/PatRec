using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{	
	// public static ScoreManager scoreMan;
	public static int totalXP = 0; 
	public TextMeshProUGUI status; 

	// Update  total xp with points obtained in a level
    public static void updateScore(int levelPoints)
    {
		Debug.Log("tralala");
		totalXP += levelPoints;
		Debug.Log("Level score =  " + levelPoints);
		Debug.Log("Total xp = " + totalXP);
		
    }
	
	private void Update(){
		status.GetComponent<TextMeshProUGUI>().text = totalXP.ToString() + "/10";
	}

	
}
