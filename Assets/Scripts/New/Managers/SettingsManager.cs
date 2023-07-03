using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource playerAudio;

    [SerializeField] Slider volumeSlider;
    [SerializeField] Slider sfxVolumeSlider;

    [SerializeField] ScriptableSettings settings;

    private void Start()
    {
        volumeSlider.onValueChanged.AddListener(delegate { MusicValueChangeCheck(); });
        sfxVolumeSlider.onValueChanged.AddListener(delegate { SFXValueChangeCheck(); });

        volumeSlider.value = settings.volume;
        sfxVolumeSlider.value = settings.sfxVolume;
    }

    public void MusicValueChangeCheck()
    {
        settings.volume = volumeSlider.value;
        audioSource.volume = settings.volume;
    }

    public void SFXValueChangeCheck()
    {
        settings.sfxVolume = sfxVolumeSlider.value;
        if (playerAudio != null)
        {
            playerAudio.volume = settings.sfxVolume;
        }
    }
}
