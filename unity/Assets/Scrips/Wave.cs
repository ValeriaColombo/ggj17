using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour 
{
	private float timeToLive;

	void Start()
	{
		timeToLive = 0;
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.name.IndexOf ("vaca") > -1)
		{
			GameObject bomb = col.gameObject;

			Vector3 waveCenter = transform.localPosition;
			Vector3 bombCenter = bomb.transform.localPosition;

			waveCenter.y = 0;
			bombCenter.y = 0;

			Vector3 direction = new Vector3(bombCenter.x - waveCenter.x, 0, bombCenter.z - waveCenter.z);
			float distance = Vector3.Distance(bombCenter, waveCenter);
			float extraImpulse = Mathf.Max (0.1f, (4 - distance));
			Vector3 force = new Vector3(direction.x * extraImpulse, extraImpulse, direction.z * extraImpulse);

			//Debug.Log ("waveCenter: "+ waveCenter + " / bombCenter: "+bombCenter + " / distance: " +direction +" / magnitude: " + distance + " / force: " + force);

			Rigidbody rb = bomb.GetComponent<Rigidbody> ();
			rb.velocity = Vector3.zero;
			rb.AddForce (force, ForceMode.Impulse);
		}
	}

	void Update()
	{
		timeToLive += Time.fixedDeltaTime;
		if (timeToLive > 2)
		{
			Destroy (gameObject);
		}
	}
}
