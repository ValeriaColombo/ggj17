using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : MonoBehaviour {

	public MeshRenderer drop;
	public ParticleSystem explosion;

	private bool exploding;
	private float time_explosion;

	public void Reset()
	{
		drop.enabled = true;
		explosion.gameObject.SetActive (false);
	}

	void OnCollisionEnter(Collision col)
	{
		bool anim_explotion = false;
		if (col.gameObject.name.IndexOf ("floor") > -1)
		{
			anim_explotion = true;
		}
		else if (col.collider.CompareTag("Player"))
		{
			anim_explotion = true;
			col.gameObject.GetComponent<PlayerController> ().TakeDamage ();
		}
		else if(col.collider.CompareTag("Cow"))
		{
			anim_explotion = true;
			col.gameObject.GetComponent<Cow> ().BlowUp ();
		}

		if (anim_explotion)
		{
			drop.enabled = false;
			explosion.gameObject.SetActive (true);
			exploding = true;
			time_explosion = 1;

			GameObject wave = GameObjectsPool.Instance ().GiveMeADropWave ();
			wave.transform.SetParent (transform.parent);
			wave.transform.localPosition = new Vector3 (transform.localPosition.x, 0, transform.localPosition.z);
			wave.transform.localScale = new Vector3 (2,2,2);
			wave.GetComponent<DropWave> ().StartAnim ();

			GameObject splash = GameObjectsPool.Instance ().GiveMeADropSplash ();
			splash.transform.SetParent (transform.parent);
			splash.transform.localPosition = new Vector3 (transform.localPosition.x, 1, transform.localPosition.z);
			splash.GetComponent<DropSplash> ().StartAnim ();
		}
	}

	void Update()
	{
		if (exploding) 
		{
			time_explosion -= Time.fixedDeltaTime;
			if (time_explosion < 0) 
			{
				exploding = false;
				gameObject.SetActive (false);

				GameObjectsPool.Instance ().FreeThisMeteorite (gameObject);
			}
		}
	}
}
