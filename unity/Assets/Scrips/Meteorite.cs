using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : MonoBehaviour {

	public MeshRenderer drop;
	public ParticleSystem explosion;

	private bool exploding;
	private float time_explosion;

	void Awake()
	{
		drop.enabled = true;
		explosion.gameObject.SetActive (false);
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.name.IndexOf ("floor") > -1)
		{
			drop.enabled = false;
			explosion.gameObject.SetActive (true);
			exploding = true;
			time_explosion = 1;
		}
		else if (col.gameObject.name.IndexOf ("player") > -1)
		{
			
		}
	}

	void Update()
	{
		if (exploding) 
		{
			time_explosion -= Time.fixedDeltaTime;
			if (time_explosion < 0) 
			{
				exploding = false;
				gameObject.SetActive (false);

				GameObjectsPool.Instance ().FreeThisMeteorite (gameObject);
			}
		}
	}
}
