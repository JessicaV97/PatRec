using UnityEngine;

/// <summary>
/// Class that enables creation of scriptable objects for the achievements (badges)
/// For each badge it stores text for on the badge, text for the achievement pop up, a visualization, 
/// the context it belongs to and which level it matches. 
/// </summary>
[CreateAssetMenu(fileName = "New Achievement", menuName = "Achievements")]
public class SOAchievement : ScriptableObject
{
    public string AchievementText;
    public string PopUpText;
    public Sprite BadgeImage;
    public string AchievementTopic;
    public int AchievementLevel;

}
