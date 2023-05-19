using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Yarn.Unity;

public class PlayerNav : MonoBehaviour //have to set a point to go to if i click a puzzle so it doesnt block vision of it
{
    #region References
    public NavMeshAgent agent;
    Camera cam;
    DialogueRunner dialogueRunner;
    [SerializeField] Animator anim;
    #endregion
    bool canMove;
    bool gameEnded = false;
    [SerializeField] Transform endgameTransform;
    [SerializeField] SpriteRenderer _renderer;
    [SerializeField] GameObject staticDoor1;
    [SerializeField] GameObject animatedDoor1;

    [SerializeField] GameObject visualCables;
    [SerializeField] GameObject workingCables;

    [SerializeField] GameObject triggerTeleport;

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
        if (Vector3.Distance(agent.destination, transform.position) < 3f)
        {
            anim.SetFloat("Walk", 0f);
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
                    agent.SetDestination(puzzle.moveToPos.position);
                }
                else
                {
                    agent.SetDestination(hit.point);
                }

                if ((transform.position.x - hit.point.x) < -0.5f)
                {
                    anim.SetFloat("Walk", 1f);
                    _renderer.flipX = true;
                    //anim turn left
                    //anim walk
                }
                else if ((transform.position.x - hit.point.x) > 0.5f)
                {
                    anim.SetFloat("Walk", 1f);
                    _renderer.flipX = false;
                    //anim turn right
                    //anim walk
                }
                else
                {
                    anim.SetFloat("Walk", 0f);
                    //anim idle
                }
            }
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