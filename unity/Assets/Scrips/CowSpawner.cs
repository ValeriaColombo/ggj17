using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowSpawner : MonoBehaviour 
{
	// X fuerza hacia los costados, Z fuerza hacia adelante
	public float closeFromX;
	public float closeToX;
	public float closeFromZ;
	public float closeToZ;
	public float farFromX;
	public float farToX;
	public float farFromZ;
	public float farToZ;

	private float timer;

	private void Start()
	{
		timer = Random.Range (0f, 2f);
	}

	private void Update()
	{
		timer -= Time.fixedDeltaTime;

		if (timer <= 0)
		{
			SpawnCow ();
		}
	}

	private void SpawnCow()
	{
		timer = Random.Range (Configs.Instance().CowSpawnTimeFrom, Configs.Instance().CowSpawnTimeTo);

		GameObject cow = GameObjectsPool.Instance ().GiveMeACow ();
		cow.transform.SetParent (transform.parent);
		cow.transform.localPosition = transform.localPosition;
		cow.GetComponent<Cow> ().Reset ();
		cow.GetComponent<Cow> ().cowImg.flipX = farToZ < 0;

		Vector3 force;
		if (Random.Range (0, 10) < 1)
		{
			force = new Vector3 (Random.Range(farFromX, farToX), 5, Random.Range(farFromZ, farToZ));
		}
		else
		{
			force = new Vector3 (Random.Range(closeFromX, closeToX), 2, Random.Range(closeFromZ, closeToZ));
		}

		cow.GetComponent<Rigidbody> ().velocity = Vector3.zero;
		cow.GetComponent<Rigidbody> ().AddForce (force, ForceMode.Impulse);
	}
}
