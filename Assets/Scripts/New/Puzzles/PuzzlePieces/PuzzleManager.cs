using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public int partsToComplete;
    int partsCompleted;
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>(); 
    }

    public void CheckParts()
    {
        partsCompleted++;
        if (partsCompleted == partsToComplete)
        {
            partsCompleted = 0;
            //puzzle completed
            //teleport to room and trigger endgame dialogue
            anim.enabled = true;
            anim.Play("turnShelf");
            PlayerNav player = FindObjectOfType<PlayerNav>();
            //player.TriggerFinishedBookcasePuzzle();
        }
    }
}
