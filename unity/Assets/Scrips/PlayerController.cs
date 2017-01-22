using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public enum PlayerState
{
    IDLE,
    WALKING,
    HOLDING,
    DROP_COOLDOWN,
    HIT,
    STOMPING
}

public class PlayerController : MonoBehaviour
{
    public int playerLives = 5;
    public PlayerId playerId;
    PlayerState state = PlayerState.IDLE;

    PlayerMovement playerMovement;

    // COW DROPPER
    public Transform cowHoldingPlace;

    public float pickUpCooldownTime = 5f;
    private float pickUpCooldown;

    public bool isInPickUpArea;
    private bool isHoldingCow;
    private CowPickUp cow;

    // STOMPER
    public Animator waveAnimator;

    bool hitMax;
    bool hitMin;

    private DummyWaveGenerator waveGenerator;
    public float stompCooldownTime = 2f;
    private float stompCooldown;

    private void Start()
    {
        pickUpCooldownTime = Configs.Instance().PickUpCooldown;
        stompCooldownTime = Configs.Instance().StompCooldown;

        playerId = GetComponent<PlayerId>();
        waveGenerator = FindObjectOfType<DummyWaveGenerator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (playerId.playerMode == PlayerMode.COW_DROPPER)
        {
            UpdateCowDropper();
        }
        else
        {
            UpdateStomper();
        }

		if(transform.localPosition.y < -30)
		{
			PlayerIsDead ();
		}
    }

    public void TakeDamage()
    {
        ToggleHolding(false);
        cow = null;

        playerLives--;
        if(playerLives == 0)
        {
			PlayerIsDead ();
        }
    }

	private void PlayerIsDead()
	{
		gameObject.SetActive (false);
		GameManager.Instance ().GameOver (playerId.team);
	}

    void UpdateStomper()
    {
        CheckStomp();
        UpdateStompCoolDown();
    }

    void CheckStomp()
    {
        if (!IsPressingStomp())
        {
            if (hitMax)
            {
                Stomp();
            }
            hitMin = true;
            hitMax = false;
        }
        else
        {
            hitMax = true;
            hitMin = false;
        }
    }

    void Stomp()
    {
        if (stompCooldown > 0) return;
        stompCooldown = stompCooldownTime;
        waveGenerator.GenerateWave(transform.position);
        waveAnimator.SetTrigger("stomp");
    }

    void UpdateStompCoolDown()
    {
        if (stompCooldown > 0f)
        {
            stompCooldown -= Time.deltaTime;
        }
    }

    void UpdateCowDropper()
    {
        CheckPickCow();
        CheckReleaseCow();
        UpdateDropCoolDown();
    }

    void UpdateDropCoolDown()
    {
        if (!isHoldingCow && pickUpCooldown > 0f)
        {
            pickUpCooldown -= Time.deltaTime;
        }
    }

    void CheckReleaseCow()
    {
        if (!isHoldingCow || isHoldingCow && IsPressingPickUp()) return;

        pickUpCooldown = pickUpCooldownTime;
        if(cow != null) cow.DropAt(cowHoldingPlace.position);
        ToggleHolding(false);
        cow = null;
    }

    void ToggleHolding(bool holding)
    {
        isHoldingCow = holding;
        playerMovement.speed = holding ? Configs.Instance().HoldingCowPlayerSpeed : Configs.Instance().PlayerNormalSpeed;
    }

    public void CanPickCow(CowPickUp cow, bool inPickUpArea)
    {
        isInPickUpArea = inPickUpArea;
        if (isHoldingCow) return;
        this.cow = cow;
    }

    public void CheckPickCow()
    {
        if (isHoldingCow || !isInPickUpArea) return;
        if (!IsPressingPickUp()) return;
        if (pickUpCooldown > 0f) return;

        if (cow == null) return;
        ToggleHolding(true);

        cow.GrabAt(cowHoldingPlace.position, transform);
        cow.GetComponentInParent<Colorize>()
            .ApplyColor(playerId.team == PlayerTeam.RED_TEAM
            ? Configs.Instance().redColor
            : Configs.Instance().blueColor);
    }

    bool IsPressingStomp()
    {
        if(playerId.useKeyboard)
            return Input.GetKey(KeyCode.Space);
        else
            return XCI.GetAxis(XboxAxis.RightTrigger, playerId.controller) > 0.5f;
    }

    bool IsPressingPickUp()
    {
        if (playerId.useKeyboard)
            return Input.GetKey(KeyCode.P);
        else
            return XCI.GetAxis(XboxAxis.RightTrigger, playerId.controller) > 0.5f;
    }
}
