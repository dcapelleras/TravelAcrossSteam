using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
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
    [SerializeField] Curious margaretCurious;
    public int roomIndexToGoBack;

    [SerializeField] List<Transform> placesToSend;

    [SerializeField] List<Guard> guards = new List<Guard>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        runner = FindObjectOfType<DialogueRunner>();
        lineView = FindObjectOfType<LineView>();
        runner.AddCommandHandler("progPuzzle", ProgrammingPuzzle);
        runner.AddCommandHandler("sendPlayerGuard", SendPlayerBack);
        runner.AddCommandHandler<int>("OpenTutorial", ShowThisTutorial);
        runner.AddCommandHandler("lastScene", GoToLastScene);
        runner.AddCommandHandler("openWarehouse", UnlockWarehouseDoor);
        runner.AddCommandHandler("guardsFriendly", GuardsFriendly);
        runner.AddCommandHandler("guardsUnfriendly", GuardsUnfriendly);
        runner.AddCommandHandler("janitorDoor", JanitorCanOpenDoor);
        runner.AddCommandHandler("MCurious1", MargaretCurious1);
        runner.AddCommandHandler("MCurious2", MargaretCurious2);
        runner.AddCommandHandler("MCuriousNull", MargaretCuriousNull);
    }

    public void CuriousTalk(int index)
    {
        switch(index)
        {
            case 0:
                runner.Dialogue.Stop();
                runner.StartDialogue("CuriousJanitor1");
                break;
            case 1:
                runner.Dialogue.Stop();
                runner.StartDialogue("CuriousJanitor2");
                break;
            case 2:
                runner.Dialogue.Stop();
                runner.StartDialogue("CuriousMargaret1");
                break;
            case 3:
                runner.Dialogue.Stop();
                runner.StartDialogue("CuriousMargaret2");
                break;
            case 4:
                runner.Dialogue.Stop();
                runner.StartDialogue("CuriousRecepcionist1");
                break;
            case 5:
                runner.Dialogue.Stop();
                runner.StartDialogue("CuriousRecepcionist2");
                break;
            case 6:
                runner.Dialogue.Stop();
                runner.StartDialogue("CuriousRecepcionist3");
                break;
            case 7:
                runner.Dialogue.Stop();
                runner.StartDialogue("CuriousRecepcionist4");
                break;
            case 8:
                runner.Dialogue.Stop();
                runner.StartDialogue("CuriousRecepcionist5");
                break;
            case 9:
                runner.Dialogue.Stop();
                runner.StartDialogue("CuriousRecepcionist6");
                break;
        }
    }

    public void MargaretCurious1()
    {
        margaretCurious.curiousIndex = 2;
    }
    public void MargaretCurious2()
    {
        margaretCurious.curiousIndex = 3;
    }
    public void MargaretCuriousNull()
    {
        margaretCurious.curiousIndex = -1;
    }

    public void GuardsFriendly()
    {
        foreach (Guard g in guards)
        {
            g.friendly = true;
        }
    }

    public void GuardsUnfriendly()
    {
        foreach (Guard g in guards)
        {
            g.friendly = false;
        }
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

    public void JanitorCanOpenDoor()
    {
        FindObjectOfType<Janitor>().UnlockDoor();
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
        MargaretAppears();
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
        NasaNavigation playerNav = FindObjectOfType<NasaNavigation>();
        int index = playerNav.placeToGoBackWhenCaught;
        Vector3 placeToGo = placesToSend[index].position;
        playerNav.transform.position = placeToGo;
        NavMeshAgent playerAgent = playerNav.GetComponent<NavMeshAgent>();
        playerAgent.ResetPath();
        Vector3 offset = placeToGo;
        offset.z += 1f;
        playerAgent.SetDestination(offset);
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
