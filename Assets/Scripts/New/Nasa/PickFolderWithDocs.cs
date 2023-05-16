using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickFolderWithDocs : EventClickable
{
    public override void ExecuteAction()
    {
        base.ExecuteAction();
        NasaNavigation nav = FindObjectOfType<NasaNavigation>();
        nav.hasFolderWithDocs= true;
        gameObject.SetActive(false);
    }
}
