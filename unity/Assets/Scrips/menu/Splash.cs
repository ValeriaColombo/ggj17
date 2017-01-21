using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MonoBehaviour {

	public CharSelectionMenu menu;

	void Start () 
	{
		gameObject.SetActive (true);
	}
	
	void Update () 
	{
		if (Input.anyKeyDown) 
		{
			gameObject.SetActive (false);
			menu.gameObject.SetActive (true);
			menu.OpenScreen ();
		}
	}
}
