using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Happify.Client;
using System.Text.RegularExpressions;

public class StudyManager : MonoBehaviour
{
	private Object[] patternsComplete;

	//Get objects for study environment
	public GameObject patternVisual;
	public TextMeshProUGUI patternTitle;
	public int patternIndex = 0;
	public static int levelIndex;

	public async void Awake()
    {
		await MqttService.Instance.ConnectAsync();
	}
	public void Start()
    {
		levelIndex = LevelSwiper.GetLevel();
		if (levelIndex != 5)
			patternsComplete = Resources.LoadAll("ScriptableObjects/SO_Emotions", typeof(SOPattern));
		else
			patternsComplete = Resources.LoadAll("ScriptableObjects/SO_General", typeof(SOPattern));

		SetPattern();
	}

	public void NextPattern()
	{
		if (patternIndex == patternsComplete.Length - 1)
			patternIndex = 0;
		else
			patternIndex += 1;

		SetPattern();
	}
	
	public void PreviousPattern()
	{
		if (patternIndex == 0) 
			patternIndex = patternsComplete.Length - 1;
		else 
			patternIndex -= 1;

		SetPattern();
	}
	
	
	private void SetPattern()
    {
		patternVisual.GetComponent<Image>().sprite = (patternsComplete[patternIndex] as SOPattern).patternImage;
		patternTitle.GetComponent<TextMeshProUGUI>().text = (patternsComplete[patternIndex] as SOPattern).patternName;
	}

	public async void PlayPattern()
    {
		string json = (patternsComplete[patternIndex] as SOPattern).patternJson.text;
		json = Regex.Replace(json, @"\t|\n|\r", "");
		json = json.Replace(" ", "");
		await MqttService.Instance.PublishAsync("happify/tactile-board/test", json);
    }
}