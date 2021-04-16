using Happify.User;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Linq;
using Happify.Scores;

public class AchievementsCollector : MonoBehaviour
{
    public static SOAchievement[] PossibleAchievements; 
    public static List<SOAchievement> EarnedAchievements = new List<SOAchievement>();
	private int _badgeIndex = 0;
    public GameObject NoBadges;
    public GameObject BadgeSprite;
    public GameObject BadgeText;
    public GameObject BadgeTopic;
    public GameObject Status;
    public GameObject Next;
    public GameObject Previous;
    public GameObject ScoreBoard;

    public AudioSource _audio;

    IEnumerator runningCoroutine = null;
    private Queue<IEnumerator> _coroutineQueue = new Queue<IEnumerator>();

    private UserDescription currentUser;
    private void Awake()
    {
        currentUser = UserManager.Instance.CurrentUser;
        ScoreBoard.SetActive(false);
    }

    void Start()
    {
        if (runningCoroutine == null && currentUser.RemainingHearing && !currentUser.RemainingVision)
        {
            runningCoroutine = (DownloadTheAudio("Badges"));
            StartCoroutine(runningCoroutine);
        }
        else
            _coroutineQueue.Enqueue(DownloadTheAudio("Badge"));

        
        if (!currentUser.RemainingVision && currentUser.RemainingHearing)
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
            if (!currentUser.RemainingVision && currentUser.RemainingHearing)
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

            if (!currentUser.RemainingVision && currentUser.RemainingHearing)
                ReadBadges();
        }
        
    }

    public void ShowScores()
    {
        HighScoreList.Instance.CreateList();
        ScoreBoard.SetActive(true);
    }

    public void HideScores()
    {
        ScoreBoard.SetActive(false);
    }

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

	public void NextBadge()
	{
        if (_badgeIndex == EarnedAchievements.Count - 1)
			_badgeIndex = 0;
		else
			_badgeIndex += 1;
        
        BadgeText.GetComponent<TextMeshProUGUI>().text = EarnedAchievements[_badgeIndex].AchievementText;
        BadgeSprite.GetComponent<Image>().sprite = EarnedAchievements[_badgeIndex].BadgeImage;
        BadgeTopic.GetComponent<TextMeshProUGUI>().text = EarnedAchievements[_badgeIndex].AchievementTopic;
        
        if (!currentUser.RemainingVision && currentUser.RemainingHearing)
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

        if (!currentUser.RemainingVision && currentUser.RemainingHearing)
            ReadBadges();
    }

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