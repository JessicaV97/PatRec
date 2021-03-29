using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementsCollector : MonoBehaviour
{
    public static object[] PossibleAchievements; 
    public static List<SOAchievement> EarnedAchievements = new List<SOAchievement>();
	public int BadgeIndex = 0;
    public GameObject NoBadges;
    public GameObject BadgeSprite;
    public GameObject BadgeText;
    public GameObject BadgeTopic;
    // Start is called before the first frame update

    private void Awake()
    {

    }
    void Start()
    {
        
        if (EarnedAchievements.Count == 0)
        {
            NoBadges.SetActive(true);
            BadgeSprite.SetActive(false);
            BadgeText.SetActive(false);
            BadgeTopic.SetActive(false);
        }
        else
        {
            NoBadges.SetActive(false);
            BadgeSprite.SetActive(true);
            BadgeText.SetActive(true);
            BadgeText.GetComponent<TextMeshProUGUI>().text = EarnedAchievements[BadgeIndex].AchievementText;
            BadgeSprite.GetComponent<Image>().sprite = EarnedAchievements[BadgeIndex].BadgeImage;
            BadgeTopic.GetComponent<TextMeshProUGUI>().text = EarnedAchievements[BadgeIndex].AchievementTopic;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static string PopUpAchievement(string topic, int userLevel)
    {
        PossibleAchievements = Resources.LoadAll("ScriptableObjects/SO_Achievements", typeof(SOAchievement));
        SOAchievement Badge = Array.Find(PossibleAchievements, element => topic.Equals((element as SOAchievement).AchievementTopic) && (element as SOAchievement).AchievementLevel == userLevel) as SOAchievement;
        EarnedAchievements.Add(Badge);
        string PUText = Badge.PopUpText;
        Debug.Log(PUText);
        return PUText;
    }

	public void NextBadge()
	{
		if (BadgeIndex == EarnedAchievements.Count - 1)
			BadgeIndex = 0;
		else
			BadgeIndex += 1;
        BadgeText.GetComponent<TextMeshProUGUI>().text = EarnedAchievements[BadgeIndex].AchievementText;
        BadgeSprite.GetComponent<Image>().sprite = EarnedAchievements[BadgeIndex].BadgeImage;
        BadgeTopic.GetComponent<TextMeshProUGUI>().text = EarnedAchievements[BadgeIndex].AchievementTopic;
    }

	public void PreviousBadge()
	{
		if (BadgeIndex == 0)
			BadgeIndex = EarnedAchievements.Count - 1;
		else
			BadgeIndex -= 1;
        BadgeText.GetComponent<TextMeshProUGUI>().text = EarnedAchievements[BadgeIndex].AchievementText;
        BadgeSprite.GetComponent<Image>().sprite = EarnedAchievements[BadgeIndex].BadgeImage;
        BadgeTopic.GetComponent<TextMeshProUGUI>().text = EarnedAchievements[BadgeIndex].AchievementTopic;
    }

    //public static string AddXPBadge(int xp)
    //{
    //    PossibleAchievements = Resources.LoadAll("ScriptableObjects/SO_Achievements", typeof(SOAchievement));
    //    if (xp == 10)
    //    {
    //        SOAchievement Badge = Array.Find(PossibleAchievements, element => "niveau".Equals((element as SOAchievement).AchievementTopic) && (element as SOAchievement).AchievementLevel == 1) as SOAchievement;
    //        EarnedAchievements.Add(Badge);
    //        string PUText = Badge.PopUpText;
    //        return PUText;
    //    }
    //}
}