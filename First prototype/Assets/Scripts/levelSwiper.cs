using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelSwiper : MonoBehaviour{ 

	//Get objects for choosing level
	public GameObject levelVisual;
	public TextMeshProUGUI levelTitle;
	public static int levelIndex = 0;
	
	
	private Object[] levels;
	string[] levelNames = new string[] { "Emoties en Sfeer", "Eten en drinken", "Personen", "Objecten", "Ruimtes en Richting", "Algemeen"};

    // Start is called before the first frame update
    void Start()
	{
		CreateListOfLevelSprites();
    }
	
	private void Update()
	{
		levelVisual.GetComponent<Image>().sprite = levels[levelIndex] as Sprite;
		levelTitle.GetComponent<TextMeshProUGUI>().text = levelNames[levelIndex];
	}
	
	public void NextPattern()
	{
		if (levelIndex == levels.Length - 1)
			levelIndex = 0; 
		else 
			levelIndex += 1;
	}
	
	public void PreviousPattern()
	{ 
		if (levelIndex == 0) 
			levelIndex = levels.Length - 1;
		else 
			levelIndex -= 1;
	}
	
	void CreateListOfLevelSprites()
	{
        levels = Resources.LoadAll("sprt_Levels", typeof(Sprite));
	}

	public static int GetLevel()
	{
		return levelIndex;
	} 
}