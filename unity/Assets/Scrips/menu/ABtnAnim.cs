using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ABtnAnim : MonoBehaviour 
{
	private float timer = 0;

	private Image img;

	void Update () 
	{
		if (img == null)
			img = GetComponent<Image> ();
		
		timer += Time.fixedDeltaTime;
		img.enabled = Mathf.Round(timer) % 2 == 0;
	}
}
