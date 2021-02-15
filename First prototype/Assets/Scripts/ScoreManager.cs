using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{	
	public int totalXP;
	public int levelXP;
	
	
    // Start is called before the first frame update
    void Start()
    {
        levelXP = 15;
    }

    //Decrease earned XP if a mistake is made
    public void decreaseLevelScore()
    {
		levelXP -= 3;
		Debug.Log("Level score =  " + levelXP);
    }
	
	//Update total XP after a level has been completed with the XP earned in that level
	public void increaseTotalXP() {
		totalXP += levelXP; 
	}
	
}
