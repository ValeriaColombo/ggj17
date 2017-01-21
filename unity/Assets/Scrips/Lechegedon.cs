using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lechegedon : MonoBehaviour 
{
	private bool letItBeRain = false;

	public void StartLechegedon()
	{
		if (letItBeRain)
			prob++;
		else
			prob = 2;
		
		letItBeRain = true;
	}

	int prob;
	private void Update()
	{
		if (letItBeRain)
		{
			if (Random.Range (0, 100) < prob)
			{
				GameObject meteor = GameObjectsPool.Instance ().GiveMeAMeteorite ();
				meteor.transform.SetParent (transform.parent);
				meteor.transform.localPosition = new Vector3 (Random.Range (-8f, 8f), 10, Random.Range (-14f, 14f));
				meteor.transform.localRotation = Quaternion.Euler(new Vector3 (0,0,-90));
				meteor.GetComponent<Meteorite> ().Reset ();
			}
		}
	}
}
