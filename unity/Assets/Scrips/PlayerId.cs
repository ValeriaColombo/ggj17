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

[System.Serializable]
public enum PlayerTeam
{
    TEAM_1,
    TEAM_2
}

public class PlayerId : MonoBehaviour
{
    public PlayerTeam team;
    public bool useKeyboard;
    public PlayerMode playerMode;
    public XboxController controller;
}
