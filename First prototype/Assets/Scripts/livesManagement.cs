using UnityEngine;
using TMPro;
using Happify.User;
using UnityEngine.UI;

/// <summary>
/// Class that takes care off recharging lives when user has at least lost 1. Used in 'scn_Levels'.
/// </summary>
public class LivesManagement : MonoBehaviour
{
    //Collect game objects used in this script
    [SerializeField]
    private TextMeshProUGUI _livesLeft;
    [SerializeField]
    private Slider _loadingBar;
    [SerializeField]
    private TMP_Text _loadingText;

	private UserDescription currentUser;

    /// <summary>
    /// Collect the information of the current user. Set the value for number of lives. 
    /// If the user has 3 lives, visualize the charging bar as full. 
    /// </summary>
    void Start()
    {
        currentUser = UserManager.Instance.CurrentUser;
        _livesLeft.text = UserManager.Instance.CurrentUser.NrOfLives.ToString();
        if (currentUser == null || currentUser.NrOfLives == 3)
        {
            _loadingBar.value = 100;
            _loadingText.text = "Compleet";
        }
	}

    /// <summary>
    /// Check if the user still has 3 lives. If so, visualize charging bar as full. 
    /// Otherwise visualize the value of charging based on data from the 'UserManager'
    /// </summary>
	private void Update()
	{
		//Time.timeScale = 1.0f;
		_livesLeft.text = UserManager.Instance.CurrentUser.NrOfLives.ToString();

        if (currentUser == null || currentUser.NrOfLives == 3)
        {
            _loadingBar.value = 100;
            _loadingText.text = "Compleet";
        }
        else
        {
            _loadingText.text = Mathf.Round(UserManager.Instance.Difference / UserManager.Instance.NewLifeDuration*100).ToString() + "%";
            _loadingBar.value = Mathf.Clamp01(UserManager.Instance.Difference / UserManager.Instance.NewLifeDuration );
        }
    }

    /// <summary>
    /// Function that returns the value of charging for speech synthesis in blind mode. 
    /// </summary>
    /// <returns></returns>
    public static string GetValue()
    {
        float value = Mathf.Round(UserManager.Instance.Difference / UserManager.Instance.NewLifeDuration * 100);
        if (value >= 100 || value <= 0)
            return "100%";
        else
            return value.ToString() + "%";
    }
}
