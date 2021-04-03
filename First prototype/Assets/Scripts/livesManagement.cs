using UnityEngine;
using TMPro;

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
		Time.timeScale = 1.0f;
		LivesLeft/*.GetComponent<TextMeshProUGUI>()*/.text = UserCreator.User1.NrOfLives.ToString();
		_currentTime -= 1* Time.deltaTime;
		DurationBeforeNewLife/*.GetComponent<TextMeshProUGUI>()*/.text = _currentTime.ToString("0");
		if (UserCreator.User1.NrOfLives < 3 && _currentTime < 0)
			UserCreator.User1.NrOfLives += 1; 
		else if (UserCreator.User1.NrOfLives == 3)
		{
			DurationBeforeNewLife/*.GetComponent<TextMeshProUGUI>()*/.text = "Ready to play!";
			_currentTime = 10f;
		}
	}
}
