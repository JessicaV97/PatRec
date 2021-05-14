using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Happify.Levels
{
	/// <summary>
	/// Class used to select the context to play in. Is used in 'scn_Levels'.
	/// </summary>
	public class LevelSwiper : MonoBehaviour
	{
		// Create instance of level swiper
		public static LevelSwiper _levelSwiper;
		public static LevelSwiper Instance => _levelSwiper;

		//Collect game objects used in this script
		[SerializeField]
		private GameObject levelVisual;
		[SerializeField]
		private TextMeshProUGUI _levelTitle;

		// Set index for the context. This value is used in other environments to detect the topic that a user has chosen (0 = emotions, 1 = general)
		public static int LevelIndex = 0;

		// Create lists to store names and visuals of contexts in
		private Object[] _levels;
		private string[] _levelNames = new string[] { "Emoties en Sfeer", "Algemeen" };


		/// <summary>
		/// Call function to create list of visualizations of the contexts, create instance and set index to 0. 
		/// </summary>
		void Start()
		{
			CreateListOfLevelSprites();
			_levelSwiper = this;
			LevelIndex = 0;
		}

		/// <summary>
		/// Move to the next available context and adjust information and visualizations accordingly. Update index.
		/// </summary>
		public void NextLevelTopic()
		{
			if (LevelIndex == _levels.Length - 1)
				LevelIndex = 0;
			else
				LevelIndex++;
			levelVisual.GetComponent<Image>().sprite = _levels[LevelIndex] as Sprite;
			_levelTitle.GetComponent<TextMeshProUGUI>().text = _levelNames[LevelIndex];
		}

		/// <summary>
		/// Move to the previous available context and adjust information and visualizations accordingly. Update index.
		/// </summary>
		public void PreviousLevelTopic()
		{
			Debug.Log(_levels.Length);
			if (LevelIndex == 0)
				LevelIndex = _levels.Length - 1;
			else
				LevelIndex--;
			levelVisual.GetComponent<Image>().sprite = _levels[LevelIndex] as Sprite;
			_levelTitle.GetComponent<TextMeshProUGUI>().text = _levelNames[LevelIndex];
		}

		/// <summary>
		/// Collect the sprites used to visualize the contexts and add them to the list. 
		/// </summary>
		void CreateListOfLevelSprites()
		{
			_levels = Resources.LoadAll("sprt_Levels", typeof(Sprite));
		}

		/// <summary>
		/// Function used to return the current index of the list of contexts, which can in turn be used to determine the topic. 
		/// </summary>
		/// <returns></returns>
		public static int GetLevel()
		{
			return LevelIndex;
		}
    }
}