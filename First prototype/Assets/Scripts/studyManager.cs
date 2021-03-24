using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class studyManager : MonoBehaviour
{
	//private List<SOPattern> patternsComplete = new List<SOPattern>();
	private Object[] patternsComplete;

	//Get objects for study environment
	public GameObject patternVisual;
	public TextMeshProUGUI patternTitle;
	public int patternIndex = 0;
	public static int levelIndex;


	public void Start()
    {
		levelIndex = levelSwiper.getLevel();
		if (levelIndex != 5)
			patternsComplete = Resources.LoadAll("ScriptableObjects/SO_Emotions", typeof(SOPattern));
		else
			patternsComplete = Resources.LoadAll("ScriptableObjects/SO_General", typeof(SOPattern));

		setPattern();
	}

	public void nextPattern()
	{
		if (patternIndex == patternsComplete.Length - 1)
			patternIndex = 0;
		else
			patternIndex += 1;

		setPattern();
	}
	
	public void previousPattern()
	{
		if (patternIndex == 0) 
			patternIndex = patternsComplete.Length - 1;
		else 
			patternIndex -= 1;

		setPattern();
	}
	
	
	private void setPattern()
    {
		patternVisual.GetComponent<Image>().sprite = (patternsComplete[patternIndex] as SOPattern).patternImage;
		patternTitle.GetComponent<TextMeshProUGUI>().text = (patternsComplete[patternIndex] as SOPattern).patternName;
	}
	
}