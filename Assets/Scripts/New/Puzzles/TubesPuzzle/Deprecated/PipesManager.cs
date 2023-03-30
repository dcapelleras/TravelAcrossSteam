using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipesManager : MonoBehaviour
{
    int amountToFinish;
    public List<TurnablePipe> pipes= new List<TurnablePipe>();

    private void Awake()
    {
        amountToFinish=pipes.Count;
    }

    public void CheckPuzzle()
    {
        int counter = 0;
        foreach (TurnablePipe pipe in pipes)
        {
            if (pipe.complete)
            {
                counter++;
            }
        }
        if (counter >= amountToFinish)
        {
            PuzzleComplete();
        }
    }

    void PuzzleComplete()
    {
        Debug.Log("Puzzle complete, congratulations");
    }
}
