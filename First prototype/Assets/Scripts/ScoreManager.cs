using UnityEngine;
using TMPro;
using Happify.User;

public class ScoreManager : MonoBehaviour
{	
	public static int TotalXP;


    // Update  total xp with points obtained in a level
    public static void UpdateScore(int levelPoints)
	{
		TotalXP += levelPoints;
	}

}
