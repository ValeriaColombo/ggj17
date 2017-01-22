using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MonoBehaviour {

	public CharSelectionMenu menu;

	void Start () 
	{
		SoundManager.Instance.PlayMusic (SoundManager.Instance.musicMenu, true);
		gameObject.SetActive (true);
	}
	
	void Update () 
	{
		if (Input.anyKeyDown) 
		{
			SoundManager.Instance.PlayEffect (SoundManager.Instance.effectButtonMenu);
			gameObject.SetActive (false);
			menu.gameObject.SetActive (true);
			menu.OpenScreen ();
		}
	}
}
