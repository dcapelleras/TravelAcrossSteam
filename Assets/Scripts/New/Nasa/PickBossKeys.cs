using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickBossKeys : EventClickable
{
    Margaret margaret;

    private void Awake()
    {
        margaret = FindObjectOfType<Margaret>();
    }
    public override void ExecuteAction()
    {
        margaret.SitDown();
        base.ExecuteAction();
        NasaNavigation nav = FindObjectOfType<NasaNavigation>();
        nav.hasBossKeys = true;
        gameObject.SetActive(false);
    }
}
