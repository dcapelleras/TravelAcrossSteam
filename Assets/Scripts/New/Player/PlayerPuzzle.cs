using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.AI;

public class PlayerPuzzle : MonoBehaviour
{
    int previousCamUsed;
    public bool doingPuzzle;
    DialogueRunner dialogueRunner;
    bool firstPuzzleRead;
    NavMeshAgent agent;
    PlayerNav nav;
    public bool machineFinished;

    private void Awake()
    {
        dialogueRunner = FindObjectOfType<DialogueRunner>();
        agent = GetComponent<NavMeshAgent>();
        nav = FindObjectOfType<PlayerNav>();
    }

    private void Update()
    {
        if (!doingPuzzle)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.TryGetComponent(out PuzzleDetector puzzle))
                    {
                        if (machineFinished)
                        {
                            if (puzzle.puzzleIndex == 3)
                            {
                                puzzle.puzzleIndex = 4;
                            }
                        }
                        if (!firstPuzzleRead && (puzzle.puzzleIndex == 1 || puzzle.puzzleIndex == 3))
                        {
                            firstPuzzleRead = true;
                            doingPuzzle = true;

                            CamManager.instance.MoveToCam(puzzle.puzzleIndex);
                            previousCamUsed = puzzle.puzzleIndex - 1; //puzzle will always have to have the next camera from the room
                            dialogueRunner.Dialogue.Stop();
                            dialogueRunner.StartDialogue("FirstPuzzle");
                            agent.SetDestination(puzzle.moveToPos.position);
                            //InventoryManager_v2.instance.OpenInventory();
                        }
                        else if (firstPuzzleRead && (puzzle.puzzleIndex == 1 || puzzle.puzzleIndex == 3))
                        {
                            doingPuzzle = true;
                            CamManager.instance.MoveToCam(puzzle.puzzleIndex);
                            previousCamUsed = puzzle.puzzleIndex - 1; //puzzle will always have to have the next camera from the room
                            dialogueRunner.Dialogue.Stop();
                            dialogueRunner.StartDialogue("FirstPuzzleReminder");
                            agent.SetDestination(puzzle.moveToPos.position);
                            //InventoryManager_v2.instance.OpenInventory();
                        }
                    }

                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                doingPuzzle = false;
                CamManager.instance.MoveToCam(previousCamUsed);
                dialogueRunner.Dialogue.Stop();
                nav.AllowMovement(1);
                InventoryManager_v2.instance.CloseInventory();
            }
        }
    }
}