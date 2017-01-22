using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour 
{
	public Splash splash;

	void Update () 
	{
		if (Input.anyKeyDown) 
		{
			SoundManager.Instance.PlayEffect (SoundManager.Instance.effectButtonMenu);
			gameObject.SetActive (false);
			splash.gameObject.SetActive (true);
			splash.Activate ();
		}
	}
}
