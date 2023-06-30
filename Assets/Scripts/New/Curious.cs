using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curious : MonoBehaviour
{
    public int curiousIndex;


    private void OnMouseDown()
    {
        TriggerCuriousDialogue();
        Debug.Log("Interacting with " + gameObject.name);
    }

    void TriggerCuriousDialogue()
    {
        NasaDialogueManager.instance.CuriousTalk(curiousIndex);
    }
}
