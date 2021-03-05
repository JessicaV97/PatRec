using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternComplete {
    
	public string patternName { get; set; }
    public Pattern patternJson { get; set; }
    public Sprite patternImage { get; set; } 
    public int difficulty { get; set; }



	// public void setPatternComplete(string name, Pattern json, Sprite image, int diff){
		// patternName = name;
		// patternJson = json;
		// patternImage = image;
		// difficulty = diff; 
	// }

	// public void Awake(){
		
		// public Pattern alarmPattern;
		// public Pattern alarm2Pattern;
		// public Pattern angryPattern;
		// public Pattern applausePattern;
		// public Pattern endPattern;
		// public Pattern laughingPattern;
		// public Pattern questionPattern;
		// public Pattern silencePattern;
			
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
	// }

	// static void Start(){
		// PatternComplete alarm = new PatternComplete();
		// alarm.SetPatternComplete(Alarm, )
	// }
}
