using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curious : MonoBehaviour
{
    public int curiousIndex;
    public bool isRecepcionist;
    int index;

    private void Awake()
    {
        index = curiousIndex;
    }

    private void OnMouseDown()
    {
        TriggerCuriousDialogue();
    }

    void TriggerCuriousDialogue()
    {
        if (isRecepcionist)
        {
            NasaDialogueManager.instance.CuriousTalk(index);
            index = curiousIndex + 1;
            return;
        }
        NasaDialogueManager.instance.CuriousTalk(curiousIndex);
    }
}
