using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class livesManagement : MonoBehaviour
{
	public TextMeshProUGUI livesLeft;
	public static int livesNr; 
	public TextMeshProUGUI durationBeforeNewLife;
	float currentTime = 10f;
	
    // Start is called before the first frame update
    void Start()
    {
		livesNr = QuizManager.getLives();
		currentTime = 10f;
    }

    // Update is called once per frame
	private void Update()
	{
		Time.timeScale = 1.0f;
		livesLeft.GetComponent<TextMeshProUGUI>().text = livesNr.ToString();
		currentTime -= 1* Time.deltaTime;
		// Debug.Log(currentTime);
		durationBeforeNewLife.GetComponent<TextMeshProUGUI>().text = currentTime.ToString("0");
		if (livesNr < 3 && currentTime < 0)
			livesNr += 1; 
		else if (livesNr ==3)
		{
			durationBeforeNewLife.GetComponent<TextMeshProUGUI>().text = "Ready to play!";
			currentTime = 10f;
		}
	}
}
