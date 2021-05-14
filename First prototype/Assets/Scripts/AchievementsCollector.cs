using Happify.User;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Linq;
using Happify.Scores;

/// <summary>
/// Class is used in 'scn_achievements'. Takes care of the list of earned achievements
/// </summary>
public class AchievementsCollector : MonoBehaviour
{
    // Create instance of achievements collector
    private static AchievementsCollector _instance;
    public static AchievementsCollector Instance => _instance;

    // Create list of the available achievements
    public static SOAchievement[] PossibleAchievements; 
    // Create list of the earned achievements
    public static List<SOAchievement> EarnedAchievements = new List<SOAchievement>();
    // Set index for the badge in earned badges
	private int _badgeIndex = 0;

    //Collect game objects used in this script
    [SerializeField]
    private GameObject _noBadges;
    [SerializeField]
    private GameObject _badgeSprite;
    [SerializeField]
    private GameObject _badgeText;
    [SerializeField]
    private GameObject _badgeTopic;
    [SerializeField]
    private GameObject _status;
    [SerializeField]
    private GameObject _next;
    [SerializeField]
    private GameObject _previous;
    [SerializeField]
    public GameObject _scoreBoard;
    [SerializeField]
    private AudioSource _audio;

    private UserDescription currentUser;
    
    //String that used to combine pieces of information of the achievements scene to read out loud to a user using the app in blind mode
    private string _audioString;

