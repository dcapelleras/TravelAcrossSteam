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
    [SerializeField] Slider volumeSlider;
    [SerializeField] AudioSource audioSource;
    [SerializeField] ScriptableSettings settings;

    private void Awake()
    {
        audioSource.volume = settings.volume;
        volumeSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        audioSource.Play();
    }

    public void ValueChangeCheck()
    {
        settings.volume = volumeSlider.value;
        audioSource.volume = settings.volume;
    }

    public void PlayGame(string scenename)
    {
        SceneManager.LoadScene(scenename);
        //scene manager load scene 1
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