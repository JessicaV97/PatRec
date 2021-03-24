using System.Collections.Generic;

[System.Serializable]
public class Pattern {
    public bool islooped { get; set; } 
    public double duration { get; set; } 
    public int interpolation { get; set; } 
    public List<Frame> frames { get; set; } 
}
