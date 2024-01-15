using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AkshayDhotre.GraphicSettingsMenu;
using UnityEngine.Audio;

// public class MasterVolumeOption : Option
// {
//         public Slider volumeSlider;
//         public AudioMixer audioMixer;

//     public override void Apply()
//     {
//         float volume = currentSubOption.floatValue;
//         // audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
//         audioMixer.SetFloat("MasterVolume", volume);
//     }
// 
// }

public class MasterVolumeOption : Option
{
    public Slider volumeSlider;
    public AudioMixer audioMixer;

    // Override the Apply method to handle the slider value
    public override void Apply()
    {
        // Ensure the currentSubOption is not null before using it
        if (currentSubOption != null)
        {
            // Assuming floatValue is used to store the slider value
            float volume = volumeSlider.value;

            // Update the floatValue in the currentSubOption
            currentSubOption.floatValue = volume;

            // Use the floatValue to set the master volume in the audio mixer
            audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
        }
        else
        {
            Debug.LogError("Current suboption is null in : " + gameObject.name);
        }
    }
}
