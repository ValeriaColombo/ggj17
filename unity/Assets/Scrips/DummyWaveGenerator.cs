using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyWaveGenerator : MonoBehaviour 
{
	public void GenerateWave(Vector3 wavePos)
	{
        wavePos.y = 0f;
		GameObject wave = GameObjectsPool.Instance().GiveMeAStompWave();
		wave.transform.SetParent (transform.parent);
		wave.transform.position = wavePos;
	}
	/*
	private float cooldown = 0;

	void Update()
	{
		cooldown -= Time.fixedDeltaTime;

		if(Input.GetMouseButtonDown(0) && (cooldown < 0))
		{
			cooldown = 2;

			Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
			RaycastHit hit;
			bool collided = Physics.Raycast (ray, out hit);
			if (collided) 
			{
				GenerateWave(new Vector3(hit.point.x, 0, hit.point.z));
			}
		}
	}*/
}
