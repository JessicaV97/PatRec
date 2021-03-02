// using System.IO;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using Newtonsoft.Json;

// public class Json5 : MonoBehaviour
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
	// public IDictionary<int, GameObject> intToGo = new Dictionary<int, GameObject>();

	
	// public TextAsset alarm;
	// public TextAsset alarm2;
	// public TextAsset angry;
	// // public TextAsset applause;
	// // public TextAsset end;
	// // public TextAsset laughing;
	// // public TextAsset question;
	// // public TextAsset silence;
	
	// public float Delay = 0.5f;
	// private WaitForSeconds _delay;
	// public  bool isRunning = false;
	
	// public Pattern alarmPattern;
	// public Pattern alarm2Pattern;
	// public Pattern angryPattern;
	// // public Pattern applausePattern;
	// // public Pattern endPattern;
	// // public Pattern laughingPattern;
	// // public Pattern questionPattern;
	// // public Pattern silencePattern;


	
	// public void Awake(){
		
        // //Herschrijven tot voor json bestand in mapje doe json.convert.DeserializeObject<Pattern>(naam.text)
		// alarmPattern = JsonConvert.DeserializeObject<Pattern>(alarm.text);
		// alarm2Pattern = JsonConvert.DeserializeObject<Pattern>(alarm2.text);
		// angryPattern = JsonConvert.DeserializeObject<Pattern>(angry.text);
		// // applausePattern = JsonConvert.DeserializeObject<Pattern>(applause.text);
		// // endPattern = JsonConvert.DeserializeObject<Pattern>(end.text);
		// // laughingPattern = JsonConvert.DeserializeObject<Pattern>(laughing.text);
		// // questionPattern = JsonConvert.DeserializeObject<Pattern>(question.text);
		// // silencePattern = JsonConvert.DeserializeObject<Pattern>(silence.text);
		
		// intToGo.Add(1, v1);
		// intToGo.Add(2, v2);
		// intToGo.Add(3, v3);
		// intToGo.Add(4, v4);
		// intToGo.Add(5, v5);
		// intToGo.Add(6, v6);
		// intToGo.Add(7, v7);
		// intToGo.Add(8, v8);
		// intToGo.Add(9, v9);
		
		// _delay = new WaitForSeconds(Delay);

	// }
	
	// public void playA(){
		// StartCoroutine(ShowAndHide());
	// }
	
	// IEnumerator ShowAndHide(){
		// List <int> vms = new List<int>();
		// for (int i=0; i < alarmPattern.frames.Count; i++){	
			// for (int j=0; j < alarmPattern.frames[i].actuators.Count; j++){
				// if (alarmPattern.frames[i].actuators[j].value == 255){
					// vms.Add(alarmPattern.frames[i].actuators[j].pin);
				// }
			// }
			// List <GameObject> gos = new List<GameObject>();

			// for (int i =0; i<vms.Count; i++){
				// gos.Add(intToGo[vms[i]]);
			// }
			
			// foreach (GameObject go in gos){
				// go.SetActive(false);
				// yield return _delay;
				// go.SetActive(true);
			// }
			// // StartCoroutine(ShowAndHide(vms));
			// vms.Clear();		
		
		// }
	// }
	
// }