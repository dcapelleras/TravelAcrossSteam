using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickBossKeys : EventClickable
{
    Margaret margaret;
    [SerializeField]GameObject keyUI;

    private void Awake()
    {
        margaret = FindObjectOfType<Margaret>();
    }
    public override void ExecuteAction()
    {
        if (margaret != null)
        {
            margaret.SitDown();
        }
        base.ExecuteAction();
        NasaNavigation nav = FindObjectOfType<NasaNavigation>();
        nav.placeToGoBackWhenCaught = 0;
        nav.hasBossKeys = true;
        keyUI.SetActive(true);
        gameObject.SetActive(false);
    }
}
