using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

[System.Serializable]
public enum PlayerMode
{
    COW_DROPPER,
    STOMPER
}

public class PlayerId : MonoBehaviour
{
    public PlayerMode playerMode;
    public XboxController controller;
}
