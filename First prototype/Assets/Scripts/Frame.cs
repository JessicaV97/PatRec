using System.Collections.Generic;

[System.Serializable]
public class Frame {
    public double time { get; set; } 
    public List<Actuators> actuators { get; set; } 
}
