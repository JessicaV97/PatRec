using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Frame {
    public double time { get; set; } 
    public List<Actuators> actuators { get; set; } 
}
