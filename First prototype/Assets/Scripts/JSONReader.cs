using System.IO;
using UnityEngine;
using UnityEngine.UI;

 
public class JSONReader : MonoBehaviour
{
    public class Actuator    {
        public int pin { get; set; } 
        // public int value { get; set; } 
    }

    public class Frame  {
        public double time { get; set; } 
        public List<Actuator> actuators { get; set; } 
    }

    public class Alarm    {
        public string islooped { get; set; } 
        public double duration { get; set; } 
        public int interpolation { get; set; } 
        public List<Frame> frames { get; set; } 
    }
	public void start() {
		Alarm myDeserializedClass = JsonUtility.FromJson<Alarm>("C:/Users/s162094/Documents/I-TECH/Y2 (2020-2021)/Q3/Internship/First prototype/Assets/Scripts/alarm.json");
	}
}
