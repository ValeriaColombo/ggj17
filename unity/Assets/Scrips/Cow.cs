using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cow : MonoBehaviour
{
    enum CowState
    {
        INACTIVE, ACTIVE, EXPLODING
    }

    private CowPickUp pickUp;
    private CowState state;
    private bool isTouchingTheFloor;

    public GameObject sliderBar;
    public Slider slider;
    public GameObject blast;
    private float bombTime = 3f;
    private float countdown;

    private void Awake()
    {
        sliderBar.SetActive(false);
        pickUp = GetComponentInChildren<CowPickUp>();
    }

    private void Start()
    {
        bombTime = Configs.Instance().BombTime;
    }

    public void Reset()
    {
        blast.SetActive(false);
        pickUp.isBeingHold = false;
        StopAllCoroutines();
        blast.SetActive(false);
        state = CowState.INACTIVE;
        isTouchingTheFloor = false;
		GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    void OnCollisionEnter(Collision col)
    {
        if (state == CowState.INACTIVE || state == CowState.EXPLODING)
        {
            if (col.gameObject.name.IndexOf("floor") > -1)
            {
                isTouchingTheFloor = true;
            }
        }
    }

    void StartBombCountdown()
    {
        state = CowState.ACTIVE;
        sliderBar.SetActive(true);
        slider.value = 1f;
        countdown = bombTime;
    }

    void CheckCountdown()
    {
        if (state != CowState.ACTIVE) return;

        if (countdown > 0)
        {
            countdown -= Time.deltaTime;
            UpdateBar((countdown / bombTime));
        }
        else
        {
            BlowUp();
        }
    }

    void UpdateBar(float normVal)
    {
        if (state == CowState.EXPLODING) return;
        slider.value = normVal;
    }

    public void BlowUp()
    {
        if (state == CowState.EXPLODING) return;
        StartCoroutine(Detonate());
    }

    IEnumerator Detonate()
    {
        state = CowState.EXPLODING;
        sliderBar.SetActive(false);
        blast.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        ReturnToPool();
    }

	void Update () 
	{
		var rb = GetComponent<Rigidbody>();
        if (pickUp.isBeingHold)
        {
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;

            if(state == CowState.INACTIVE)
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
