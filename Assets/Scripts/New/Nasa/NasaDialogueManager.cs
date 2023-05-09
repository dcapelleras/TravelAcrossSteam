using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class NasaDialogueManager : MonoBehaviour
{
    public static NasaDialogueManager instance;
    DialogueRunner runner;
    LineView lineView;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        runner = FindObjectOfType<DialogueRunner>();
        lineView = FindObjectOfType<LineView>();
        runner.AddCommandHandler("progPuzzle", ProgrammingPuzzle);
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

    public void StartProgrammingPuzzle()
    {
        runner.Dialogue.Stop();
        runner.StartDialogue("PCPuzzle");
    }

    public void ProgrammingPuzzle()
    {
        SceneManager.LoadSceneAsync("CodingMinigame", LoadSceneMode.Additive);
    }

    public void FinishCodingPuzzle()
    {
        runner.Dialogue.Stop();
        runner.StartDialogue("AfterPCPuzzle");
    }
}
