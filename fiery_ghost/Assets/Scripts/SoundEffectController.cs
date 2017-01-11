using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundEffectController : MonoBehaviour
{

    public AudioClip[] quenched;
    public AudioSource quenchSource;


    //plays the sound for water hitting the fire
    public void PlayQuench()
    {
        int randClip = Random.Range(0,quenched.Length);
        quenchSource.clip = quenched[randClip];
        quenchSource.Play();
        print("quench");
    }
}
