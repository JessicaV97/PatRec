using UnityEngine;
using TMPro;
using Happify.User;

public class LivesManagement : MonoBehaviour
{
	public TextMeshProUGUI LivesLeft;
	public TextMeshProUGUI DurationBeforeNewLife;
	private float _currentTime = 10f;

	// Start is called before the first frame update
	void Start()
    {
		_currentTime = 10f;
    }

    // Update is called once per frame
	private void Update()
	{
		UserDescription currentUser = UserManager.Instance.CurrentUser;
		Time.timeScale = 1.0f;
		LivesLeft/*.GetComponent<TextMeshProUGUI>()*/.text = currentUser.NrOfLives.ToString();
		_currentTime -= 1* Time.deltaTime;
		DurationBeforeNewLife/*.GetComponent<TextMeshProUGUI>()*/.text = _currentTime.ToString("0");
		if (currentUser.NrOfLives < 3 && _currentTime < 0)
			currentUser.NrOfLives++; 
		else if (currentUser.NrOfLives == 3)
		{
			DurationBeforeNewLife/*.GetComponent<TextMeshProUGUI>()*/.text = "Ready to play!";
			_currentTime = 10f;
		}
	}
}