    /// <summary>
    /// Create AchievementsCollector instance and deactivate scoreboard panel
    /// </summary>
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        
        _scoreBoard.SetActive(false);
    }

    /// <summary>
    /// Set the current user and note its experience points
    /// If the user has earned no badges yet, show message to encourage playing and deactivate the buttons related to moving through the list of earned badges.
    /// Otherwise show 1 badge with corresponding information and activate buttons to move through the list. 
    /// In addition, add information to the audioString used in blind mode. 
    /// If the user is playing in blind mode, play the collected audio information at opening of the achievements scene. 
    /// </summary>
    void Start()
    {
        currentUser = UserManager.Instance.CurrentUser;
        _audioString = "Badges omgeving. Je hebt " + ScoreManager.TotalXP.ToString() + "experience punten";
        _status.GetComponent<TextMeshProUGUI>().text = UserManager.Instance.CurrentUser.ExperiencePoints + "XP";

        if (EarnedAchievements.Count == 0)
        {
            _noBadges.SetActive(true);
            _badgeSprite.SetActive(false);
            _badgeText.SetActive(false);
            _badgeTopic.SetActive(false);
            _next.SetActive(false);
            _previous.SetActive(false);
            _audioString = _audioString + "Je hebt nog geen badges behaald. Probeer de quiz om een badge te verdienen.";
        }
        else
        {
            _noBadges.SetActive(false);
            _badgeSprite.SetActive(true);
            _badgeText.SetActive(true);
            _badgeText.GetComponent<TextMeshProUGUI>().text = EarnedAchievements[_badgeIndex].AchievementText;
            _badgeSprite.GetComponent<Image>().sprite = EarnedAchievements[_badgeIndex].BadgeImage;
            _badgeTopic.GetComponent<TextMeshProUGUI>().text = EarnedAchievements[_badgeIndex].AchievementTopic;
            _next.SetActive(true);
            _previous.SetActive(true);

            if (!currentUser.RemainingVision && currentUser.RemainingHearing)
                _audioString = _audioString + "Onderwerp is " + EarnedAchievements[_badgeIndex].AchievementTopic + ". Badge is " + EarnedAchievements[_badgeIndex].AchievementText; 
        }
        
        if (!currentUser.RemainingVision && currentUser.RemainingHearing)
            StartCoroutine(DownloadTheAudio(_audioString));
    }

    /// <summary>
    /// Function used to activate creation list of scores and activate the panel that belongs to it. 
    /// </summary>
    public void ShowScores()
    {
        HighScoreList.Instance.CreateList();
        _scoreBoard.SetActive(true);
    }

    /// <summary>
    /// Deactivate the panel with list of scores per person. 
    /// </summary>
    public void HideScores()
    {
        _scoreBoard.SetActive(false);
    }

    /// <summary>
    /// Function that provides the QuizManager with the pop up text that belongs to a badge that is earned by the user. 
    /// </summary>
    /// <param name="topic"></param>
    /// Refers to the context in which the badge is earned
    /// <param name="userLevel"></param>
    /// Refers to the level of experience a user has in a certain context. 
    /// <returns></returns>
    public static string PopUpAchievement(string topic, Level userLevel)
    {
        PossibleAchievements = Resources.LoadAll<SOAchievement>("ScriptableObjects/SO_Achievements");
        SOAchievement badge = PossibleAchievements.FirstOrDefault(element => element.AchievementTopic == topic && 
                                                                             element.AchievementLevel == (int)userLevel);

        if(badge == null)
        {
            Debug.LogError($"Could not find achievement for topic: {topic}");
            return string.Empty;
        }

        EarnedAchievements.Add(badge);
        UserManager.Instance.CurrentUser.NumberOfObtainedBadges++;
        UserManager.Instance.Save();
        string PUText = badge.PopUpText;
        return PUText;
    }

    /// <summary>
    /// Go to next badge in list of earned achievements. Adjust information on screen to the next badge. 
    /// If the user uses blind mode, read out loud the badge that is shown on the screen. 
    /// </summary>
	public void NextBadge()
	{
        if (_badgeIndex == EarnedAchievements.Count - 1)
			_badgeIndex = 0;
		else
			_badgeIndex += 1;
        
        _badgeText.GetComponent<TextMeshProUGUI>().text = EarnedAchievements[_badgeIndex].AchievementText;
        _badgeSprite.GetComponent<Image>().sprite = EarnedAchievements[_badgeIndex].BadgeImage;
        _badgeTopic.GetComponent<TextMeshProUGUI>().text = EarnedAchievements[_badgeIndex].AchievementTopic;

        if (!currentUser.RemainingVision && currentUser.RemainingHearing)
        {
            _audioString = "Volgende. Onderwerp is " + EarnedAchievements[_badgeIndex].AchievementTopic + ". Badge is " + EarnedAchievements[_badgeIndex].AchievementText;
            StartCoroutine(DownloadTheAudio(_audioString));
        }
    }

    /// <summary>
    /// Go to next badge in list of earned achievements. Adjust information on screen to the next badge. 
    /// If the user uses blind mode, read out loud the badge that is shown on the screen. 
    /// </summary>
    public void PreviousBadge()
	{
        if (_badgeIndex == 0)
			_badgeIndex = EarnedAchievements.Count - 1;
		else
			_badgeIndex -= 1;
        _badgeText.GetComponent<TextMeshProUGUI>().text = EarnedAchievements[_badgeIndex].AchievementText;
        _badgeSprite.GetComponent<Image>().sprite = EarnedAchievements[_badgeIndex].BadgeImage;
        _badgeTopic.GetComponent<TextMeshProUGUI>().text = EarnedAchievements[_badgeIndex].AchievementTopic;

        if (!currentUser.RemainingVision && currentUser.RemainingHearing)
        {
            _audioString = "Vorige. Onderwerp is " + EarnedAchievements[_badgeIndex].AchievementTopic + ". Badge is " + EarnedAchievements[_badgeIndex].AchievementText;
            StartCoroutine(DownloadTheAudio(_audioString));
        }
    }

    /// <summary>
    /// Function used to add badges to the list of earned badges based on experience points. 
    /// Provides QuizManager with pop up text that belongs to the earned badge. 
    /// </summary>
    /// <param name="level"></param>
    /// Level refers to the amount of experience of the user in general (10XP, 25XP, 50XP, 100XP)
    /// <returns></returns>
    public static string AddXpBadge(int level)
    {
        PossibleAchievements = Resources.LoadAll<SOAchievement>("ScriptableObjects/SO_Achievements");
        SOAchievement badge = PossibleAchievements.FirstOrDefault(x => x.AchievementTopic == "Niveau" &&
                                                                       x.AchievementLevel == level);

        if(badge == null)
        {
            Debug.LogError($"Could not find XP Badge for level: {level}");
            return string.Empty;
        }

        Debug.Log(badge.PopUpText);
        if (!EarnedAchievements.Contains(badge))
        {
            EarnedAchievements.Add(badge);
            string PUText = badge.PopUpText;
            return PUText;
        }

        UserManager.Instance.CurrentUser.NumberOfObtainedBadges++;
        UserManager.Instance.Save();

        return "";
    }

    /// <summary>
    /// Function to produce speech synthesis using google translate. Wifi needed. 
    /// </summary>
    /// <param name="message"></param>
    /// Message is the string of information you want to have read out loud
    /// <returns></returns>
    IEnumerator DownloadTheAudio(string message)
    {
        using (UnityWebRequest website = UnityWebRequestMultimedia.GetAudioClip("https://translate.google.com/translate_tts?ie=UTF-8&total=1&idx=0&textlen=32&client=tw-ob&q=" + message + "&tl=NL", AudioType.MPEG))
        {
            yield return website.SendWebRequest();

            if (website.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(website.error);
            }
            else
            {
                AudioClip myClip = DownloadHandlerAudioClip.GetContent(website);
                _audio.clip = myClip;
                _audio.Play();
            }
        }
    }

}