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
    [SerializeField] GameObject decorationPeople;
    [SerializeField] GameObject MargaretH;
    [SerializeField] GameObject bossKeys;
    [SerializeField] NasaDoor doorToCommand;
    public int roomIndexToGoBack;

    [SerializeField] List<Transform> placesToSend;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        runner = FindObjectOfType<DialogueRunner>();
        lineView = FindObjectOfType<LineView>();
        runner.AddCommandHandler("progPuzzle", ProgrammingPuzzle);
        runner.AddCommandHandler("margaret", MargaretAppears);
        runner.AddCommandHandler("sendPlayerGuard", SendPlayerBack);
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
        decorationPeople.SetActive(true); //activate decoration people
    }

    public void MargaretAppears()
    {
        MargaretH.SetActive(true);
        bossKeys.SetActive(true);
    }

    public void GotKeysAndTalkedToMargaret()
    {
        runner.Dialogue.Stop();
        runner.StartDialogue("MargaretAfterBossKeys");
        doorToCommand.isLocked = false;
    }

    public void CatchedDialogue()
    {
        if (!runner.Dialogue.IsActive)
        {
            runner.StartDialogue("PlayerCatched");
        }
    }

    public void SendPlayerBack()
    {
        FindObjectOfType<NasaNavigation>().MoveToThisDestination(placesToSend[roomIndexToGoBack]);
    }
}
