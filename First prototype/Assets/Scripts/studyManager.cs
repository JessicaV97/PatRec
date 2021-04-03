using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Happify.Client;
using System.Text.RegularExpressions;

public class StudyManager : MonoBehaviour
{
	private Object[] patternsComplete;

	//Get objects for study environment
	public GameObject PatternVisual;
	public TextMeshProUGUI PatternTitle;
	public int PatternIndex = 0;
	public static int LevelIndex;

	public async void Awake()
    {
		await MqttService.Instance.ConnectAsync();
	}
	public void Start()
    {
		LevelIndex = LevelSwiper.GetLevel();
		if (LevelIndex != 5)
			patternsComplete = Resources.LoadAll("ScriptableObjects/SO_Emotions", typeof(SOPattern));
		else
			patternsComplete = Resources.LoadAll("ScriptableObjects/SO_General", typeof(SOPattern));

		SetPattern();
	}

	public void NextPattern()
	{
		if (PatternIndex == patternsComplete.Length - 1)
			PatternIndex = 0;
		else
			PatternIndex += 1;

		SetPattern();
	}
	
	public void PreviousPattern()
	{
		if (PatternIndex == 0) 
			PatternIndex = patternsComplete.Length - 1;
		else 
			PatternIndex -= 1;

		SetPattern();
	}
	
	
	private void SetPattern()
    {
		PatternVisual.GetComponent<Image>().sprite = (patternsComplete[PatternIndex] as SOPattern).PatternImage;
		PatternTitle.GetComponent<TextMeshProUGUI>().text = (patternsComplete[PatternIndex] as SOPattern).PatternName;
	}

	public async void PlayPattern()
    {
		string json = (patternsComplete[PatternIndex] as SOPattern).PatternJson.text;
		json = Regex.Replace(json, @"\t|\n|\r", "");
		json = json.Replace(" ", "");
		await MqttService.Instance.PublishAsync("happify/tactile-board/test", json);
    }
}