using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelSwiper : MonoBehaviour{ 

	//Get objects for choosing level
	public GameObject LevelVisual;
	public TextMeshProUGUI LevelTitle;
	public static int LevelIndex = 0;
	
	
	private Object[] _levels;
	string[] levelNames = new string[] { "Emoties en Sfeer", "Eten en drinken", "Personen", "Objecten", "Ruimtes en Richting", "Algemeen"};

    // Start is called before the first frame update
    void Start()
	{
		CreateListOfLevelSprites();
    }
	
	public void NextPattern()
	{
		if (LevelIndex == _levels.Length - 1)
			LevelIndex = 0; 
		else 
			LevelIndex++;
		LevelVisual.GetComponent<Image>().sprite = _levels[LevelIndex] as Sprite;
		LevelTitle.GetComponent<TextMeshProUGUI>().text = levelNames[LevelIndex];
	}
	
	public void PreviousPattern()
	{
		Debug.Log(_levels.Length);
		if (LevelIndex == 0) 
			LevelIndex = _levels.Length - 1;
		else 
			LevelIndex--;
		LevelVisual.GetComponent<Image>().sprite = _levels[LevelIndex] as Sprite;
		LevelTitle.GetComponent<TextMeshProUGUI>().text = levelNames[LevelIndex];
	}
	
	void CreateListOfLevelSprites()
	{
        _levels = Resources.LoadAll("sprt_Levels", typeof(Sprite));
	}

	public static int GetLevel()
	{
		return LevelIndex;
	} 
}