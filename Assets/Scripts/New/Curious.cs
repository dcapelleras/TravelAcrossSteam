using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

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
        if (FindObjectOfType<DialogueRunner>().IsDialogueRunning)
        {
            return;
        }
        if (isRecepcionist)
        {
            NasaDialogueManager.instance.CuriousTalk(index);
            index = curiousIndex + 1;
            return;
        }
        NasaDialogueManager.instance.CuriousTalk(curiousIndex);
    }
}
