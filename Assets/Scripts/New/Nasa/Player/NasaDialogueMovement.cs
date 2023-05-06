using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.AI;

public class NasaDialogueMovement : MonoBehaviour
{
    DialogueRunner dialogueRunner;
    [SerializeField] NasaDoor receptionDoor;
    NasaNavigation nav;
    [SerializeField]Janitor janitor;

    private void Awake()
    {
        dialogueRunner = FindObjectOfType<DialogueRunner>();
        nav = GetComponent<NasaNavigation>();
    }

    private void Start()
    {
        dialogueRunner.AddCommandHandler<int>("movement", AllowMovement);
        dialogueRunner.AddCommandHandler("callJanitor", CallJanitor);
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
