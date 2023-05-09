using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingProgrammingPuzzleTrigger : EventTriggerBase
{
    [SerializeField] Transform stopPosition;
    public override void ExecuteTrigger()
    {
        base.ExecuteTrigger();
        if (alreadyExecuted == 1)
        {
            NasaDialogueManager.instance.StartProgrammingPuzzle();
            NasaNavigation nav = FindObjectOfType<NasaNavigation>();
            nav.MoveToThisDestination(stopPosition);
        }
    }
}
