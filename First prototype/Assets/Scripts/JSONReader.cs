using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
// using System.Text.Json;

 
public class JSONReader : MonoBehaviour
{
	public TextAsset textAsset;
 	public void Start() {
		
        Alarm myDeserializedPattern = JsonConvert.DeserializeObject<Alarm>(textAsset.text);
        Debug.Log(myDeserializedPattern.islooped);
		Debug.Log($"Pattern duration? {myDeserializedPattern.duration}");
		Debug.Log($"Interpolation?" + myDeserializedPattern.interpolation);
		foreach (Frame frame in myDeserializedPattern.frames){
			Debug.Log("Found time " + frame.time);
			foreach (Actuators actuator in frame.actuators){
				Debug.Log(actuator.pin);
				Debug.Log(actuator.value);
			}
		}
	}
}
