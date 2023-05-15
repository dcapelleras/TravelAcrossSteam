using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class NasaDialogueMovement : MonoBehaviour
{
    DialogueRunner dialogueRunner;
    [SerializeField] NasaDoor receptionDoor;
    NasaNavigation nav;
    [SerializeField]Janitor janitor;

    private void Awake()
    {
        dialogueRunner = FindObjectOfType<DialogueRunner>();
        dialogueRunner.AddCommandHandler<int>("movement", AllowMovement);
        dialogueRunner.AddCommandHandler("callJanitor", CallJanitor);
        nav = GetComponent<NasaNavigation>();
    }

    public void AllowMovement(int allowed)
    {
        if (allowed == 0)
        {
            nav.canMove = false;
        }
        else
        {
            nav.canMove = true;
        }
    }

    public void CheckClosedReceptionDoor()
    {
        dialogueRunner.Dialogue.Stop();
        dialogueRunner.StartDialogue("TriedOpeningDoor");
    }

    public void CallJanitor()
    {
        janitor.GoAssistDoor();
    }
}
