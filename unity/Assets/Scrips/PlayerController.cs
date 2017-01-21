using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform cowHoldingPlace;

    public float dropCooldownTime = 5f;
    private float dropTime;

    private CowPickUp cow;

    void Update()
    {
        CheckInput();
        UpdateCoolDown();
    }

    void CheckInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ReleaseCow();
        }
    }

    void UpdateCoolDown()
    {
        if (cow == null && dropTime > 0f)
        {
            dropTime -= Time.deltaTime;
        }
    }

    void ReleaseCow()
    {
        if (cow == null) return;

        dropTime = dropCooldownTime;
        cow.DropAt(cowHoldingPlace.position);

        cow = null;
    }

    public void PickCow(CowPickUp cow)
    {
        if (dropTime > 0f) return;

        print("Picked a cow");

        this.cow = cow;
        cow.PlaceAt(cowHoldingPlace.position, transform);
    }
}
