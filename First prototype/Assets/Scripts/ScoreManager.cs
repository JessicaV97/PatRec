using UnityEngine;
using TMPro;
using Happify.User;

public class ScoreManager : MonoBehaviour
{	
	public static int TotalXP = 0;


	// Update  total xp with points obtained in a level
	public static void UpdateScore(int levelPoints)
	{
		TotalXP += levelPoints;
		
		//Debug.Log("Level score =  " + levelPoints);
		//Debug.Log("Total xp = " + TotalXP);
	}
}
