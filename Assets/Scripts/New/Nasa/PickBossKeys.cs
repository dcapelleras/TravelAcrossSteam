using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickBossKeys : EventClickable
{

    public override void ExecuteAction()
    {
        base.ExecuteAction();
        gameObject.SetActive(false);
    }
}
