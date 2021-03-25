using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Navigation : MonoBehaviour
{
    public void GoToSettings()
    {
        SceneManager.LoadScene("scn_Settings");
    }

    public void GoToAchievements()
    {
        SceneManager.LoadScene("scn_Achievements");
    }

    public void GoToHome()
    {
        SceneManager.LoadScene("scn_MainMenu");
    }
    public void GoToLevels()
    {
        SceneManager.LoadScene("scn_Levels");
    }

    public void GoToStudyEnvironment()
    {
        SceneManager.LoadScene("scn_StudyEnvironment");
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("scn_MainGameScreen");
    }
}
