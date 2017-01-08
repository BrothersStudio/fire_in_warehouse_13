using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MusicController : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioMixerSnapshot start;
    public AudioMixerSnapshot stringsIn;



    public Image hp_bars_0;
    public float stringsAmount = 0.75f;
    private float fadeInTime = 1.0f;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	void FixedUpdate ()
    {
        if (hp_bars_0.fillAmount < stringsAmount)
        {
            stringsIn.TransitionTo(fadeInTime);
        } 		
	}

}
