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
    public PlayerId playerId;
    PlayerState state = PlayerState.IDLE;

    // COW DROPPER
    public Transform cowHoldingPlace;

    public float dropCooldownTime = 5f;
    private float dropCooldown;

    public bool isInPickUpArea;
    private bool isHoldingCow;
    private CowPickUp cow;

    // STOMPER
    bool hitMax;
    bool hitMin;

    private DummyWaveGenerator waveGenerator;
    public float stompCooldownTime = 2f;
    private float stompCooldown;

    private void Start()
    {
        playerId = GetComponent<PlayerId>();
        waveGenerator = FindObjectOfType<DummyWaveGenerator>();
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
        if (!isHoldingCow && dropCooldown > 0f)
        {
            dropCooldown -= Time.deltaTime;
        }
    }

    void CheckReleaseCow()
    {
        if (!isHoldingCow || isHoldingCow && IsPressingPickUp()) return;

        dropCooldown = dropCooldownTime;
        if(cow != null) cow.DropAt(cowHoldingPlace.position);
        isHoldingCow = false;
        cow = null;
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
        if (dropCooldown > 0f) return;

        if (cow == null) return;
        isHoldingCow = true;
        cow.GrabAt(cowHoldingPlace.position, transform);
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
