using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lechegedon : MonoBehaviour 
{
	public GameObject meteoritePrefab;

	private bool letItBeRain = false;

	public void StartLechegedon()
	{
		letItBeRain = true;
	}

	private void Update()
	{
		if (letItBeRain)
		{
			if (Random.Range (0, 100) < 1)
			{
				GameObject lechazo = Instantiate (meteoritePrefab);
				meteoritePrefab.transform.SetParent (transform.parent);
				meteoritePrefab.transform.localPosition = new Vector3 (Random.Range (-8f, 8f), 10, Random.Range (-14f, 14f));
			}
		}
	}
}
