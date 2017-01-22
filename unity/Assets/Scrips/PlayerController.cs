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
    public int playerLives = 1;
    public PlayerId playerId;
    PlayerState state = PlayerState.IDLE;

    PlayerMovement playerMovement;
    Animator playerAnimator;

    // COW DROPPER
    public Transform cowHoldingPlace;

    private float pickUpCooldown;

    public bool isInPickUpArea;
    private bool isHoldingCow;
    private CowPickUp cow;

    // STOMPER
    bool hitMax;
    bool hitMin;

    private DummyWaveGenerator waveGenerator;
    private float stompCooldown;

    private void Start()
    {
		//transform.localPosition = new Vector3 (Random.Range (-6f, 6f), 1, Random.Range (-10f, 10f));

		playerLives = Configs.Instance ().PlayerLives;

        playerId = GetComponent<PlayerId>();
        waveGenerator = FindObjectOfType<DummyWaveGenerator>();
        playerMovement = GetComponent<PlayerMovement>();

        playerAnimator = GetComponent<Animator>();
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
		if (GameManager.Instance ().GameOverAlready)
			return;
		
        ToggleHolding(false);
        cow = null;

        playerLives--;
        if(playerLives == 0)
        {
            playerAnimator.SetTrigger("death");
            Invoke("PlayerIsDead", 1f);
        }
        else
        {
            playerAnimator.SetTrigger("hit");
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

    public void PushBombs()
    {
        // llamado desde la animación
        waveGenerator.GenerateWave(transform.position);
    }

    void Stomp()
    {
        if (stompCooldown > 0) return;
		stompCooldown = Configs.Instance().StompCooldown;

        playerMovement.enabled = false;
        playerAnimator.SetTrigger("stomp");
    }

    void UpdateStompCoolDown()
    {
        if (stompCooldown > 0f)
        {
            stompCooldown -= Time.deltaTime;
        }
        else
        {
            if (!playerMovement.enabled) playerMovement.enabled = true;
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

		pickUpCooldown = Configs.Instance().PickUpCooldown;
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
            ? Configs.Instance().ReadTeamColor
            : Configs.Instance().BlueTeamColor);
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
			return XCI.GetAxis(XboxAxis.RightTrigger, playerId.controller) > 0.5f && XCI.GetAxis(XboxAxis.LeftTrigger, playerId.controller) > 0.5f;
    }
}
