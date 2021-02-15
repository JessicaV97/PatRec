using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class patternProducer : MonoBehaviour{
	
	public GameObject v1;
	public GameObject v2;
	public GameObject v3;
	public GameObject v4;
	public GameObject v5;
	public GameObject v6;
	public GameObject v7;
	public GameObject v8;
	public GameObject v9;
	public GameObject v10;
	public GameObject v11;
	public GameObject v12;
	public GameObject v13;
	public GameObject v14;
	public GameObject v15;
	public GameObject v16;
	
	public Sprite v;
	public Sprite vsel;
	
	public float Delay = 0.5f;
	private WaitForSeconds _delay;
	
	List<GameObject> optionA = new List<GameObject>();
	List<GameObject> optionB = new List<GameObject>();
	List<GameObject> optionC = new List<GameObject>();

	
	private void Awake(){
		_delay = new WaitForSeconds(Delay);
	}
	
    public void playA(){
		optionA.Add(v1);
		optionA.Add(v7);
		optionA.Add(v13);
		optionA.Add(v5);
		StartCoroutine(ShowAndHide(optionA));		
    }
	
	    public void playB(){
		optionB.Add(v10);
		optionB.Add(v2);
		optionB.Add(v3);
		optionB.Add(v11);
		StartCoroutine(ShowAndHide(optionB));		
    }
	
	    public void playC(){
		optionC.Add(v2);
		optionC.Add(v16);
		optionC.Add(v13);
		optionC.Add(v7);
		StartCoroutine(ShowAndHide(optionC));		
    }
	
    IEnumerator ShowAndHide(List<GameObject> buttonList){
		foreach (GameObject go in buttonList){
			go.SetActive(false);
			yield return _delay;
			go.SetActive(true);
		}
    }
	

}
