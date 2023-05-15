using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingProgrammingPuzzleTrigger : EventTriggerBase
{
    [SerializeField] Transform stopPosition;
    [SerializeField] NasaNavigation nav;
    public override void ExecuteTrigger()
    {
        switch (alreadyExecuted)
        {
            case 0:
                base.ExecuteTrigger();
                NasaDialogueManager.instance.StartProgrammingPuzzle();
                nav.MoveToThisDestination(stopPosition);
                break;
            case 1:
                NasaDialogueManager.instance.GotKeysAndTalkedToMargaret();
                nav.MoveToThisDestination(stopPosition);
                if (nav.hasBossKeys)
                {
                    base.ExecuteTrigger();
                }
                break;

        }

        if (alreadyExecuted == 0)
        {
            base.ExecuteTrigger();
            NasaDialogueManager.instance.StartProgrammingPuzzle();
            NasaNavigation nav = FindObjectOfType<NasaNavigation>();
            nav.MoveToThisDestination(stopPosition);
        }
    }
}
