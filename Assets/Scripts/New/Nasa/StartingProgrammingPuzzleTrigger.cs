using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingProgrammingPuzzleTrigger : EventTriggerBase
{
    [SerializeField] Transform stopPosition;
    [SerializeField] NasaNavigation nav;
    public override void ExecuteTrigger()
    {

        if (alreadyExecuted == 0)
        {
            alreadyExecuted = 1;
            NasaDialogueManager.instance.StartProgrammingPuzzle();
            NasaNavigation nav = FindObjectOfType<NasaNavigation>();
            nav.MoveToThisDestination(stopPosition);
        }

        if (nav.hasBossKeys && alreadyExecuted == 1)
        {
            alreadyExecuted = 2;
            NasaDialogueManager.instance.GotKeysAndTalkedToMargaret();
            nav.MoveToThisDestination(stopPosition);
        }

        if (alreadyExecuted == 2 && nav.hasFolderWithDocs)
        {
            alreadyExecuted = 3;
            NasaDialogueManager.instance.GotDocumentsToMargaret();
            nav.MoveToThisDestination(stopPosition);
        }
    }
}
