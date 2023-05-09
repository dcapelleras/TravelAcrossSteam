using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class ExteriorToInterior : MonoBehaviour
{
    DialogueRunner runner;
    private void Awake()
    {
        runner = FindObjectOfType<DialogueRunner>();
        runner.AddCommandHandler("goInside", GoInside);
    }

    public void GoInside()
    {
        runner.Dialogue.Stop();
        SceneManager.LoadScene(3);
    }
}
