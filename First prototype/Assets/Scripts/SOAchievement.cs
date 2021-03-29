using UnityEngine;

[CreateAssetMenu(fileName = "New Achievement", menuName = "Achievements")]
public class SOAchievement : ScriptableObject
{
    public string AchievementText;
    public string PopUpText;
    public Sprite BadgeImage;
    public string AchievementTopic;
    public int AchievementLevel;
}
