using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementsCollector : MonoBehaviour
{
    public static object[] PossibleAchievements; 
    public static List<SOAchievement> EarnedAchievements = new List<SOAchievement>();
	private int _badgeIndex = 0;
    public GameObject NoBadges;
    public GameObject BadgeSprite;
    public GameObject BadgeText;
    public GameObject BadgeTopic;
    public GameObject Status;
    public GameObject Next;
    public GameObject Previous;

    void Start()
    {
        Status.GetComponent<TextMeshProUGUI>().text = ScoreManager.TotalXP + "XP";

        if (EarnedAchievements.Count == 0)
        {
            NoBadges.SetActive(true);
            BadgeSprite.SetActive(false);
            BadgeText.SetActive(false);
            BadgeTopic.SetActive(false);
            Next.SetActive(false);
            Previous.SetActive(false);
        }
        else
        {
            NoBadges.SetActive(false);
            BadgeSprite.SetActive(true);
            BadgeText.SetActive(true);
            BadgeText.GetComponent<TextMeshProUGUI>().text = EarnedAchievements[_badgeIndex].AchievementText;
            BadgeSprite.GetComponent<Image>().sprite = EarnedAchievements[_badgeIndex].BadgeImage;
            BadgeTopic.GetComponent<TextMeshProUGUI>().text = EarnedAchievements[_badgeIndex].AchievementTopic;
            Next.SetActive(true);
            Previous.SetActive(true);
        }
    }

    public static string PopUpAchievement(string topic, int userLevel)
    {
        PossibleAchievements = Resources.LoadAll("ScriptableObjects/SO_Achievements", typeof(SOAchievement));
        SOAchievement Badge = Array.Find(PossibleAchievements, element => topic.Equals((element as SOAchievement).AchievementTopic) && (element as SOAchievement).AchievementLevel == userLevel) as SOAchievement;
        EarnedAchievements.Add(Badge);
        string PUText = Badge.PopUpText;
        return PUText;
    }

	public void NextBadge()
	{
		if (_badgeIndex == EarnedAchievements.Count - 1)
			_badgeIndex = 0;
		else
			_badgeIndex += 1;
        BadgeText.GetComponent<TextMeshProUGUI>().text = EarnedAchievements[_badgeIndex].AchievementText;
        BadgeSprite.GetComponent<Image>().sprite = EarnedAchievements[_badgeIndex].BadgeImage;
        BadgeTopic.GetComponent<TextMeshProUGUI>().text = EarnedAchievements[_badgeIndex].AchievementTopic;
    }

	public void PreviousBadge()
	{
		if (_badgeIndex == 0)
			_badgeIndex = EarnedAchievements.Count - 1;
		else
			_badgeIndex -= 1;
        BadgeText.GetComponent<TextMeshProUGUI>().text = EarnedAchievements[_badgeIndex].AchievementText;
        BadgeSprite.GetComponent<Image>().sprite = EarnedAchievements[_badgeIndex].BadgeImage;
        BadgeTopic.GetComponent<TextMeshProUGUI>().text = EarnedAchievements[_badgeIndex].AchievementTopic;
    }

    public static string AddXpBadge(int level)
    {
        PossibleAchievements = Resources.LoadAll("ScriptableObjects/SO_Achievements", typeof(SOAchievement));
        SOAchievement Badge = Array.Find(PossibleAchievements, element => "Niveau".Equals((element as SOAchievement).AchievementTopic) && (element as SOAchievement).AchievementLevel == level) as SOAchievement;

        Debug.Log(Badge.PopUpText);
        if (!EarnedAchievements.Contains(Badge))
        {
            EarnedAchievements.Add(Badge);
            string PUText = Badge.PopUpText;
            return PUText;
        }
        return "";
    }
}