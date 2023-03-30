using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject menuPausa;
    public GameObject exit;
    bool menuOpen;

    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
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
        Debug.Log("No puedes huir MWAHAHAHA... porque en unity no funciona el quit =)");
        SceneManager.LoadScene("MainMenu");
    }
}
