using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject menuPausa;
    public GameObject menuSettings;
    bool menuOpen;
    [SerializeField] AudioSource audioSource;
    public static GameManager instance;
    [SerializeField] ScriptableSettings settings;
    [SerializeField] GameObject tutorialPanel;
    [SerializeField]List<GameObject> tutorials;
    public int currentTutorialIndex;
    [SerializeField] GameObject tutorialReminderButton;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = settings.volume;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuOpen)
            {
                Resume();

            }
            else
            {
                PauseGame();

            }
        }
    }

    public void Resume()
    {
        menuOpen = false;
        menuPausa.SetActive(false);
        menuSettings.SetActive(false);
        Time.timeScale= 1f;
    }

    public void PauseGame()
    {
        menuOpen = true;
        menuPausa.SetActive(true);
        Time.timeScale= 0f;
    }

    public void OpenSettings()
    {
        menuPausa.SetActive(false);
        menuSettings.SetActive(true);
    }

    public void CloseSettings()
    {
        menuPausa.SetActive(true);
        menuSettings.SetActive(false);
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ShowLastTutorial()
    {
        ShowTutorial(currentTutorialIndex);
    }

    public void ShowTutorial(int index)
    {
        tutorialPanel.SetActive(true);
        tutorials[index].SetActive(true);
        //Time.timeScale = 0f;
        currentTutorialIndex= index;
        tutorialReminderButton.SetActive(true);
    }

    public void HideTutorial()
    {
        foreach (GameObject t in tutorials)
        {
            t.SetActive(false);
        }
        tutorialPanel.SetActive(false);
        //Time.timeScale = 1f;
    }
}
