using Happify.User;
using UnityEngine;

/// <summary>
/// Class that enables creation of scriptable objects for the patterns. 
/// It stores their name, json file to play the pattern, their visualization and difficulty.
/// </summary>
[CreateAssetMenu(fileName = "New Pattern", menuName = "Patterns")]
public class SOPattern : ScriptableObject
{
    public string PatternName;
    public TextAsset PatternJson;
    public Sprite PatternImage;
    public Level Difficulty;
}
