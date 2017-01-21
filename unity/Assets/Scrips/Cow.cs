using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow : MonoBehaviour 
{
	enum CowState 
	{
		INACTIVE, ACTIVE, EXPLODING
	}

	private CowState state;
	private bool isTouchingTheFloor;

	public void Reset()
	{
		state = CowState.INACTIVE;
		isTouchingTheFloor = false;
	}

	public void TouchedByPlayer()
	{
		state = CowState.ACTIVE;
	}

	void OnCollisionEnter(Collision col)
	{
		if (state == CowState.INACTIVE || state == CowState.EXPLODING)
		{
			if (col.gameObject.name.IndexOf ("floor") > -1) 
			{
				isTouchingTheFloor = true;
			}
		}
	}

	void Update () 
	{
		if ((state == CowState.INACTIVE || state == CowState.EXPLODING) && isTouchingTheFloor) 
		{
			GetComponent<Rigidbody> ().velocity = Vector3.zero;
		}

		if (transform.localPosition.y < -30) 
		{
			gameObject.SetActive (false);
			GameObjectsPool.Instance ().FreeThisCow (gameObject);
		}
	}
}
