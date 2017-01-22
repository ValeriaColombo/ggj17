using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmagedonSign : MonoBehaviour 
{
	public Transform obj1;
	public Transform obj2;
	public Transform obj3;

	public void StartAnim()
	{
		waitForExit = 2;
		gameObject.SetActive (true);
		entering = true;
	}

	private bool entering = false;
	private bool exiting = false;
	private float waitForExit;

	private void Update()
	{
		if (entering)
		{
			Vector3 obj1P = obj1.localPosition;
			Vector3 obj2P = obj2.localPosition;
			Vector3 obj3P = obj3.localPosition;

			obj1P.x = Mathf.Min (0, obj1P.x + 20);
			obj2P.x = Mathf.Max (0, obj2P.x - 20);
			obj3P.x = Mathf.Min (0, obj3P.x + 20);

			obj1.localPosition = obj1P;
			obj2.localPosition = obj2P;
			obj3.localPosition = obj3P;

			if (obj1P.x == 0 && obj2P.x == 0 && obj3P.x == 0) 
			{
				entering = false;
				waitForExit = 2;
			}
		} 
		else if (exiting) 
		{
			Vector3 obj1P = obj1.localPosition;
			Vector3 obj2P = obj2.localPosition;
			Vector3 obj3P = obj3.localPosition;

			obj1P.x = obj1P.x + 20;
			obj2P.x = obj2P.x - 20;
			obj3P.x = obj3P.x + 20;

			obj1.localPosition = obj1P;
			obj2.localPosition = obj2P;
			obj3.localPosition = obj3P;

			if (obj1P.x > 2000) 
			{
				gameObject.SetActive (false);
			}
		}
		else
		{
			waitForExit -= Time.fixedDeltaTime;
			if(waitForExit < 0)
				exiting = true;
		}
	}
}
