using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class PlayerMovement : MonoBehaviour
{
    public PlayerId playerId;

    public float speed;

    private Rigidbody rb;
    private Transform tr;
        
    private float horizontalAxis;
    private float verticalAxis;

    public SpriteRenderer spriteRenderer;

    private void Awake()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        speed = Configs.Instance().PlayerNormalSpeed;
        playerId = GetComponent<PlayerId>();
    }

    void Update ()
    {
        if (playerId.useKeyboard)
        {
            CheckInputKeyboard();
        }
        else
        {
            CheckInput();
        }
        tr.Translate(verticalAxis, 0, horizontalAxis);
        UpdateSpriteFacing();
    }

    void UpdateSpriteFacing()
    {
        if (horizontalAxis > -0.01f && horizontalAxis < 0.01f) return;
        spriteRenderer.flipX = (horizontalAxis < 0) ? true : false;
    }

    void CheckInput()
    {
        horizontalAxis = XCI.GetAxis(XboxAxis.LeftStickX, playerId.controller) * Time.deltaTime * speed;
        verticalAxis = -XCI.GetAxis(XboxAxis.LeftStickY, playerId.controller) * Time.deltaTime * speed;
    }

    void CheckInputKeyboard()
    {
        switch (playerId.controller)
        {
            case XboxController.First:
                horizontalAxis = Input.GetAxis("Horizontal1") * Time.deltaTime * speed;
                verticalAxis = -Input.GetAxis("Vertical1") * Time.deltaTime * speed;
                break;
            case XboxController.Second:
                horizontalAxis = Input.GetAxis("Horizontal2") * Time.deltaTime * speed;
                verticalAxis = -Input.GetAxis("Vertical2") * Time.deltaTime * speed;
                break;
        }
    }
}
