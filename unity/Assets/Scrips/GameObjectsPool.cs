using System;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectsPool : MonoBehaviour
{
	public GameObject cowPrefab;
	public GameObject meteoritePrefab;

	private static GameObjectsPool _instance;
	public static GameObjectsPool Instance()
	{
		return _instance;
	}

	private List<GameObject> meteorites;
	private List<GameObject> cows;

	private void Awake ()
	{
		_instance = this;

		meteorites = new List<GameObject> ();
		cows = new List<GameObject> ();
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
}

