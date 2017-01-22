using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using XboxCtrlrInput;

public class PlayerBtn : MonoBehaviour 
{
	public KeyCode keyboardKeyToStart = KeyCode.Alpha1;
	public XboxController controller;

	public GameObject waitingGO;
	public GameObject readyGO;

	public Action ResetTimerCallback;

	private bool isReady;
	public bool IsReady 
	{
		get { return isReady; }
	}

	public void OpenScreen ()
	{
		isReady = false;
		waitingGO.SetActive (true);
		readyGO.SetActive (false);
	}

	private void Update()
	{
		if(Input.GetKeyDown(keyboardKeyToStart) || XCI.GetButton(XboxButton.A, controller))
		{
			ResetTimerCallback ();

			SoundManager.Instance.PlayEffect (SoundManager.Instance.effectButtonMenu);
			isReady = true;
			waitingGO.SetActive (false);
			readyGO.SetActive (true);
		}
	}
}
