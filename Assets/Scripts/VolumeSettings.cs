using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider masterSlider;

    
    public void SetMusicVolume() //fungsi untuk mengatur volume musik
    {
       float volume = musicSlider.value;
        myMixer.SetFloat("Music", Mathf.Log10(volume)*20); //
    }public void SetMasterVolume() //fungsi untuk mengatur volume musik
    {
       float volume = masterSlider.value;
        myMixer.SetFloat("Master", Mathf.Log10(volume)*20); //
    }
    
}
