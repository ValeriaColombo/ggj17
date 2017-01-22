using System;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectsPool : MonoBehaviour
{
	public GameObject cowPrefab;
	public GameObject meteoritePrefab;
	public GameObject dropWavePrefab;
	public GameObject dropSplashPrefab;
	public GameObject stompWavePrefab;

	private static GameObjectsPool _instance;
	public static GameObjectsPool Instance()
	{
		return _instance;
	}

	private List<GameObject> meteorites;
	private List<GameObject> cows;
	private List<GameObject> dropWaves;
	private List<GameObject> dropSplashes;
	private List<GameObject> stompWaves;

	private void Awake ()
	{
		_instance = this;

		meteorites = new List<GameObject> ();
		cows = new List<GameObject> ();
		dropWaves = new List<GameObject> ();
		dropSplashes = new List<GameObject> ();
		stompWaves = new List<GameObject> ();
	}

	public GameObject GiveMeAStompWave()
	{
		if (stompWaves.Count > 0) 
		{
			GameObject aSW = stompWaves [0];
			stompWaves.RemoveAt (0);
			aSW.SetActive (true);
			return aSW;
		}

		return Instantiate (stompWavePrefab);
	}

	public void FreeThisStompWave(GameObject go)
	{
		go.SetActive(false);
		stompWaves.Add (go);
		go.transform.SetParent (transform);
	}

	public GameObject GiveMeACow()
	{
		if (cows.Count > 0) 
		{
			GameObject aCow = cows [0];
			cows.RemoveAt (0);
			aCow.SetActive (true);
			return aCow;
		}

		return Instantiate (cowPrefab);
	}

	public void FreeThisCow(GameObject go)
	{
		go.SetActive(false);
		cows.Add (go);
		go.transform.SetParent (transform);
	}

	public GameObject GiveMeAMeteorite()
	{
		if (meteorites.Count > 0) 
		{
			GameObject aMet = meteorites [0];
			meteorites.RemoveAt (0);
			aMet.SetActive (true);
			return aMet;
		}

		return Instantiate (meteoritePrefab);
	}

	public void FreeThisMeteorite(GameObject go)
	{
		go.SetActive(false);
		meteorites.Add (go);
		go.transform.SetParent (transform);
	}

	public GameObject GiveMeADropWave()
	{
		if (dropWaves.Count > 0) 
		{
			GameObject aDR = dropWaves [0];
			dropWaves.RemoveAt (0);
			aDR.SetActive (true);
			return aDR;
		}

		return Instantiate (dropWavePrefab);
	}

	public void FreeThisDropWave(GameObject go)
	{
		go.SetActive(false);
		dropWaves.Add (go);
		go.transform.SetParent (transform);
	}

	public GameObject GiveMeADropSplash()
	{
		if (dropWaves.Count > 0) 
		{
			GameObject aDS = dropSplashes [0];
			dropSplashes.RemoveAt (0);
			aDS.SetActive (true);
			return aDS;
		}

		return Instantiate (dropSplashPrefab);
	}

	public void FreeThisDropSplash(GameObject go)
	{
		go.SetActive(false);
		dropSplashes.Add (go);
		go.transform.SetParent (transform);
	}
}
