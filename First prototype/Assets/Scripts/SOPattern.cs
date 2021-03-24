using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pattern", menuName = "Patterns")]
public class SOPattern : ScriptableObject
{
    public string patternName;
    public TextAsset patternJson;
    public Sprite patternImage;
    public int difficulty;
}
