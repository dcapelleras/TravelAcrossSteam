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
}
