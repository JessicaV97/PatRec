using Happify.User;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pattern", menuName = "Patterns")]
public class SOPattern : ScriptableObject
{
    public string PatternName;
    public TextAsset PatternJson;
    public Sprite PatternImage;
    public Level Difficulty;
}
