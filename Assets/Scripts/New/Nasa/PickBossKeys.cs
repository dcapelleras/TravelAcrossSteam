using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickBossKeys : EventClickable
{

    public override void ExecuteAction()
    {
        base.ExecuteAction();
        NasaNavigation nav = FindObjectOfType<NasaNavigation>();
        nav.hasBossKeys = true;
        gameObject.SetActive(false);
    }
}
