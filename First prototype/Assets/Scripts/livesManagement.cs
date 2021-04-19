using UnityEngine;
using TMPro;
using Happify.User;
using UnityEngine.UI;

public class LivesManagement : MonoBehaviour
{
    public TextMeshProUGUI LivesLeft;
    //public TextMeshProUGUI DurationBeforeNewLife;

    public Slider loadingBar;
    public TMP_Text loadingText;

	private UserDescription currentUser;

    private void Awake()
    {
		
	}

    // Start is called before the first frame update
    void Start()
    {
        currentUser = UserManager.Instance.CurrentUser;
        Debug.Log(currentUser.Name);
        Debug.Log(currentUser.NrOfLives);
        Debug.Log("livesmanagement lives" + UserManager.Instance.CurrentUser.NrOfLives);
        LivesLeft.text = UserManager.Instance.CurrentUser.NrOfLives.ToString();
        if (currentUser == null || currentUser.NrOfLives == 3)
        {
            loadingBar.value = 100;
            loadingText.text = "Compleet";
        }
	}

    // Update is called once per frame
	private void Update()
	{
		//Time.timeScale = 1.0f;
		LivesLeft.text = UserManager.Instance.CurrentUser.NrOfLives.ToString();

        if (currentUser == null || currentUser.NrOfLives == 3)
        {
            loadingBar.value = 100;
            loadingText.text = "Compleet";
        }
        else
        {
            loadingText.text = Mathf.Round(UserManager.Instance.Difference / UserManager.Instance.NewLifeDuration*100).ToString() + "%";
            loadingBar.value = Mathf.Clamp01(UserManager.Instance.Difference / UserManager.Instance.NewLifeDuration );
        }
    }

    public static string getValue()
    {
        float value = Mathf.Round(UserManager.Instance.Difference / UserManager.Instance.NewLifeDuration * 100);
        if (value >= 100 || value <= 0)
            return "100%";
        else
            return value.ToString() + "%";
    }
}
