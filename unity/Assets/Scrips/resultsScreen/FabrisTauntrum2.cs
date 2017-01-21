using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FabrisTauntrum2 : MonoBehaviour 
{
	public FabrisTauntrum oneOfTheLines;

	public int enterSpeed = 30;
	private bool done = false;

	public void Setup(bool team)
	{
		GetComponent<Image> ().sprite = Resources.Load<Sprite> ("gameOver/" + (team ? "blue_wins" : "red_wins")) as Sprite;
	}

	void Update () 
	{
		if (oneOfTheLines.enterFinished && !done)
		{
			Vector3 p = transform.localPosition;
			p.x += enterSpeed;

			if (p.x >= 0)
			{
				p.x = 0;
				done = true;
			}

			transform.localPosition = p;
		}
	}
}
