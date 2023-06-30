using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource playerAudio;

    [SerializeField] Slider volumeSlider;

    [SerializeField] ScriptableSettings settings;

    private void Start()
    {
        volumeSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    public void ValueChangeCheck()
    {
        settings.volume = volumeSlider.value;
        audioSource.volume = settings.volume;
        if (playerAudio != null)
        {
            playerAudio.volume = settings.volume;
        }
    }
}
