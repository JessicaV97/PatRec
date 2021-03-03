using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMPC : MonoBehaviour
{
    public static bool MPCisShown = true;
	public static bool bodyIsShown = false;
	
	public GameObject MPCMenuUI;
	public GameObject bodyMenuUI;
	
	// Hide mpc options at initiation
	public void Start(){
		MPCMenuUI.SetActive(true);
	}
	
	// If orange button is clicked, hide panel with all mpc options
	public void hideOptions(){
		MPCMenuUI.SetActive(false);
		MPCisShown = false;
	}
	
	// If blue button is clicked, hide panel with all mpc options
	public void showOptions(){
		MPCMenuUI.SetActive(true);
		MPCisShown = true;
	}
}
