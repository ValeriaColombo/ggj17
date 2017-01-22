using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XboxCtrlrInput;

public class Cow : MonoBehaviour
{
    enum CowState
    {
        INACTIVE, ACTIVE, EXPLODING
    }

    private CowPickUp pickUp;
    private CowState state;
    private bool isTouchingTheFloor;

	public SpriteRenderer cowImg;
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
		pushPushCount = 0;
        blast.SetActive(false);
        pickUp.isBeingHold = false;
        StopAllCoroutines();
        blast.SetActive(false);
        state = CowState.INACTIVE;
		cowImg.sprite = Resources.Load<Sprite> ("cow/inactive") as Sprite;
        isTouchingTheFloor = false;
		GetComponent<Rigidbody>().velocity = Vector3.zero;
		sliderBar.SetActive(false);
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
		cowImg.sprite = Resources.Load<Sprite> ("cow/active") as Sprite;
        state = CowState.ACTIVE;
        sliderBar.SetActive(true);
        slider.value = 1f;
        countdown = bombTime;
    }

	private int pushPushCount = 0;
    void CheckCountdown()
    {
        if (state != CowState.ACTIVE) return;

        if (countdown > 0)
        {
			if (pickUp.playerController != null && XCI.GetButtonUp (XboxButton.A, pickUp.playerController.playerId.controller)) 
			{
				pushPushCount++;
			}

            countdown -= Time.deltaTime;

			if(pushPushCount > 8)
			{
				pushPushCount = 0;
				countdown = Mathf.Min(countdown + 1, bombTime);
			}

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
		GameObject wave = GameObjectsPool.Instance ().GiveMeADropWave ();
		wave.transform.SetParent (transform.parent);
		wave.transform.localPosition = new Vector3 (transform.localPosition.x, 0, transform.localPosition.z);
		wave.transform.localScale = new Vector3 (4,4,4);
		wave.GetComponent<DropWave> ().StartAnim ();

        state = CowState.EXPLODING;
        sliderBar.SetActive(false);
        blast.SetActive(true);
		cowImg.sprite = Resources.Load<Sprite> ("cow/invisible") as Sprite;
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
