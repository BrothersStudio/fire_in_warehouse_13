using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MusicController : MonoBehaviour
{
    public AudioSource effectsSource;
    public AudioMixerSnapshot start;
    public AudioMixerSnapshot stringsIn;
    public AudioClip spookyNoise;



    public Image hp_bars_0;
    private float stringsAmount = 0.75f;
    private float fadeInTime = 0.3f;
    private float spookyEffectAmount = 0.60f;

    private bool hasPlayed = false;
    // Use this for initialization
    void Start ()
    {
        effectsSource.clip = spookyNoise;
    }
	
	void FixedUpdate ()
    {
        if (hp_bars_0.fillAmount < stringsAmount)
        {
            stringsIn.TransitionTo(fadeInTime);
        }
        
        if (hp_bars_0.fillAmount < spookyEffectAmount && !hasPlayed)
        {
            effectsSource.PlayOneShot(spookyNoise);
            hasPlayed = true;
        } 		
	}

}
