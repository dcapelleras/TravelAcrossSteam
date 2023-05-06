using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnteringOfficesFirstTime : EventTriggerBase
{
    public override void ExecuteTrigger()
    {
        base.ExecuteTrigger();
        if (alreadyExecuted == 1)
        {
            NasaDialogueManager.instance.EnterOfficesFirstTime();
            
        }
    }
}
