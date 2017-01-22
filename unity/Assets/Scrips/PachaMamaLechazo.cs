using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PachaMamaLechazo : MonoBehaviour 
{
	public bool esElQueHaceRuido = false;

	public void Terminar()
	{
	//	gameObject.SetActive (false);
	}

	public void PlaySFX()
	{
		if(esElQueHaceRuido)
			SoundManager.Instance.PlayEffect (SoundManager.Instance.effectMilkLazer);
	}
}
