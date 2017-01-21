using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FabrisTauntrum : MonoBehaviour 
{
	List<GameObject> parts;

	public int enterSpeed = 30;
	public int loopSpeed = 1;

	public bool enterFinished = false;

	void Start ()
	{
		Init ();
	}

	private void Init()
	{
		if (parts != null)
			return;
		
		int pos = 1366;
		parts = new List<GameObject> ();
		foreach (Image part in transform.GetComponentsInChildren<Image>()) 
		{
			part.transform.localPosition = new Vector3 (pos,0,0);
			parts.Add (part.gameObject);

			pos += 135;
		}
	}

	public void Setup(bool team)
	{
		Init ();

		enterFinished = false;
		foreach (GameObject part in parts)
		{
			part.GetComponent<Image>().sprite = Resources.Load<Sprite> ("gameOver/" + (team ? "guarda_win_blue" : "guarda_win_red")) as Sprite;
		}
	}
	
	void Update () 
	{
		foreach (GameObject part in parts)
		{
			Vector3 p = part.transform.localPosition;
			p.x -= enterFinished ? loopSpeed : enterSpeed;

			if (p.x <= -135)
			{
				enterFinished = true;
				p.x = -135;
				p.x += 12 * 135;
			}

			part.transform.localPosition = p;
		}
	}
}
