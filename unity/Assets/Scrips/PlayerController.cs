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

    public Transform cowHoldingPlace;

    public float dropCooldownTime = 5f;
    private float dropTime;

    public bool isInPickUpArea;
    private bool isHoldingCow;
    private CowPickUp cow;

    private void Start()
    {
        playerId = GetComponent<PlayerId>();
    }

    private void Update()
    {
        if(playerId.playerMode == PlayerMode.COW_DROPPER)
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

    }

    void UpdateCowDropper()
    {
        CheckPickCow();
        CheckReleaseCow();
        UpdateCoolDown();
    }

    void UpdateCoolDown()
    {
        if (!isHoldingCow && dropTime > 0f)
        {
            dropTime -= Time.deltaTime;
        }
    }

    void CheckReleaseCow()
    {
        if (!isHoldingCow || isHoldingCow && IsPressingPickUp()) return;

        dropTime = dropCooldownTime;
        cow.DropAt(cowHoldingPlace.position);

        cow = null;
        isHoldingCow = false;
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
        if (dropTime > 0f) return;

        isHoldingCow = true;
        cow.GrabAt(cowHoldingPlace.position, transform);
    }

    bool IsPressingPickUp()
    {
        return XCI.GetAxis(XboxAxis.RightTrigger, playerId.controller) > 0.5f;
    }
}
