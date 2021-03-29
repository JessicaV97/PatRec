using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{	
	// public static ScoreManager scoreMan;
	public static int TotalXP = 0; 
	public TextMeshProUGUI status; 

	// Update  total xp with points obtained in a level
    public static void UpdateScore(int levelPoints)
    {
		TotalXP += levelPoints;
		Debug.Log("Level score =  " + levelPoints);
		Debug.Log("Total xp = " + TotalXP);
    }
	
	private void Update()
	{
		status.GetComponent<TextMeshProUGUI>().text = TotalXP + "XP";
	}
}
