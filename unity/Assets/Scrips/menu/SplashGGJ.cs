using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashGGJ : MonoBehaviour {

	private float timer = 0;

	void Update () 
	{
		timer += Time.fixedDeltaTime;
		if (timer > 3) 
		{
			SceneManager.LoadScene ("menu");
		}
	}
}
