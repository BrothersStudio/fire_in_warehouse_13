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

    public Image player_hp;
	public Image house_hp;

    private float stringsAmount = 0.75f;
    private float fadeInTime = 0.3f;
    private float spookyEffectAmount = 0.60f;

    private bool hasPlayed = false;

    // Use this for initialization
    void Start ()
    {
        
    }
	
	void FixedUpdate ()
    {
        //when HP hits designated amt, switch to new snapshot
		if (player_hp.fillAmount < stringsAmount || house_hp.fillAmount < stringsAmount)
        {
            stringsIn.TransitionTo(fadeInTime);
        }
        //when HP reaches certain threshold, and the noise hasn't played before, play the spooky noise
        //and make sure it doesn't play again
		if ((player_hp.fillAmount < stringsAmount || house_hp.fillAmount < stringsAmount) && !hasPlayed)
        {
            effectsSource.clip = spookyNoise;
            effectsSource.Play();
            hasPlayed = true;
        } 		
	}

}
