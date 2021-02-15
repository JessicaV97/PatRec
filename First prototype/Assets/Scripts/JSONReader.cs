using System.IO;
using UnityEngine;
using UnityEngine.UI;

 
public class JSONReader : MonoBehaviour
{
 	public void Start() {
		Alarm myDeserializedPattern = JsonUtility.FromJson<Alarm>("C:/Users/s162094/Documents/I-TECH/Y2 (2020-2021)/Q3/Internship/internship/First prototype/Assets/Scripts/alarm.json");
		Debug.Log("is the pattern looped? " + myDeserializedPattern.islooped);
	}
}
