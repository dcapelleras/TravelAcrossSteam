using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public int partsToComplete;
    int partsCompleted;
    Animator anim;
    public int puzzleIndex = 0;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void CheckParts()
    {
        partsCompleted++;
        if (partsCompleted == partsToComplete)
        {
            PlayerNav player = FindObjectOfType<PlayerNav>();
            if (puzzleIndex == 0)
            {
                partsCompleted = 0;
                //puzzle completed
                //teleport to room and trigger endgame dialogue
                //anim.enabled = true;
                //anim.Play("turnShelf");

                player.TriggerFinishedBookcasePuzzle();
            }
            else if (puzzleIndex == 1)
            {
                player.TriggerFinishMachinePuzzle();
            }
        }
    }
}
