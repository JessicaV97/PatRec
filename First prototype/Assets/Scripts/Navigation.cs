using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Navigation : MonoBehaviour
{
    public void goToSettings()
    {
        SceneManager.LoadScene("scn_Settings");
    }

    public void goToAchievements()
    {
        SceneManager.LoadScene("scn_Achievements");
    }

    public void goToHome()
    {
        SceneManager.LoadScene("scn_MainMenu");
    }
    public void goToLevels()
    {
        SceneManager.LoadScene("scn_Levels");
    }

    public void goToStudyEnvironment()
    {
        SceneManager.LoadScene("scn_StudyEnvironment");
    }

    public void playGame()
    {
        SceneManager.LoadScene("scn_MainGameScreen");
    }
}
