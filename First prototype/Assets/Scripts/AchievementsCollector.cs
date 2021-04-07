using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
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

    public AudioSource _audio;

    IEnumerator runningCoroutine = null;
    private Queue<IEnumerator> _coroutineQueue = new Queue<IEnumerator>();

    void Start()
    {
        if (runningCoroutine == null)
        {
            runningCoroutine = (DownloadTheAudio("Badges"));
            StartCoroutine(runningCoroutine);
        }
        else
            _coroutineQueue.Enqueue(DownloadTheAudio("Badge"));
        
        if (!UserCreator.User1.RemainingVision && UserCreator.User1.RemainingHearing)
        {
            if (runningCoroutine == null)
            {
                runningCoroutine = DownloadTheAudio(ScoreManager.TotalXP.ToString());
                StartCoroutine(runningCoroutine);
            }
            else
                _coroutineQueue.Enqueue(DownloadTheAudio(ScoreManager.TotalXP.ToString()));
        }
        Status.GetComponent<TextMeshProUGUI>().text = ScoreManager.TotalXP + "XP";

        if (EarnedAchievements.Count == 0)
        {
            NoBadges.SetActive(true);
            BadgeSprite.SetActive(false);
            BadgeText.SetActive(false);
            BadgeTopic.SetActive(false);
            Next.SetActive(false);
            Previous.SetActive(false);
            if (!UserCreator.User1.RemainingVision && UserCreator.User1.RemainingHearing)
                if (runningCoroutine == null)
                    StartCoroutine(DownloadTheAudio("Helaas! Je hebt nog geen badges behaald. Probeer de quiz om een badge te verdienen."));
                else
                    _coroutineQueue.Enqueue(DownloadTheAudio("Helaas! Je hebt nog geen badges behaald. Probeer de quiz om een badge te verdienen."));
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
        if (!UserCreator.User1.RemainingVision && UserCreator.User1.RemainingHearing)
            ReadBadges();
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
        
        if (!UserCreator.User1.RemainingVision && UserCreator.User1.RemainingHearing)
            ReadBadges();
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

        if (!UserCreator.User1.RemainingVision && UserCreator.User1.RemainingHearing)
            ReadBadges();
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

        runningCoroutine = null; 
        if (_coroutineQueue.Count > 0)
        {
            runningCoroutine = _coroutineQueue.Dequeue();
            StartCoroutine(runningCoroutine);
        }
    }

    void ReadBadges()
    {
        if (runningCoroutine == null)
        {
            runningCoroutine = DownloadTheAudio("Onderwerp");
            StartCoroutine(runningCoroutine);
        }
        else
            _coroutineQueue.Enqueue(runningCoroutine);

        _coroutineQueue.Enqueue(DownloadTheAudio(EarnedAchievements[_badgeIndex].AchievementTopic));
        _coroutineQueue.Enqueue(DownloadTheAudio("Inhoud"));
        _coroutineQueue.Enqueue(DownloadTheAudio(EarnedAchievements[_badgeIndex].AchievementText));
    }
}