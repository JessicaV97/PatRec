// using System.IO;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using Newtonsoft.Json;
// // using System.Text.Json;

 
// public class Json3 : MonoBehaviour
// {
	// public GameObject v1;
	// public GameObject v2;
	// public GameObject v3;
	// public GameObject v4;
	// public GameObject v5;
	// public GameObject v6;
	// public GameObject v7;
	// public GameObject v8;
	// public GameObject v9;
	
	// public TextAsset alarm;
	// public TextAsset alarm2;
	// public TextAsset angry;
	// public TextAsset applause;
	// public TextAsset end;
	// public TextAsset laughing;
	// public TextAsset question;
	// public TextAsset silence;
	
	// public float Delay = 0.5f;
	// private WaitForSeconds _delay;
	
	// public Pattern alarmPattern;
	// public Pattern alarm2Pattern;
	// public Pattern angryPattern;
	// public Pattern applausePattern;
	// public Pattern endPattern;
	// public Pattern laughingPattern;
	// public Pattern questionPattern;
	// public Pattern silencePattern;

	// // public List<string> activeActuators;
	// // public List<string> allActuators;
	
	// // public Pattern alarmPattern;
	// // public Pattern endPattern;
	// // public Pattern laughingPattern;
	
 	// // private void Awake(){
		// // 
	// // }
	
	
	// public void Awake(){
		
        // //Herschrijven tot voor json bestand in mapje doe json.convert.DeserializeObject<Pattern>(naam.text)
		// alarmPattern = JsonConvert.DeserializeObject<Pattern>(alarm.text);
		// alarm2Pattern = JsonConvert.DeserializeObject<Pattern>(alarm2.text);
		// angryPattern = JsonConvert.DeserializeObject<Pattern>(angry.text);
		// applausePattern = JsonConvert.DeserializeObject<Pattern>(applause.text);
		// endPattern = JsonConvert.DeserializeObject<Pattern>(end.text);
		// laughingPattern = JsonConvert.DeserializeObject<Pattern>(laughing.text);
		// questionPattern = JsonConvert.DeserializeObject<Pattern>(question.text);
		// silencePattern = JsonConvert.DeserializeObject<Pattern>(silence.text);
		
		// _delay = new WaitForSeconds(Delay);

	// }
	
	
	// // public void printStatement(){
		// // Debug.Log(alarmPattern.islooped);
		// // Debug.Log($"Pattern duration? {alarmPattern.duration}");
		// // Debug.Log($"Interpolation?" + alarmPattern.interpolation);
		// // foreach (Frame frame in alarmPattern.frames){
			// // Debug.Log("Found time " + frame.time);
			// // foreach (Actuators actuator in frame.actuators){
				// // Debug.Log(actuator.pin);
				// // Debug.Log(actuator.value);
			// // }
		// // }
	// // }
	
	// // public void playA(){
		// // createListofLists(alarmPattern);
		// // foreach (List<string> actuatorPerFrame in allActuators){
			// // StartCoroutine(ShowAndHide(actuatorPerFrame));
			// // StopAllCoroutines();
		// // }
		// // // StartCoroutine(ShowAndHide(alarmPattern));
	// // }
	
	// public void playA(){
		// int [][] array = new int [alarmPattern.frames.Count][9];
		// for (var i = 0; i<alarmPattern.frames.Count; i++){
			// for (var j = 0; j<alarmPattern.frames[i].actuators.Count; j++){
				// if (alarmPattern.frames[i].actuators[j].value == 255){
					// array[i][j] =  alarmPattern.frames[i].actuators[j].pin;
				// } else {
					// array[i][j] =  0;
				// }
			// } 
		// }
		// for (int i = 0; i<alarmPattern.frames.Count; i++){
			// StartCoroutine(ShowAndHide(array[i]));
		// }
		
	// }
	

	
	// // public void createListofLists(Pattern pattern){
		// // foreach (Frame frame in pattern.frames){
			// // foreach (Actuators actuator in frame.actuators){
				// // if (actuator.value == 255){
					// // activeActuators.Add(actuator.pin);
				// // }
			// // } allActuators.Add(activeActuators);
			// // activeActuators.Clear();
		// // }
	// // }
	
	
	// IEnumerator ShowAndHide(int[] actuators){
		// foreach (int i in actuators){
			// if (i == 1){
				// v1.SetActive(false);
				// yield return _delay;
				// v1.SetActive(true);
			// } 
			// if (i == 2){
				// v2.SetActive(false);
				// yield return _delay;
				// v2.SetActive(true);
			// }
			// if (i == 3){
				// v3.SetActive(false);
				// yield return _delay;
				// v3.SetActive(true);
			// }
			// if (i == 4){
				// v4.SetActive(false);
				// yield return _delay;
				// v4.SetActive(true);
			// }
			// if (i == 5){
				// v5.SetActive(false);
				// yield return _delay;
				// v5.SetActive(true);
			// }
			// if (i == 6){
				// v6.SetActive(false);
				// yield return _delay;
				// v6.SetActive(true);
			// }
			// if (i == 7){
				// v7.SetActive(false);
				// yield return _delay;
				// v7.SetActive(true);
			// }
			// if (i == 8){
				// v8.SetActive(false);
				// yield return _delay;
				// v8.SetActive(true);
			// }
			// if (i == 9){
				// v9.SetActive(false);
				// yield return _delay;
				// v9.SetActive(true);
			// }
				

		// }
    // }
// }
