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

    public GameObject blast;
    private float bombTime = 3f;
    private float countdown;

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

    void StartBombCountdown()
    {
        state = CowState.ACTIVE;
        countdown = bombTime;
    }

    void CheckCountdown()
    {
        if (state != CowState.ACTIVE) return;

        if(countdown > 0)
        {
            countdown -= Time.deltaTime;
        }
        else
        {
            Detonate();
        }
    }

    void Detonate()
    {
        state = CowState.EXPLODING;
        blast.SetActive(true);
        Invoke("Kill", 0.5f);
    }

    void Kill()
    {
        blast.SetActive(false);
        ReturnToPool();
    }

	void Update () 
	{
        var rb = GetComponent<Rigidbody>();
        if (pickUp.isBeingHold)
        {
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;

            if(state != CowState.ACTIVE)
            {
                StartBombCountdown();
            }
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
            ReturnToPool();
		}

        CheckCountdown();
	}

    void ReturnToPool()
    {
        gameObject.SetActive(false);
        GameObjectsPool.Instance().FreeThisCow(gameObject);
    }
}
