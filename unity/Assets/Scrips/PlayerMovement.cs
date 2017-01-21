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
        CheckInput();
        tr.Translate(verticalAxis, 0, horizontalAxis);
    }

    void CheckInput()
    {
        horizontalAxis = XCI.GetAxis(XboxAxis.LeftStickX, playerId.controller) * Time.deltaTime * speed;
        verticalAxis = -XCI.GetAxis(XboxAxis.LeftStickY, playerId.controller) * Time.deltaTime * speed;
    }

    void CheckInputKeyboard()
    {
        horizontalAxis = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        verticalAxis = -Input.GetAxis("Vertical") * Time.deltaTime * speed;
    }
}
