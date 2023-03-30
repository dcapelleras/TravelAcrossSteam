using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public int partsToComplete;
    int partsCompleted;

    public void CheckParts()
    {
        partsCompleted++;
        if (partsCompleted == partsToComplete)
        {
            partsCompleted = 0;
            //puzzle completed
            //teleport to room and trigger endgame dialogue
            PlayerNav player = FindObjectOfType<PlayerNav>();
            player.TriggerEndGame(1);
        }
    }
}
