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
    [SerializeField] NasaDoor doorToWarehouse;
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
        runner.AddCommandHandler<int>("OpenTutorial", ShowThisTutorial);
        runner.AddCommandHandler("lastScene", GoToLastScene);
        runner.AddCommandHandler("openWarehouse", UnlockWarehouseDoor);
    }

    public void ShowThisTutorial(int i)
    {
        GameManager.instance.ShowTutorial(i);
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

    private void Update() //for testing purposes only
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            FinishCodingPuzzle();
        }
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
        doorToCommand.UnlockDoor();
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

    public void OpenWarehouse()
    {
        runner.Dialogue.Stop();
        runner.StartDialogue("WarehouseOpened");
    }

    public void UnlockWarehouseDoor()
    {
        doorToWarehouse.UnlockDoor();
    }

    public void StartSlidingPuzzle()
    {
        runner.Dialogue.Stop();
        runner.StartDialogue("StartSlidingPuzzleDialogue");
        CamManager.instance.MoveToCam(4);
    }

    public void FinishSlidingPuzzle()
    {
        runner.Dialogue.Stop();
        runner.StartDialogue("FinishSlidingPuzzleDialogue");
        CamManager.instance.MoveToCam(3);
    }

    public void GotDocumentsToMargaret()
    {
        runner.Dialogue.Stop();
        runner.StartDialogue("MargaretAfterDocuments");
    }

    public void GoToLastScene()
    {
        SceneManager.LoadScene(9); //will be the 9
    }
}
