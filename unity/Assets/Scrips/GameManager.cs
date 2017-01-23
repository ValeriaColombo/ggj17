using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
	public bool GameOverAlready = false;

	private static GameManager _instance;
	public static GameManager Instance()
	{
		return _instance;
	}

	private void Awake ()
	{
		_instance = this;
	}

	private float timeLeft;

	public GameOver gameOver;

	public Image min0;
	public Image min1;
	public Image sec0;
	public Image sec1;

	public Lechegedon ArmagedonTrigger;

	private void Start ()
	{
		SoundManager.Instance.PlayMusic (SoundManager.Instance.musicGamePlay, false);
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
		else if(timeLeft <= Configs.Instance().TimeToPachaDoom*2 && timeLeft > Configs.Instance().TimeToPachaDoom)
		{
			ArmagedonTrigger.StartThePachamaming (Configs.Instance().TimeToPachaDoom);
			StartCoroutine (UpdateTimer());
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

	public void GameOver(PlayerTeam team)
	{
		if (GameOverAlready)
			return;
		
		GameOverAlready = true;
//		SoundManager.Instance.PlayEffect (SoundManager.Instance.effectPlayerDeath);
		min0.transform.parent.gameObject.SetActive (false);

		StartCoroutine (ShowGameOverScreenInAWhile (team));
	}

	private IEnumerator ShowGameOverScreenInAWhile(PlayerTeam team)
	{
		yield return new WaitForSeconds (2);
		SoundManager.Instance.PlayMusic (SoundManager.Instance.musicGameOver, false);
		gameOver.ShowGameOver (team == PlayerTeam.RED_TEAM);
	}
}
