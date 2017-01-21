using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Configs : MonoBehaviour {

	private static Configs _instance;
	public static Configs Instance()
	{
		return _instance;
	}

	private void Awake ()
	{
		_instance = this;
	}

	public int GameTime = 120;
	public int TimeToPachaDoom = 15;

	public int CowSpawnTimeFrom = 8;
	public int CowSpawnTimeTo = 10;

	public int WaveIntensity = 4;

	public float BombTime = 10;

	public float StompCooldown = 2;
	public float PickUpCooldown = 1;
}
