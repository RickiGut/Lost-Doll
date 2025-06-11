using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("----- Audio Source -----")]
    public  AudioSource musicSource;
    public  AudioSource SFXSource;

    [Header("---- Audio Clip")]
    public AudioClip background;
    public AudioClip MenuButton;
    public AudioClip playerRun;
    public AudioClip playerJump;


    // Start is called before the first frame update
    void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }


    public void PlaySFX(AudioClip clip){
        SFXSource.PlayOneShot(clip);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
