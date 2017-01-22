using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lechegedon : MonoBehaviour 
{
	private bool letItBeRain = false;

	public Transform laPachaMa;
	public GameObject teta1;
	public GameObject teta2;
	public Transform cameraTr;

	private Vector3 CamStartPos;

	private void Start()
	{
		teta1.SetActive (false);
		teta2.SetActive (false);
		CamStartPos = cameraTr.localPosition;
		laPachaMa.localPosition = new Vector3 (-32, -37, 0.7f);
	}

	private float pachamamaTimer;
	public void StartThePachamaming(float timeToFullExit)
	{
		if(pachamamaTimer == 0)
			pachamamaTimer = timeToFullExit;
	}

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
		if(pachamamaTimer > 0)
		{
			Vector3 p = laPachaMa.transform.localPosition;

			float distanceLeft = -4.5f - p.y;

			p.y += (Time.fixedDeltaTime * distanceLeft) / pachamamaTimer;

			laPachaMa.transform.localPosition = p;

			pachamamaTimer -= Time.fixedDeltaTime;

			cameraTr.position = new Vector3(CamStartPos.x, CamStartPos.y, CamStartPos.z + Random.Range(-0.01f * (15-pachamamaTimer),0.01f*(15-pachamamaTimer)));

			teta1.SetActive (distanceLeft < .5f);
			teta2.SetActive (distanceLeft < .5f);
		}
		else
		{
			cameraTr.position = CamStartPos;
			if(laPachaMa.localPosition.y > -10)
				laPachaMa.localPosition = new Vector3 (-32, -4.5f, 0.7f);
		}

		if (letItBeRain)
		{
			if (Random.Range (0, 100) < prob)
			{
				GameObject meteor = GameObjectsPool.Instance ().GiveMeAMeteorite ();
				meteor.transform.SetParent (transform.parent);
				meteor.transform.localPosition = new Vector3 (Random.Range (-8f, 8f), 10, Random.Range (-14f, 14f));
				meteor.transform.localRotation = Quaternion.Euler(new Vector3 (57,90,0));
				meteor.GetComponent<Meteorite> ().Reset ();
			}
		}
	}
}
