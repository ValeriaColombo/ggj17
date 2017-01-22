using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSplash : MonoBehaviour {

	private float timer = 0;

	public void StartAnim ()
	{
		timer = 0;
	}
	
	void Update ()
	{
		timer += Time.fixedDeltaTime;
		if (timer > 0.5f)
		{
			GameObjectsPool.Instance ().FreeThisDropSplash (gameObject);
		}
	}
}
