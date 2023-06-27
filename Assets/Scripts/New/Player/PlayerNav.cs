using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Yarn.Unity;

public class PlayerNav : MonoBehaviour //have to set a point to go to if i click a puzzle so it doesnt block vision of it
{
    #region References
    public NavMeshAgent nav;
    Camera cam;
    DialogueRunner dialogueRunner;
    [SerializeField] Animator anim;
    #endregion
    bool canMove;
    bool gameEnded = false;
    [SerializeField] Transform endgameTransform;
    [SerializeField] GameObject staticDoor1;
    [SerializeField] GameObject animatedDoor1;

    [SerializeField] GameObject visualCables;
    [SerializeField] GameObject workingCables;

    [SerializeField] GameObject triggerTeleport;
    [SerializeField] AudioSource secondaryAudio;
    bool walkingSoundOn;
    bool walking;

    private void Awake()
    {
        cam = Camera.main;
        dialogueRunner = FindObjectOfType<DialogueRunner>();
        dialogueRunner.AddCommandHandler<int>("Movement", AllowMovement);
        dialogueRunner.AddCommandHandler<int>("FinishCable", AfterCables);
        dialogueRunner.AddCommandHandler<int>("startPluggingGame", StartPluggingGame);
    }

    private void Update()
    {
        float dist = 3f;
        if (Vector3.Distance(nav.destination, transform.position) < dist && walkingSoundOn)
        {
            walkingSoundOn = false;
            anim.SetFloat("Walk", 0f);
            secondaryAudio.Stop();
        }
        if (GameManager.instance.menuOpen)
        {
            walkingSoundOn = false;
            secondaryAudio.Stop();
        }
        else if (!walkingSoundOn && Vector3.Distance(nav.destination, transform.position) > dist)
        {
            walkingSoundOn = true;
            anim.SetFloat("Walk", 1f);
            secondaryAudio.Play();
        }
        if (!canMove || gameEnded)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.TryGetComponent(out PuzzleDetector puzzle))
                {
                    nav.SetDestination(puzzle.moveToPos.position);
                }
                else
                {
                    nav.SetDestination(hit.point);
                }

            }
        }
    }

    public void MoveToThisDestination(Transform pos)
    {
        nav.SetDestination(pos.position);
        if (Vector3.Distance(nav.destination, transform.position) < 3f)
        {
            anim.SetFloat("Walk", 0f);
        }
        else
        {
            anim.SetFloat("Walk", 1f);
        }
    }

    public void AllowMovement(int allowed)
    {
        if (allowed == 0)
        {
            canMove = false;
            return;
        }
        canMove = true;
    }

    public void AfterCables(int i)
    {
        //AllowMovement(0);
        //gameEnded = true;
        //agent.enabled = false;
        //transform.position = endgameTransform.position;
        CamManager.instance.MoveToCam(2);
        triggerTeleport.SetActive(true);
        //RoomManager.instance.ChangeLoadingScreen(true);
    }

    public void TriggerFinishedBookcasePuzzle()
    {
        //CamManager.instance.MoveToCam(1);
        dialogueRunner.Stop();
        dialogueRunner.StartDialogue("FinishBookcasePuzzle");
        staticDoor1.SetActive(false);
        animatedDoor1.SetActive(true);


        CamManager.instance.MoveToCam(0);
        PlayerPuzzle puzzle = GetComponent<PlayerPuzzle>();
        puzzle.doingPuzzle = false;
        InventoryManager_v2.instance.CloseInventory();
    }

    public void TriggerFinishMachinePuzzle()
    {
        Debug.Log("Finished machine puzzle");
        dialogueRunner.Stop();
        dialogueRunner.StartDialogue("StartPlugging");

        PlayerPuzzle playerPuzzle = GetComponent<PlayerPuzzle>();
        playerPuzzle.machineFinished = true;

        CamManager.instance.MoveToCam(2);
        PlayerPuzzle puzzle = GetComponent<PlayerPuzzle>();
        puzzle.doingPuzzle = false;
        InventoryManager_v2.instance.CloseInventory();
    }

    public void TriggerEndGame(int i)
    {
        dialogueRunner.Dialogue.Stop();
        dialogueRunner.StartDialogue("TerminateGame");
    }

    public void StartPluggingGame(int i) //move cam to cables, change visual cable for working one
    {
        CamManager.instance.MoveToCam(4);
        visualCables.SetActive(false);
        workingCables.SetActive(true);
    }
}