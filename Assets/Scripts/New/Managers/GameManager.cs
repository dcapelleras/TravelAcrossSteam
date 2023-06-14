using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject menuPausa;
    public GameObject exit;
    bool menuOpen;
    [SerializeField] AudioSource audioSource;
    public static GameManager instance;
    [SerializeField] ScriptableSettings settings;
    [SerializeField] GameObject tutorialPanel;
    [SerializeField]List<GameObject> tutorials;

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
                menuOpen = false;
            }
            else
            {
                menuPausa.SetActive(true);
                menuOpen = true;
            }
        }
    }

    public void Resume()
    {
        menuPausa.SetActive(false);
        exit.SetActive(false);
    }

    public void Exit()
    {
        exit.SetActive(true);
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ShowTutorial(int index)
    {
        tutorialPanel.SetActive(true);
        tutorials[index].SetActive(true);
        //Time.timeScale = 0f;
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
