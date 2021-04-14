using UnityEngine;
using TMPro;
using Happify.User;
using UnityEngine.UI;

public class LivesManagement : MonoBehaviour
{
    public TextMeshProUGUI LivesLeft;
    //public TextMeshProUGUI DurationBeforeNewLife;
    //private float _currentTime = 10f;

    public Slider loadingBar;
    public TMP_Text loadingText;

	private UserDescription currentUser;

    private void Awake()
    {
		currentUser = UserManager.Instance.CurrentUser;
	}

    // Start is called before the first frame update
    void Start()
    {
        //_currentTime = 10f;
        if (currentUser == null || currentUser.NrOfLives == 3)
        {
            loadingBar.value = 100;
            loadingText.text = "";
        }
	}

    // Update is called once per frame
	private void Update()
	{
		//Time.timeScale = 1.0f;
		LivesLeft.text = currentUser.NrOfLives.ToString();

        if (currentUser == null || currentUser.NrOfLives == 3)
        {
            loadingBar.value = 100;
            loadingText.text = "";
        }
        else
        {
            loadingText.text = Mathf.Round(UserManager.Instance.Difference / UserManager.Instance.NewLifeDuration*100).ToString() + "%";
            loadingBar.value = Mathf.Clamp01(UserManager.Instance.Difference / UserManager.Instance.NewLifeDuration );
        }
    }
}
