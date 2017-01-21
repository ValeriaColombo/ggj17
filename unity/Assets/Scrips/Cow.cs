using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow : MonoBehaviour 
{
	enum CowState 
	{
		INACTIVE, ACTIVE, EXPLODING
	}

    private CowPickUp pickUp;
	private CowState state;
	private bool isTouchingTheFloor;

    private void Awake()
    {
        pickUp = GetComponentInChildren<CowPickUp>();
    }

    public void Reset()
	{
		state = CowState.INACTIVE;
		isTouchingTheFloor = false;
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
        var rb = GetComponent<Rigidbody>();
        if (pickUp.isBeingHold)
        {
            state = CowState.ACTIVE;
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
        }
        else
        {
            rb.isKinematic = false;
        }

		if ((state == CowState.INACTIVE || state == CowState.EXPLODING) && isTouchingTheFloor) 
		{
            rb.velocity = Vector3.zero;
		}

		if (transform.localPosition.y < -30) 
		{
			gameObject.SetActive (false);
			GameObjectsPool.Instance ().FreeThisCow (gameObject);
		}
	}
}
