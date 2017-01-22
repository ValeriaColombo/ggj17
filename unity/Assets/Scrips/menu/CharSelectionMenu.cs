using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;

public class CharSelectionMenu : MonoBehaviour 
{
	public Splash splash;
	public Credits credits;
	public float TimeMenuIddle = 30;
	public List<PlayerBtn> players;

	private float timer;

	public void OpenScreen ()
	{
		foreach (PlayerBtn p in players) 
		{
			p.ResetTimerCallback = ResetTimer;
			p.OpenScreen ();
		}

		ResetTimer ();
	}

	public void ResetTimer()
	{
		timer = TimeMenuIddle;
	}

	private void Update()
	{
		if (Input.GetKeyDown (KeyCode.C) || XCI.GetButton (XboxButton.B)) 
		{
			SoundManager.Instance.PlayEffect (SoundManager.Instance.effectButtonMenu);
			gameObject.SetActive (false);
			credits.gameObject.SetActive (true);
		}

		timer -= Time.fixedDeltaTime;

		if (timer < 0)
		{
			splash.gameObject.SetActive (true);
			gameObject.SetActive (false);
			splash.Activate ();
		}

		foreach (PlayerBtn p in players) 
		{
			if (!p.IsReady)
				return;
		}

		timer = 10000;
		SceneManager.LoadScene ("main");
	}
}
