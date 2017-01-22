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
    public AudioSource walkAudioSource;

    private Animator playerAnimator;

    private void Awake()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();

        playerAnimator = GetComponent<Animator>();
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
        UpdateEffects();
        UpdateSpriteFacing();
    }

    void UpdateEffects()
    {
        var addedSpeed = Mathf.Abs(verticalAxis) + Mathf.Abs(horizontalAxis);
        playerAnimator.SetFloat("speed", addedSpeed);

        var holding = speed < Configs.Instance().PlayerNormalSpeed;

        if (addedSpeed > 0.07f || holding) 
        {
            var clip = holding
                ? SoundManager.Instance.effectCharStepHolding
                : SoundManager.Instance.effectCharStep;

            if (walkAudioSource.clip == clip) return;

            walkAudioSource.clip = clip;
            walkAudioSource.loop = true;
            walkAudioSource.Play();
            walkAudioSource.volume = holding ? 0.25f : 0.5f;
            print("called play");
        }
        else
        {
            walkAudioSource.clip = null;
            walkAudioSource.Stop();
        }
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
