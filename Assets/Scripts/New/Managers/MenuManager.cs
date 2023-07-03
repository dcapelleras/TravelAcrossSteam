using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject instructionsPanel;
    [SerializeField] GameObject settingsPanel;
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject levelsPanel;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] AudioSource audioSource;
    [SerializeField] ScriptableSettings settings;

    private void Awake()
    {
        musicSlider.onValueChanged.AddListener(delegate { MusicValueChangeCheck(); });
        musicSlider.onValueChanged.AddListener(delegate { SFXValueChangeCheck(); });

        musicSlider.value = settings.volume;
        sfxSlider.value = settings.sfxVolume;

        audioSource.Play();
    }

    public void MusicValueChangeCheck()
    {
        settings.volume = musicSlider.value;
        audioSource.volume = settings.volume;
    }

    public void SFXValueChangeCheck()
    {
        settings.sfxVolume = sfxSlider.value;
    }

    public void PlayGame(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void SelectLevel()
    {
        levelsPanel.SetActive(true);
    }

    public void ShowInstructions()
    {
        //deactivate all panels and activate instructions
        settingsPanel.SetActive(false);
        menuPanel.SetActive(false);
        instructionsPanel.SetActive(true);
        levelsPanel.SetActive(false);
    }

    public void ShowSettings()
    {
        //load all current settings
        //deactivate all panels and activate settings
        settingsPanel.SetActive(true);
        menuPanel.SetActive(false);
        instructionsPanel.SetActive(false);
        levelsPanel.SetActive(false);
    }

    public void BackToMain()
    {
        //deactivate all panels and activate main
        settingsPanel.SetActive(false);
        menuPanel.SetActive(true);
        instructionsPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}