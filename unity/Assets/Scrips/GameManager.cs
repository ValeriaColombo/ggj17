using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
	private float timeLeft;

	public Image min0;
	public Image min1;
	public Image sec0;
	public Image sec1;

	public Lechegedon ArmagedonTrigger;

	private void Start ()
	{
		timeLeft = Configs.Instance().GameTime;
		StartCoroutine (UpdateTimer());
	}
	
	private void Update ()
	{
		
	}

	private IEnumerator UpdateTimer()
	{
		int mins = Mathf.FloorToInt (timeLeft/60);
		int secs = (int) (timeLeft % 60);

		string min0str = mins.ToString("00")[0].ToString();
		string min1str = mins.ToString("00")[1].ToString();
		string sec0str = secs.ToString("00")[0].ToString();
		string sec1str = secs.ToString("00")[1].ToString();

		min0.sprite = Instantiate (Resources.Load<Sprite> ("counter/timer_"+min0str) as Sprite);
		min1.sprite = Instantiate (Resources.Load<Sprite> ("counter/timer_"+min1str) as Sprite);
		sec0.sprite = Instantiate (Resources.Load<Sprite> ("counter/timer_"+sec0str) as Sprite);
		sec1.sprite = Instantiate (Resources.Load<Sprite> ("counter/timer_"+sec1str) as Sprite);

		yield return new WaitForSeconds (1);

		timeLeft--;

		if (timeLeft < 0) 
		{
			//Se termino;
		}
		else if(timeLeft <= Configs.Instance().TimeToPachaDoom)
		{
			ArmagedonTrigger.StartLechegedon ();
			StartCoroutine (UpdateTimer());
		}
		else 
		{
			StartCoroutine (UpdateTimer());
		}
	}
}
