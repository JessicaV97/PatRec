using UnityEngine;

/// <summary>
/// Class to add the earned score in a level of the quiz to the total number of experience points.
/// </summary>
public class ScoreManager : MonoBehaviour
{	
	public static int TotalXP;

	/// <summary>
	/// Update  total xp with points obtained in a level
	/// </summary>
	/// <param name="levelPoints"></param>
	/// Corresponds to the score earned in the level.
	public static void UpdateScore(int levelPoints)
	{
		TotalXP += levelPoints;
	}

}
