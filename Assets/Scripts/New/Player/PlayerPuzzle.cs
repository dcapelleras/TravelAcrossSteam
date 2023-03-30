using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Yarn.Unity;

public class PlayerPuzzle : MonoBehaviour
{
    int previousCamUsed;
    bool doingPuzzle;
    DialogueRunner dialogueRunner;
    bool firstPuzzleRead;

    private void Awake()
    {
        dialogueRunner = FindObjectOfType<DialogueRunner>();
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
                        if (!firstPuzzleRead)
                        {
                            firstPuzzleRead = true;
                            doingPuzzle = true;
                            CamManager.instance.MoveToCam(puzzle.puzzleIndex);
                            previousCamUsed = puzzle.puzzleIndex - 1; //puzzle will always have to have the next camera from the room
                            dialogueRunner.Dialogue.Stop();
                            dialogueRunner.StartDialogue("FirstPuzzle");
                        }
                        else
                        {
                            doingPuzzle = true;
                            CamManager.instance.MoveToCam(puzzle.puzzleIndex);
                            previousCamUsed = puzzle.puzzleIndex - 1; //puzzle will always have to have the next camera from the room
                            dialogueRunner.Dialogue.Stop();
                            dialogueRunner.StartDialogue("FirstPuzzleReminder");
                        }
                    }
                        
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(1))
            {
                doingPuzzle= false;
                CamManager.instance.MoveToCam(previousCamUsed);
                dialogueRunner.Dialogue.Stop();
            }
        }
    }
}
