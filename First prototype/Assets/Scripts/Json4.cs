// using System.IO;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using Newtonsoft.Json;

// public class Json4 : MonoBehaviour
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
	// public TextAsset applause;
	// public TextAsset end;
	// public TextAsset laughing;
	// public TextAsset question;
	// public TextAsset silence;
	
	// public float Delay = 0.5f;
	// private WaitForSeconds _delay;
	// public  bool isRunning = false;
	// public List<int> fullList = new List<int>();
	
	// public Pattern alarmPattern;
	// public Pattern alarm2Pattern;
	// public Pattern angryPattern;
	// public Pattern applausePattern;
	// public Pattern endPattern;
	// public Pattern laughingPattern;
	// public Pattern questionPattern;
	// public Pattern silencePattern;
	
	// public Sprite alarmPatternIm;
	// public Sprite alarm2PatternIm;
	// public Sprite angryPatternIm;
	// public Sprite applausePatternIm;
	// public Sprite endPatternIm;
	// public Sprite laughingPatternIm;
	// public Sprite questionPatternIm;
	// public Sprite silencePatternIm;

	// public List<Pattern> emotions = new List<Pattern>();
	// public List<PatternComplete> emotionsComplete = new List<PatternComplete>();	
	
	// public void Awake(){
        // //Herschrijven tot voor json bestand in mapje doe json.convert.DeserializeObject<Pattern>(naam.text)
		// alarmPattern = JsonConvert.DeserializeObject<Pattern>(alarm.text);
		// emotions.Add(alarmPattern);
		// alarm2Pattern = JsonConvert.DeserializeObject<Pattern>(alarm2.text);
		// emotions.Add(alarm2Pattern);
		// angryPattern = JsonConvert.DeserializeObject<Pattern>(angry.text);
		// emotions.Add(angryPattern);
		// applausePattern = JsonConvert.DeserializeObject<Pattern>(applause.text);
		// endPattern = JsonConvert.DeserializeObject<Pattern>(end.text);
		// laughingPattern = JsonConvert.DeserializeObject<Pattern>(laughing.text);
		// questionPattern = JsonConvert.DeserializeObject<Pattern>(question.text);
		// silencePattern = JsonConvert.DeserializeObject<Pattern>(silence.text);
		
		// emotionsComplete.Add(new PatternComplete {patternName = "Alarm", patternJson = alarmPattern, patternImage = alarmPatternIm, difficulty = 1});
		// emotionsComplete.Add(new PatternComplete {patternName = "Alarm2", patternJson = alarm2Pattern, patternImage = alarm2PatternIm, difficulty = 1});
		// emotionsComplete.Add(new PatternComplete {patternName = "Angry", patternJson = angryPattern, patternImage = angryPatternIm, difficulty = 1});
		// emotionsComplete.Add(new PatternComplete {patternName = "Applause", patternJson = applausePattern, patternImage = applausePatternIm, difficulty = 2});
		// emotionsComplete.Add(new PatternComplete {patternName = "End", patternJson = endPattern, patternImage = endPatternIm, difficulty = 2});
		// emotionsComplete.Add(new PatternComplete {patternName = "Laughing", patternJson = laughingPattern, patternImage = laughingPatternIm, difficulty = 2});
		// emotionsComplete.Add(new PatternComplete {patternName = "Silence", patternJson = silencePattern, patternImage = silencePatternIm, difficulty = 3});
		
		// // intToGo.Add(1, v1);
		// // intToGo.Add(2, v2);
		// // intToGo.Add(3, v3);
		// // intToGo.Add(4, v4);
		// // intToGo.Add(5, v5);
		// // intToGo.Add(6, v6);
		// // intToGo.Add(7, v7);
		// // intToGo.Add(8, v8);
		// // intToGo.Add(9, v9);
		
		// // _delay = new WaitForSeconds(Delay);
		
	

	// }

	
	// // public void selectAnswerOptions(){
		// // Random random = new Random();
		// // List<int> numbers = new List<int>();
		// // for (int i = 0; i < 3; i++){
			// // int number = random.Next(0, emotions.Count);
			// // while (numbers.Contains(number)){
				// // number = random.Next(0, emotions.Count);
			// // }
			// // numbers.Add(number);
		// // }
		// // //Option A
		// // playAnswer(numbers[0]);
		// // //Option B
		// // playAnswer(numbers[1]);
		// // //Option C
		// // playAnswer(numbers[2]);
	// // }
	
	// // public void playAnswer(){
		// // List<int> vms = new List<int>();
		// // for (int i=0; i < alarmPattern.frames.Count; i++){	
			// // for (int j=0; j < alarmPattern.frames[i].actuators.Count; j++){
				// // if (alarmPattern.frames[i].actuators[j].value == 255){
					// // vms.Add(alarmPattern.frames[i].actuators[j].pin);
				// // } 
			// // }
			// // Debug.Log(vms.Count);
			// // fullList.AddRange(vms);
			// // // StartCoroutine(ShowAndHide(vms));
			// // vms.Clear();
		// // }
		// // StartCoroutine(ShowAndHide(fullList));
		
	// // }
	

	
	// // IEnumerator ShowAndHide(List<int> vms){
		// // GameObject pin;
		// // Debug.Log(vms.Count);
		// // // foreach (List<int> frame in vms){
			// // // Debug.Log(frame.Count);
			// // foreach (int vmPin in vms){
				// // pin = intToGo[vmPin];
				// // pin.SetActive(false);
				// // yield return _delay;
				// // pin.SetActive(true);
			// // }
		// // // }
		
		
		
		// // // // isRunning = true;
		// // // Debug.Log(vms.Count);
		// // // List <GameObject> gos = new List<GameObject>();

		
		// // // foreach (int vm in vms){
			// // // Debug.Log("Something");
			// // // gos.Add(intToGo[vm]);
			// // // }
			
			
		// // // foreach (GameObject go in gos){
			// // // Debug.Log("plum");
			// // // go.SetActive(false);
			// // // yield return _delay;
			// // // go.SetActive(true);
		// // // } 
		// // // Debug.Log("End");
	// // }
		
		// // isRunning = false;
	
	
// }