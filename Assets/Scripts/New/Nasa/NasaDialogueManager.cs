using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class NasaDialogueManager : MonoBehaviour
{
    public static NasaDialogueManager instance;
    DialogueRunner runner;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        runner = FindObjectOfType<DialogueRunner>();
    }

    public void JanitorOpenedOffices()
    {
        runner.Dialogue.Stop();
        runner.StartDialogue("JanitorOpenedDoor");
    }

    public void EnterOfficesFirstTime()
    {
        runner.Dialogue.Stop();
        runner.StartDialogue("InOffice");
    }

    
}
