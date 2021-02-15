using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Alarm {
    public bool islooped { get; set; } 
    public double duration { get; set; } 
    public int interpolation { get; set; } 
    public List<Frame> frames { get; set; } 
}
