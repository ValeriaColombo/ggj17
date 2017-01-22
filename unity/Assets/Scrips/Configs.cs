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

    public float PlayerNormalSpeed = 5f;
    public float HoldingCowPlayerSpeed = 2.5f;

    [HideInInspector] public Color ReadTeamColor = new Color(1, 114f / 255f, 114f / 255f, 1f);
    [HideInInspector] public Color BlueTeamColor = new Color(114f / 255f, 208f / 255f, 1f, 1f);
}
