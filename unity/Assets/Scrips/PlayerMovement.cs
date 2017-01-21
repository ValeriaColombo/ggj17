using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
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

    void Update ()
    {
        CheckInput();
        tr.Translate(verticalAxis, 0, horizontalAxis);
    }

    void CheckInput()
    {
        horizontalAxis = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        verticalAxis = -Input.GetAxis("Vertical") * Time.deltaTime * speed;
    }
}
