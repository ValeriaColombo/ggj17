using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowSpawner : MonoBehaviour 
{
	public GameObject cowPrefab;

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
		timer = Random.Range (8f, 10f);

		GameObject cow = Instantiate (cowPrefab);
		cow.transform.SetParent (transform.parent);
		cow.transform.localPosition = transform.localPosition;

		Vector3 force;
		if (Random.Range (0, 10) < 5)
		{
			force = new Vector3 (Random.Range(farFromX, farToX), 0, Random.Range(farFromZ, farToZ));
		}
		else
		{
			force = new Vector3 (Random.Range(closeFromX, closeToX), 0, Random.Range(closeFromZ, closeToZ));
		}

		cow.GetComponent<Rigidbody> ().AddForce (force, ForceMode.Impulse);
	}
}
