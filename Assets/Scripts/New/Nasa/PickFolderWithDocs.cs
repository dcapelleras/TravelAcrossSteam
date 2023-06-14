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
        nav.MoveToThisDestination(placeToPick);
        //nav.rend.flipX= false;
        //start puzzle game
        //change camera for cam[4]
        NasaDialogueManager.instance.StartSlidingPuzzle();
        gameObject.SetActive(false);
    }
}
