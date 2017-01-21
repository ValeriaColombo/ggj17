using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharSelectionMenu : MonoBehaviour {

	public Splash splash;
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
		timer -= Time.fixedDeltaTime;

		if (timer < 0)
		{
			splash.gameObject.SetActive (true);
			gameObject.SetActive (false);
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
