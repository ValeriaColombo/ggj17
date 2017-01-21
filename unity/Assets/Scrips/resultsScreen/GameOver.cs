using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour 
{
	public FabrisTauntrum asset1;
	public FabrisTauntrum asset2;
	public FabrisTauntrum2 asset3;

	private float timer;

	public void ShowGameOver(bool team) //0 red, 1 blue
	{
		asset1.Setup (team);
		asset2.Setup (team);
		asset3.Setup (team);

		gameObject.SetActive (true);

		timer = 10;
	}

	private void Update()
	{
		timer -= Time.fixedDeltaTime;
		if (timer < 0 || Input.anyKey)
		{
			SceneManager.LoadScene ("menu");
		}
	}
}
