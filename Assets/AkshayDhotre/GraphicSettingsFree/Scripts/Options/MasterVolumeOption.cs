using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AkshayDhotre.GraphicSettingsMenu;
using UnityEngine.Audio;

public class MasterVolumeOption : Option
{
    public Slider volumeSlider;
    public AudioMixer audioMixer;

    public override void Apply()
    {
        float volume = currentSubOption.floatValue;
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
    }
}
