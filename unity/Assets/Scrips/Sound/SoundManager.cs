using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public AudioClip musicGamePlay;
	public AudioClip musicMenu;
	public AudioClip musicGameOver;

	public AudioClip effectButtonMenu;
	public AudioClip effectPlayerDeath;

	public AudioClip effectCharStep;
    public AudioClip effectCharStepHolding;
    public AudioClip effectCharStomp;
	public AudioClip effectCharMaleHurt;
	public AudioClip effectCharMaleCowUp;
	public AudioClip effectCharMaleCowDown;
	public AudioClip effectCharMaleDeath;
	public AudioClip effectCharFemaleHurt;
	public AudioClip effectCharFemaleCowUp;
	public AudioClip effectCharFemaleCowDown;
	public AudioClip effectCharFemaleDeath;
	public AudioClip effectSplashName;

	public AudioClip effectDrop;
	public AudioClip effectMilkLazer;

    [Header("Cow")]
    public AudioClip[] cowEffects;
    public AudioClip effectCowSpawn;
    public AudioClip effectCowBlowUp;

    public void PlayRandomCow()
    {
        var random = (int)Random.Range(0, cowEffects.Length);
        PlayEffect(cowEffects[random]);
    }

    public static SoundManager Instance { get { return instance; } }
    private static SoundManager instance;

    public AudioSource effectsSource;
	public AudioSource musicsSource;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

	public void PlayMusic(AudioClip clip, bool loop)
	{
		musicsSource.clip = clip;
		musicsSource.loop = loop;
		musicsSource.Play();
	}

    public void PlayEffect(AudioClip clip)
    {
        effectsSource.PlayOneShot(clip);
    }
}
