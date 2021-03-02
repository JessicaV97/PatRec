using System.IO;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

 
public class Json2 : MonoBehaviour
{
	public GameObject v1;
	public GameObject v2;
	public GameObject v3;
	public GameObject v4;
	public GameObject v5;
	public GameObject v6;
	public GameObject v7;
	public GameObject v8;
	public GameObject v9;
	
	public TextAsset alarm;
	public TextAsset alarm2;
	public TextAsset angry;
	public TextAsset applause;
	public TextAsset end;
	public TextAsset laughing;
	public TextAsset question;
	public TextAsset silence;
	
	public float Delay = 0.5f;
	private WaitForSeconds _delay;
	
	public Pattern alarmPattern;
	public Pattern alarm2Pattern;
	public Pattern angryPattern;
	public Pattern applausePattern;
	public Pattern endPattern;
	public Pattern laughingPattern;
	public Pattern questionPattern;
	public Pattern silencePattern;
	
	public List<int> pins = new List<int>();
	
	// public Pattern alarmPattern;
	// public Pattern endPattern;
	// public Pattern laughingPattern;
	
 	// private void Awake(){
		// 
	// }
	
	public void Awake(){
		
        //Herschrijven tot voor json bestand in mapje doe json.convert.DeserializeObject<Pattern>(naam.text)
		alarmPattern = JsonConvert.DeserializeObject<Pattern>(alarm.text);
		alarm2Pattern = JsonConvert.DeserializeObject<Pattern>(alarm2.text);
		angryPattern = JsonConvert.DeserializeObject<Pattern>(angry.text);
		applausePattern = JsonConvert.DeserializeObject<Pattern>(applause.text);
		endPattern = JsonConvert.DeserializeObject<Pattern>(end.text);
		laughingPattern = JsonConvert.DeserializeObject<Pattern>(laughing.text);
		questionPattern = JsonConvert.DeserializeObject<Pattern>(question.text);
		silencePattern = JsonConvert.DeserializeObject<Pattern>(silence.text);
		
		_delay = new WaitForSeconds(Delay);

		// Debug.Log(alarmPattern.islooped);
		// Debug.Log($"Pattern duration? {alarmPattern.duration}");
		// Debug.Log($"Interpolation?" + alarmPattern.interpolation);
		// foreach (Frame frame in alarmPattern.frames){
			// Debug.Log("Found time " + frame.time);
			// foreach (Actuators actuator in frame.actuators){
				// Debug.Log(actuator.pin);
				// Debug.Log(actuator.value);
			// }
		// }
	}
	
	public void playA(){
		for (var i = 0; i<alarmPattern.frames.Count; i++){
			for (var j = 0; j<alarmPattern.frames[i].actuators.Count; j++){
				if (alarmPattern.frames[i].actuators[j].value == 255){
					pins.Add(alarmPattern.frames[i].actuators[j].pin);
					Debug.Log(alarmPattern.frames[i].actuators[j].pin);
				}
				StartCoroutine(ShowAndHide(pins));
			} 
			pins.Clear();
			
			Debug.Log("Einde der list of actuators");
			foreach( var x in pins) {
				Debug.Log( x.ToString());
			}		
		}	
		
	}
	
	// public void playB(){
		// StartCoroutine(ShowAndHide(endPattern));
	// }
	
	// public void playC(){
		// StartCoroutine(ShowAndHide(laughingPattern));
	// }
	
	
	IEnumerator ShowAndHide(List<int> pin){
		//for element in list if number is something, activate this pin, delay deactivate. 
		// Debug.Log(pattern.islooped);
		foreach (int vm in pin){
			if (vm == 1){
				v1.SetActive(false);
			} if (vm == 2){
				v2.SetActive(false);
			} if (vm == 3){
				v3.SetActive(false);
			} if (vm == 4){
				v4.SetActive(false);
			} if (vm == 5){
				v5.SetActive(false);
			} if (vm == 6){
				v6.SetActive(false);
			} if (vm == 7){
				v7.SetActive(false);
			} if (vm == 8){
				v8.SetActive(false);
			} if (vm == 9){
				v9.SetActive(false);
			} 
		}
		yield return new WaitForSeconds(1.0f);
		v1.SetActive(true);
		v2.SetActive(true);
		v3.SetActive(true);
		v4.SetActive(true);
		v5.SetActive(true);
		v6.SetActive(true);
		v7.SetActive(true);
		v8.SetActive(true);
		v9.SetActive(true);
	} 		  
}
