using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleWall : MonoBehaviour 
{
	void OnTriggerEnter(Collider col)
	{
		if (col.CompareTag("Player"))
		{
			col.GetComponent<Rigidbody> ().isKinematic = false;
			col.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;
		}
	}
}
