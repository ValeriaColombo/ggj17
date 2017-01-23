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
    RED_TEAM,
    BLUE_TEAM
}

public class PlayerId : MonoBehaviour
{
    public PlayerTeam team;
    public bool useKeyboard;
    public PlayerMode playerMode;
    public XboxController controller;
	
	void Awake()
	{
		int queriedNumberOfCtrlrs = XCI.GetNumPluggedCtrlrs();
		
		useKeyboard = (int)controller <= queriedNumberOfCtrlrs ? false : true;
	}
}
