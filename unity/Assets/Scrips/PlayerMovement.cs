using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class PlayerMovement : MonoBehaviour
{
    public PlayerId playerId;

    public float speed = 20f;

    private Rigidbody rb;
    private Transform tr;
        
    private float horizontalAxis;
    private float verticalAxis;

    private void Awake()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
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
