using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSetting : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SfxSlider;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("musicVolume")){
            LoadVolume();
        }else{
            SetMusicVolume();
            SetSfxVolume();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMusicVolume(){
        float volume = musicSlider.value;
        myMixer.SetFloat("music",Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("musicVolume",volume);
    }

    public void SetSfxVolume(){
        float volume = SfxSlider.value;
        myMixer.SetFloat("Sfx",Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("SfxVolume",volume);
    }


    private void LoadVolume(){
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        SfxSlider.value = PlayerPrefs.GetFloat("SfxVolume");
        SetSfxVolume();
        SetMusicVolume();
    }

}
