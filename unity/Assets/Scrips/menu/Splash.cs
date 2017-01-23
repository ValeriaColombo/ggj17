using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class Splash : MonoBehaviour {

	public CharSelectionMenu menu;
	public Credits credits;

	void Start () 
	{
		SoundManager.Instance.PlayMusic (SoundManager.Instance.musicMenu, true);
		gameObject.SetActive (true);
		menu.gameObject.SetActive (false);
		credits.gameObject.SetActive (false);

		Activate ();
	}

	public void Activate()
	{
		SoundManager.Instance.PlayEffect (SoundManager.Instance.effectSplashName);
	}

	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.B) || XCI.GetButton (XboxButton.B)) 
		{
			SoundManager.Instance.PlayEffect (SoundManager.Instance.effectButtonMenu);
			gameObject.SetActive (false);
			credits.gameObject.SetActive (true);
		}
		else if (Input.anyKeyDown) 
		{
			SoundManager.Instance.PlayEffect (SoundManager.Instance.effectButtonMenu);
			gameObject.SetActive (false);
			menu.gameObject.SetActive (true);
			menu.OpenScreen ();
		}
	}
}
